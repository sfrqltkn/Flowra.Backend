using Flowra.Backend.Application.Abstractions.Persistence;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Domain.Entities;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Allowances.CreateAllowance
{
    public class CreateAllowanceCommandHandler : IRequestHandler<CreateAllowanceCommandRequest, SuccessDetails<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAllowanceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessDetails<int>> Handle(CreateAllowanceCommandRequest request, CancellationToken cancellationToken)
        {
            var allowance = new Allowance
            {
                PersonName = request.PersonName,
                Amount = request.Amount
            };

            var writeRepository = _unitOfWork.WriteRepository<Allowance, int>();

            await writeRepository.AddAsync(allowance, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultResponse.Success(allowance.Id, "Ödenek başarıyla eklendi.");
        }
    }
}
