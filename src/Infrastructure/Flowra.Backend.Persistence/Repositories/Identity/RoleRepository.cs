using Flowra.Backend.Application.Abstractions.Persistence.Repositories.Identity;
using Flowra.Backend.Domain.Identity;
using Flowra.Backend.Persistence.Main.Context;
using Microsoft.EntityFrameworkCore;

namespace Flowra.Backend.Persistence.Repositories.Identity
{
    public class RoleRepository : IRoleRepository
    {
        private readonly FlowraDbContext _context;

        public RoleRepository(FlowraDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Role>> GetAllRolesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                  .AsNoTracking()
                  .ToListAsync(cancellationToken);
        }
    }
}