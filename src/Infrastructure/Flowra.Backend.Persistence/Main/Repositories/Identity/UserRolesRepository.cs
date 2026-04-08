using Flowra.Backend.Application.Abstractions.Persistence.Repositories.Identity;
using Flowra.Backend.Application.DTOs.UserRoles;
using Flowra.Backend.Persistence.Main.Context;
using Microsoft.EntityFrameworkCore;

namespace Flowra.Backend.Persistence.Main.Repositories.Identity
{
    public class UserRolesRepository : IUserRolesRepository
    {
        private readonly FlowraDbContext _context;

        public UserRolesRepository(FlowraDbContext context)
        {
            _context = context;
        }

        public async Task<IList<UserRolesDto>> GetAllUserRolesAsync(CancellationToken cancellationToken = default)
        {
            var query = from ur in _context.UserRoles
                        join u in _context.Users on ur.UserId equals u.Id
                        join r in _context.Roles on ur.RoleId equals r.Id
                        select new UserRolesDto
                        {
                            UserId = ur.UserId,
                            UserName = u.UserName!,
                            RoleId = ur.RoleId,
                            RoleName = r.Name!
                        };

            return await query.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
