using Flowra.Backend.Application.DTOs.UserRoles;

namespace Flowra.Backend.Application.Abstractions.Persistence.Repositories.Identity
{
    public interface IUserRolesRepository
    {
        Task<IList<UserRolesDto>> GetAllUserRolesAsync(CancellationToken cancellationToken = default);
    }
}
