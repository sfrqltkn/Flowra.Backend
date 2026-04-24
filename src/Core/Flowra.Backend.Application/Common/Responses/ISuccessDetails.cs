namespace Flowra.Backend.Application.Common.Responses
{
    public interface ISuccessDetails
    {
        int Status { get; }
        string Detail { get; }
        object? DataObject { get; }
    }
}