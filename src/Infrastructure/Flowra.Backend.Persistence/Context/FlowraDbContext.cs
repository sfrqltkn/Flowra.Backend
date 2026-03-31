using Flowra.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Flowra.Backend.Persistence.Context
{
    public class FlowraDbContext : DbContext
    {
        public FlowraDbContext(DbContextOptions<FlowraDbContext> options) : base(options) { }

        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<CashRecord> CashRecords { get; set; }
        public DbSet<Allowance> Allowances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
