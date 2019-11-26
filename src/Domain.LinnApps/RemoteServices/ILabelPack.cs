namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    public interface ILabelPack
    {
        string GetLabelData(string labelTypeCode, int serialNumber, string articleNumber);
    }
}
