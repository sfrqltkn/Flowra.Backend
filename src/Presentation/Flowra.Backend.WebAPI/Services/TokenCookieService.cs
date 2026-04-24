using Flowra.Backend.Presentation.Abstractions;
using Flowra.Backend.WebAPI.Authentication;
using Microsoft.Extensions.Options;

namespace Flowra.Backend.WebAPI.Services
{
    public sealed class TokenCookieService : ITokenCookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtCookieOptions _jwtCookieOptions;

        public TokenCookieService(
            IHttpContextAccessor httpContextAccessor,
            IOptions<JwtCookieOptions> jwtCookieOptions)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtCookieOptions = jwtCookieOptions.Value;
        }

        public void SetAccessToken(string accessToken, DateTime expiresAtUtc)
        {
            var context = GetHttpContext();

            context.Response.Cookies.Append(
                JwtCookieNames.AccessToken,
                accessToken,
                BuildCookieOptions(expiresAtUtc, _jwtCookieOptions.AccessTokenPath));
        }

        public void SetRefreshToken(string refreshToken, DateTime expiresAtUtc)
        {
            var context = GetHttpContext();

            context.Response.Cookies.Append(
                JwtCookieNames.RefreshToken,
                refreshToken,
                BuildCookieOptions(expiresAtUtc, _jwtCookieOptions.RefreshTokenPath));
        }

        public string? GetRefreshToken()
        {
            var context = GetHttpContext();

            return context.Request.Cookies.TryGetValue(JwtCookieNames.RefreshToken, out var refreshToken)
                ? refreshToken
                : null;
        }

        public void ClearAccessToken()
        {
            var context = GetHttpContext();

            context.Response.Cookies.Delete(
                JwtCookieNames.AccessToken,
                BuildDeleteCookieOptions(_jwtCookieOptions.AccessTokenPath));
        }

        public void ClearRefreshToken()
        {
            var context = GetHttpContext();

            context.Response.Cookies.Delete(
                JwtCookieNames.RefreshToken,
                BuildDeleteCookieOptions(_jwtCookieOptions.RefreshTokenPath));
        }

        public void ClearAll()
        {
            ClearAccessToken();
            ClearRefreshToken();
        }

        private HttpContext GetHttpContext()
        {
            return _httpContextAccessor.HttpContext
                ?? throw new InvalidOperationException("Active HttpContext bulunamadı.");
        }

        private CookieOptions BuildCookieOptions(DateTime expiresAtUtc, string path)
        {
            return new CookieOptions
            {
                HttpOnly = _jwtCookieOptions.HttpOnly,
                Secure = _jwtCookieOptions.Secure,
                SameSite = _jwtCookieOptions.SameSite,
                Expires = new DateTimeOffset(expiresAtUtc),
                Path = path,
                IsEssential = _jwtCookieOptions.IsEssential
            };
        }

        private CookieOptions BuildDeleteCookieOptions(string path)
        {
            return new CookieOptions
            {
                HttpOnly = _jwtCookieOptions.HttpOnly,
                Secure = _jwtCookieOptions.Secure,
                SameSite = _jwtCookieOptions.SameSite,
                Path = path,
                IsEssential = _jwtCookieOptions.IsEssential
            };
        }
    }
}