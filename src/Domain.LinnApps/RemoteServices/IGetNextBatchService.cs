namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    public interface IGetNextBatchService
    {
        int GetNextBatch(string partNumber);
    }
}
