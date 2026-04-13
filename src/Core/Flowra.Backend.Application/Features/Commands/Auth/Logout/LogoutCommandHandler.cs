using Flowra.Backend.Application.Abstractions.Infrastructure.Token;
using Flowra.Backend.Application.Abstractions.Presentation;
using Flowra.Backend.Application.Common.Exceptions;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.SystemMessages;
using MediatR;

namespace Flowra.Backend.Application.Features.Commands.Auth.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommandRequest, SuccessDetails>
    {
        private readonly ITokenService _tokenService;
        private readonly IRequestContext _requestContext;

        public LogoutCommandHandler(ITokenService tokenService, IRequestContext requestContext)
        {
            _tokenService = tokenService;
            _requestContext = requestContext;
        }

        public async Task<SuccessDetails> Handle(LogoutCommandRequest request, CancellationToken cancellationToken)
        {
            if (_requestContext.UserId is null)
                throw new UnauthorizedException("Oturum bilgisi bulunamadı.");

            await _tokenService.RevokeAllAsync(_requestContext.UserId.Value);

            return ResultResponse.Success(ResponseMessages.Auth.Logout_Success);
        }
    }
}
