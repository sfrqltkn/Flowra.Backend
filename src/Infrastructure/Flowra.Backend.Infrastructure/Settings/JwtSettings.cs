namespace Flowra.Backend.Infrastructure.Settings
{
    public sealed class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SigningKey { get; set; } = string.Empty;
        public string? EncryptionKey { get; set; }
        public bool RequireHttpsMetadata { get; set; } = true;
        public int AccessTokenExpirationMinutes { get; set; } = 1;
        public int RefreshTokenExpirationDays { get; set; } = 7;
    }
}
