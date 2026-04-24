using Flowra.Backend.Domain.Identity;
using Flowra.BackendApplication.DTOs.Auth;
using System.Security.Claims;

namespace Flowra.Backend.Application.Abstractions.Infrastructure.Token
{
    public interface ITokenService
    {
        // Access Token üretimi
        Task<AccessTokenResult> GenerateAccessTokenAsync(User user, IEnumerable<Claim>? extraClaims = null);

        // Refresh token oluşturma 
        Task<RefreshToken> CreateRefreshTokenAsync(User user);

        // Refresh token doğrulama
        Task<RefreshToken> ValidateRefreshTokenAsync(string refreshToken);

        // Refresh token döndürme 
        Task<RefreshToken> RotateRefreshTokenAsync(User user, string oldToken);

        // Kullanıcının tüm oturumlarını kapatma
        Task RevokeAllAsync(int userId);
    }
}
