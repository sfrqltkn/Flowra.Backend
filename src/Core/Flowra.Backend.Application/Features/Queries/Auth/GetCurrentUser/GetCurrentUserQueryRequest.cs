using Flowra.Backend.Application.Common.Responses;
using Flowra.Backend.Application.DTOs.Auth;
using MediatR;

namespace Europower.EuroScada.Application.Features.Queries.Auth.GetCurrentUser
{
    public class GetCurrentUserQueryRequest : IRequest<SuccessDetails<CurrentUserDto>>
    {
    }
}
