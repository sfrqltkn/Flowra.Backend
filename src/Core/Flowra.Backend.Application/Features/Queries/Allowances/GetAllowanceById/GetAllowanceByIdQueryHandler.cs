using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Allowence;
using Flowra.Backend.Application.Extensions;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Queries.Allowances.GetAllowanceById
{
    public class GetAllowanceByIdQueryHandler : IRequestHandler<GetAllowanceByIdQueryRequest, SuccessDetails<AllowanceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllowanceByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails<AllowanceDto>> Handle(GetAllowanceByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var readRepository = _unitOfWork.ReadRepository<Allowance, int>();

            var allowance = await readRepository.GetByIdAsync(request.Id, cancellationToken);

            allowance.ThrowIfNull("Getirilmek istenen ödenek kaydı bulunamadı.");

            var dto = new AllowanceDto
            {
                Id = allowance.Id,
                PersonName = allowance.PersonName,
                Amount = allowance.Amount
            };

            return ResultResponse.Success(dto, "Ödenek başarıyla getirildi.");
        }
    }
}
