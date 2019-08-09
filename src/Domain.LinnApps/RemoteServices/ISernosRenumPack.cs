namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    using Resources;

    public interface ISernosRenumPack
    {
        string ReissueSerialNumber(SerialNumberReissueResource resource);
    }
}