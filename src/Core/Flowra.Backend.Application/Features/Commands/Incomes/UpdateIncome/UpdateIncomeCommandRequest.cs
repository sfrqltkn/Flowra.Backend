using Flowra.Backend.Application.Common.Responses;
using MediatR;
using System.Text.Json.Serialization;

namespace Flowra.Backend.Application.Features.Commands.Incomes.UpdateIncome
{
    public class UpdateIncomeCommandRequest : IRequest<SuccessDetails>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public bool IsRecurring { get; set; }
    }
}
