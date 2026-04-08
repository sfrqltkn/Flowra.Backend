namespace Flowra.Backend.Domain.Absractions
{
    public interface ITrackable
    {
        void SetCreated(int? userId, DateTime createdAtUtc);
        void SetUpdated(int? userId, DateTime updatedAtUtc);
    }
}
