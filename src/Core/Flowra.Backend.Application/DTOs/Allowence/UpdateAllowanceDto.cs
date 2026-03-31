namespace Flowra.Backend.Application.DTOs.Allowence
{
    public class UpdateAllowanceDto
    {
        public int Id { get; set; }
        public string PersonName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
