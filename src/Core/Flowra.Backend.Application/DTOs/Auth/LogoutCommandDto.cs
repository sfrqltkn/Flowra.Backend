using Flowra.Backend.Application.Common.Auth;
using System.Text.Json.Serialization;

namespace Flowra.Backend.Application.DTOs.Auth
{
    public sealed class LogoutCommandDto : ITokenCookieMutation
    {
        [JsonIgnore]
        public string? AccessToken => null;

        [JsonIgnore]
        public DateTime? AccessTokenExpiresAtUtc => null;

        [JsonIgnore]
        public string? RefreshToken => null;

        [JsonIgnore]
        public DateTime? RefreshTokenExpiresAtUtc => null;

        [JsonIgnore]
        public bool ClearAccessTokenCookie { get; set; }

        [JsonIgnore]
        public bool ClearRefreshTokenCookie { get; set; }
    }
}
