using Flowra.Backend.Application.Abstractions.Infrastructure;
using Flowra.Backend.Application.Abstractions.Presentation;
using Flowra.Backend.Application.Common.Exceptions;
using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Auth;
using Flowra.Backend.Application.Extensions;
using MediatR;
using System.Security.Claims;

namespace Europower.EuroScada.Application.Features.Queries.Auth.GetCurrentUser
{
    public sealed class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQueryRequest, SuccessDetails<CurrentUserDto>>
    {
        private readonly IRequestContext _requestContext;
        private readonly IUserService _userService;

        public GetCurrentUserQueryHandler(IRequestContext requestContext,IUserService userService)
        {
            _requestContext = requestContext;
            _userService = userService;
        }
        public async Task<SuccessDetails<CurrentUserDto>> Handle(GetCurrentUserQueryRequest request, CancellationToken cancellationToken)
        {
            var userId = _requestContext.UserId;

            if (userId is null)
                throw new UnauthorizedException("Oturum bilgisi bulunamadı.");

            var claims = _requestContext.User?.Claims?.ToList() ?? new List<Claim>();

            var roles = claims
               .Where(c => c.Type == ClaimTypes.Role || c.Type == "role")
               .Select(c => c.Value)
               .Distinct(StringComparer.OrdinalIgnoreCase)
               .ToList();

            var permissions = claims
                .Where(c => c.Type == "permission")
                .Select(c => c.Value)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            var user = await _userService.FindByIdAsync(userId.Value.ToString());

            user.ThrowIfNull("Eklenecek");

            var dto = new CurrentUserDto
            {
                Id = userId,
                UserName = user.UserName!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = roles,
                Permissions = permissions
            };


            return ResultResponse.Success(dto, "Eklenecek");
        }
    }
}
