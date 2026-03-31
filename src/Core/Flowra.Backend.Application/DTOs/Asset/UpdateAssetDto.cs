namespace Flowra.Backend.Application.DTOs.Asset
{
    public class UpdateAssetDto
    {
        public int Id { get; set; }
        public string MonthYear { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal EstimatedUnitValue { get; set; }
    }
}
