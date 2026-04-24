using Flowra.Backend.Application.Common.Auth;
using System.Text.Json.Serialization;

namespace Flowra.Backend.Application.DTOs.Auth
{
    public sealed class LoginCommandDto: ITokenCookieMutation
    {
        public LoginResponseDto Response { get; set; } = default!;

        [JsonIgnore]
        public string? AccessToken { get; set; }

        [JsonIgnore]
        public DateTime? AccessTokenExpiresAtUtc { get; set; }

        [JsonIgnore]
        public string? RefreshToken { get; set; }

        [JsonIgnore]
        public DateTime? RefreshTokenExpiresAtUtc { get; set; }

        [JsonIgnore]
        public bool ClearAccessTokenCookie => false;

        [JsonIgnore]
        public bool ClearRefreshTokenCookie => false;
    }
}
