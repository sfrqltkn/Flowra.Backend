namespace Flowra.Backend.Domain.Common
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; }
        DateTime? DeletedAtUtc { get; }
        void MarkAsDeleted(DateTime? deletedAtUtc = null);
        void Restore();
    }
}
