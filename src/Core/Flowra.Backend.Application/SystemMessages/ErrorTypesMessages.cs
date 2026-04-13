namespace Flowra.Backend.Application.SystemMessages
{
    public static class ErrorTypesMessages
    {
        private const string BasePath = "errors";

        public const string BadRequest = $"{BasePath}/bad-request";
        public const string Validation = $"{BasePath}/validation";
        public const string NotFound = $"{BasePath}/not-found";
        public const string Conflict = $"{BasePath}/conflict";
        public const string Unauthorized = $"{BasePath}/unauthorized";
        public const string Forbidden = $"{BasePath}/forbidden";
        public const string BusinessRule = $"{BasePath}/business-rule";
        public const string Database = $"{BasePath}/database";
        public const string Integration = $"{BasePath}/integration";
        public const string ServiceUnavailable = $"{BasePath}/service-unavailable";
        public const string Timeout = $"{BasePath}/timeout";
        public const string Internal = $"{BasePath}/internal";
    }
}
