namespace Flowra.Backend.Domain.Common
{
    public abstract class BaseEntity<TId> : IEquatable<BaseEntity<TId>>, ISoftDelete
    {
        // Identity (ID Yönetimi)

        public TId Id { get; protected set; } = default!;

        public bool IsTransient => EqualityComparer<TId>.Default.Equals(Id, default!);

        // Soft Delete (Silinme Yönetimi)
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAtUtc { get; private set; }

        public void MarkAsDeleted(DateTime? deletedAtUtc = null)
        { if (!IsDeleted) { IsDeleted = true; DeletedAtUtc = deletedAtUtc ?? DateTime.UtcNow; } }

        public void Restore()
        { IsDeleted = false; DeletedAtUtc = null; }


        // Equality Checks (Eşitlik Kontrolleri)

        public override bool Equals(object? obj)
            => Equals(obj as BaseEntity<TId>);

        public bool Equals(BaseEntity<TId>? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (IsTransient || other.IsTransient)
                return false;

            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }
    }
}
