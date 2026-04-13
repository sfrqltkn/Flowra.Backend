using Flowra.Backend.Domain.Entities;
using Flowra.Backend.Domain.Identity;
using Flowra.Backend.Persistence.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Flowra.Backend.Persistence.Main.Context
{
    public class FlowraDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public FlowraDbContext(DbContextOptions<FlowraDbContext> options)
          : base(options)
        {
        }
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<CashRecord> CashRecords { get; set; }
        public DbSet<Allowance> Allowances { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplySoftDeleteQueryFilter();
            builder.ApplyConfigurationsFromAssembly(typeof(FlowraDbContext).Assembly);
        }
    }
}
