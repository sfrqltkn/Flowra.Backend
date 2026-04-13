using Flowra.Backend.Application.Abstractions.Infrastructure;
using Flowra.Backend.Application.Common.Exceptions;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.SystemMessages;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Users.SetActiveUser
{
    public class SetActiveUserCommandHandler : IRequestHandler<SetActiveUserCommandRequest, SuccessDetails>
    {
        private readonly IUserService _userService;

        public SetActiveUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<SuccessDetails> Handle(SetActiveUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByIdAsync(request.Id.ToString());
            if (user is null)
                throw new NotFoundException("Durumu güncellenmek istenen kullanıcı sistemde bulunamadı.");

            //Kullanıcının mevcut durumuyla istenen durum aynıysa, gereksiz güncelleme yapmamak adına başarılı bir yanıt döner
            if (user.IsActive == request.IsActive)
                return ResultResponse.Success(ResponseMessages.Common.OperationSuccess);

            user.IsActive = request.IsActive;
            var result = await _userService.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
                throw new OperationFailedException($"Kullanıcı durumu güncellenirken hata meydana geldi: {errors}");
            }

            if (!request.IsActive)
            {
                //Kullanıcının tüm aktif oturumlarını düşürür
                await _userService.UpdateSecurityStampAsync(user);
            }

            return ResultResponse.Success(ResponseMessages.Common.OperationSuccess);
        }
    }
}
