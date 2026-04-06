namespace Flowra.Backend.Application.DTOs.FinanceData
{
    public class LivePriceDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; } = "TRY";
    }
}
