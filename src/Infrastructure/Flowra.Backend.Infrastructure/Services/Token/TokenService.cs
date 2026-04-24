using Flowra.Backend.Application.Abstractions.Infrastructure.Token;
using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Exceptions;
using Flowra.Backend.Application.SystemMessages;
using Flowra.Backend.Domain.Identity;
using Flowra.Backend.Infrastructure.Settings;
using Flowra.BackendApplication.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Flowra.Backend.Infrastructure.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public TokenService(IOptions<JwtSettings> jwtOptions, UserManager<User> userManager, RoleManager<Role> roleManager, IUnitOfWork unitOfWork)
        {
            _jwtSettings = jwtOptions.Value;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<AccessTokenResult> GenerateAccessTokenAsync(User user, IEnumerable<Claim>? extraClaims = null)
        {
            var now = DateTime.UtcNow;
            var expiresAt = now.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes);
            var jti = Guid.NewGuid().ToString();

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email ?? string.Empty),
                new(ClaimTypes.Name, user.UserName ?? string.Empty),
                new(JwtRegisteredClaimNames.Jti, jti),
                new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var roleName in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleName));

                var role = await _roleManager.FindByNameAsync(roleName);
                if (role is not null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    claims.AddRange(roleClaims);
                }
            }

            if (extraClaims is not null)
            {
                claims.AddRange(extraClaims);
            }

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                Expires = expiresAt,
                SigningCredentials = GetSigningCredentials()
            };

            var encryptingCredentials = GetEncryptingCredentials();
            if (encryptingCredentials is not null)
            {
                descriptor.EncryptingCredentials = encryptingCredentials;
            }

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(descriptor);
            var tokenString = handler.WriteToken(token);

            return new AccessTokenResult
            {
                Token = tokenString,
                ExpiresAtUtc = expiresAt
            };
        }

        public async Task<RefreshToken> CreateRefreshTokenAsync(User user)
        {
            var now = DateTime.UtcNow;

            var refresh = new RefreshToken
            {
                UserId = user.Id,
                Token = GenerateRefreshToken(),
                ExpiresAtUtc = now.AddDays(_jwtSettings.RefreshTokenExpirationDays),
            };

            await _unitOfWork.WriteRepository<RefreshToken, int>().AddAsync(refresh);
            await _unitOfWork.SaveChangesAsync();

            return refresh;
        }

        public async Task<RefreshToken> ValidateRefreshTokenAsync(string refreshToken)
        {
            var token = await _unitOfWork.ReadRepository<RefreshToken, int>().GetAsync(x => x.Token == refreshToken);

            if (token == null)
            {
                throw new UnauthorizedException(ResponseMessages.Token.RefreshTokenInvalid);
            }

            if (!token.IsActive)
            {
                throw new UnauthorizedException(ResponseMessages.Token.RefreshTokenExpired);
            }

            return token;
        }

        public async Task<RefreshToken> RotateRefreshTokenAsync(User user, string oldToken)
        {
            var existing = await _unitOfWork.ReadRepository<RefreshToken, int>().GetAsync(x => x.Token == oldToken);

            if (existing == null)
            {
                throw new UnauthorizedException(ResponseMessages.Token.RefreshTokenNotFound);
            }

            if (existing.IsRevoked)
            {
                await RevokeAllAsync(user.Id);
                throw new UnauthorizedException(ResponseMessages.Token.SecurityAlert);
            }

            if (!existing.IsActive)
            {
                throw new UnauthorizedException(ResponseMessages.Token.RefreshTokenExpired);
            }
            var now = DateTime.UtcNow;
            var newRefreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = GenerateRefreshToken(),
                ExpiresAtUtc = now.AddDays(_jwtSettings.RefreshTokenExpirationDays),
            };

            existing.Revoke(newRefreshToken.Token, reason: "Rotated");
            //Eski refresh token'ı kullanılmış olarak işaretliyoruz, böylece aynı token ile birden
            //fazla kez refresh token alma girişimlerini engellemiş oluyoruz.
            existing.MarkAsUsed();

            _unitOfWork.WriteRepository<RefreshToken, int>().Update(existing);
            await _unitOfWork.WriteRepository<RefreshToken, int>().AddAsync(newRefreshToken);
            await _unitOfWork.SaveChangesAsync();

            return newRefreshToken;
        }

        public async Task RevokeAllAsync(int userId)
        {
            var tokens = await _unitOfWork.ReadRepository<RefreshToken, int>().GetListAsync(x => x.UserId == userId && x.RevokedAtUtc == null); if (!tokens.Any()) return;

            foreach (var t in tokens)
                t.Revoke(null, "Revoke All Requested");

            _unitOfWork.WriteRepository<RefreshToken, int>().UpdateRange(tokens);
            await _unitOfWork.SaveChangesAsync();
        }
        private SigningCredentials GetSigningCredentials()
        {
            var signingKey = new SymmetricSecurityKey(Convert.FromBase64String(_jwtSettings.SigningKey));
            return new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        }

        private EncryptingCredentials GetEncryptingCredentials()
        {
            if (string.IsNullOrWhiteSpace(_jwtSettings.EncryptionKey))
            {
                return null;
            }

            var encryptionKey = new SymmetricSecurityKey(Convert.FromBase64String(_jwtSettings.EncryptionKey));

            return new EncryptingCredentials(encryptionKey, SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512);
        }

        private static string GenerateRefreshToken()
        {
            byte[] randomBytes = RandomNumberGenerator.GetBytes(64);
            return Microsoft.AspNetCore.WebUtilities.WebEncoders.Base64UrlEncode(randomBytes);
        }

    }
}
