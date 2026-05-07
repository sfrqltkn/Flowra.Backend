using Flowra.Backend.Application.Abstractions.Persistence.Repositories.Identity;
using Flowra.Backend.Domain.Identity;
using Flowra.Backend.Persistence.Main.Context;
using Microsoft.EntityFrameworkCore;

namespace Flowra.Backend.Persistence.Repositories.Identity
{
    public class UserRepository : IUserRepository
    {
        private readonly FlowraDbContext _context;

        public UserRepository(FlowraDbContext context)
        {
            _context = context;
        }

        public async Task<IList<User>> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Users
                   .Include(u => u.UserRoles)
                   .ThenInclude(ur => ur.Role)
                   .AsSplitQuery()
                   .AsNoTracking()
                   .ToListAsync(cancellationToken);
        }
    }
}
