namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    public interface IWwdTrigFunction
    {
        int WwdTriggerRun(string partNumber, int qty);
    }
}