using Flowra.Backend.Application.Abstractions.Presentation;
using Flowra.Backend.Domain.Absractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Flowra.Backend.Persistence.Interceptors
{
    public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            ApplySoftDelete(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            ApplySoftDelete(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private static void ApplySoftDelete(DbContext? context)
        {
            if (context is null)
                return;

            var entries = context.ChangeTracker
                .Entries()
                .Where(e =>
                    e.State == EntityState.Deleted &&
                    e.Entity is ISoftDelete);

            foreach (var entry in entries)
            {
                entry.State = EntityState.Modified;

                var softDeleteEntity = (ISoftDelete)entry.Entity;
                softDeleteEntity.MarkAsDeleted(DateTime.UtcNow);
            }
        }
    }
}