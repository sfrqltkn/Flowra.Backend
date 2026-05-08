namespace Flowra.Backend.Application.DTOs.Asset
{
    public class AssetDto
    {
        public int Id { get; set; }
        public DateTime MonthYear { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal EstimatedUnitValue { get; set; }
    }
}
