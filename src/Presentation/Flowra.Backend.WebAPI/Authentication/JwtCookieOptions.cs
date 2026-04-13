namespace Flowra.Backend.WebAPI.Authentication
{
    public sealed class JwtCookieOptions
    {
        public bool HttpOnly { get; set; } = true;
        public bool Secure { get; set; } = true;
        public SameSiteMode SameSite { get; set; } = SameSiteMode.None;
        public string AccessTokenPath { get; set; } = "/";
        public string RefreshTokenPath { get; set; } = "/";
        public bool IsEssential { get; set; } = true;
    }
}