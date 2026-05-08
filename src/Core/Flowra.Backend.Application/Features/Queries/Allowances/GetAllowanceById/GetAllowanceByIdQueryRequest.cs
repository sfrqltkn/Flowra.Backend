using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Allowence;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Allowances.GetAllowanceById
{
    public class GetAllowanceByIdQueryRequest : IRequest<SuccessDetails<AllowanceDto>>
    {
        public int Id { get; set; }
    }
}
