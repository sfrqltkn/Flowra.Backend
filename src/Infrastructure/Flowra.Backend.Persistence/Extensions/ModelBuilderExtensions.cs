using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Flowra.Backend.Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplySoftDeleteQueryFilter(this ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var isDeletedProperty = entityType.FindProperty("IsDeleted");

                if (isDeletedProperty != null && isDeletedProperty.ClrType == typeof(bool))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, "IsDeleted");
                    var falseConstant = Expression.Constant(false);
                    var body = Expression.Equal(property, falseConstant);
                    var lambda = Expression.Lambda(body, parameter);
                    entityType.SetQueryFilter(lambda);
                }
            }
        }
    }
}