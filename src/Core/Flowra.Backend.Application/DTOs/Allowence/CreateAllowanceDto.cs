namespace Flowra.Backend.Application.DTOs.Allowence
{
    public class CreateAllowanceDto
    {
        public string PersonName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
