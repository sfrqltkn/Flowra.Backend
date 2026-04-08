using Flowra.Backend.Domain.Identity;

namespace Flowra.Backend.Application.Abstractions.Persistence.Repositories.Identity
{
    public interface IUserRepository
    {
        Task<IList<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    }
}
