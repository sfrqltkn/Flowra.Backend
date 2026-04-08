using Flowra.Backend.Domain.Identity;

namespace Flowra.Backend.Application.Abstractions.Persistence.Repositories.Identity
{
    public interface IRoleRepository
    {
        Task<IList<Role>> GetAllRolesAsync(CancellationToken cancellationToken = default);

    }
}
