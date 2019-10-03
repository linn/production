namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    public interface IWorksOrderProxyService
    {
        string CanRaiseWorksOrder(string partNumber);

        int GetNextBatch(string partNumber);
    }
}
