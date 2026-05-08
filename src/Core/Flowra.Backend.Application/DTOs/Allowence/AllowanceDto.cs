namespace Flowra.Backend.Application.DTOs.Allowence
{
    public class AllowanceDto
    {
        public int Id { get; set; }
        public string PersonName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
