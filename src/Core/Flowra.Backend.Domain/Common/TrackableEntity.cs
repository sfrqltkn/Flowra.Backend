namespace Flowra.Backend.Domain.Common
{
    public abstract class TrackableEntity<TId> : BaseEntity<TId>
    {
        public int? CreatedBy { get; private set; }
        public DateTime? CreatedAtUtc { get; private set; }
        public int? UpdatedBy { get; private set; }
        public DateTime? UpdatedAtUtc { get; private set; }

        public void SetCreated(int? userId, DateTime? createdAtUtc = null)
        {
            CreatedBy = userId;
            CreatedAtUtc = createdAtUtc ?? DateTime.UtcNow;
        }

        public void SetUpdated(int? userId, DateTime? updatedAtUtc = null)
        {
            UpdatedBy = userId;
            UpdatedAtUtc = updatedAtUtc ?? DateTime.UtcNow;
        }
    }
}
