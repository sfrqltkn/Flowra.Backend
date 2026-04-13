using Flowra.Backend.Application.Abstractions.Presentation;
using Flowra.Backend.Domain.Absractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Flowra.Backend.Persistence.Interceptors
{
    public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        private readonly IRequestContext _requestContext;

        public SoftDeleteInterceptor(IRequestContext requestContext)
        {
            _requestContext = requestContext;
        }

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

        private void ApplySoftDelete(DbContext? context)
        {
            if (context is null) return;

            // Performans için sadece ISoftDelete olan ve silinmek istenenleri filtrele
            var entries = context.ChangeTracker
                .Entries<ISoftDelete>()
                .Where(e => e.State == EntityState.Deleted);

            // Kimin sildiği bilgisini al (UserId veya UserName)
            var deletedBy = _requestContext.UserId?.ToString() ?? "system";

            foreach (var entry in entries)
            {
                // İşlemi silmeden güncellemeye (Soft Delete) çevir
                entry.State = EntityState.Modified;

                // Entity üzerindeki silme metodunu çalıştır
                entry.Entity.MarkAsDeleted(deletedBy);
            }
        }
    }
}