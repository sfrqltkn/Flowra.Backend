using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Allowence;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Allowances.GetAllAllowances
{
    public class GetAllAllowancesQueryHandler : IRequestHandler<GetAllAllowancesQueryRequest, SuccessDetails<IEnumerable<AllowanceDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAllowancesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails<IEnumerable<AllowanceDto>>> Handle(GetAllAllowancesQueryRequest request, CancellationToken cancellationToken)
        {
            var readRepository = _unitOfWork.ReadRepository<Allowance, int>();

            var allowances = await readRepository.GetListAsync(x => true, cancellationToken);

            var dtos = allowances.Select(a => new AllowanceDto
            {
                Id = a.Id,
                PersonName = a.PersonName,
                Amount = a.Amount
            });

            return ResultResponse.Success(dtos, "Ödenekler başarıyla listelendi.");
        }
    }
}
