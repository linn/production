namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    using Resources;

    public interface ISernosRenumPack
    {
        bool ReissueSerialNumber(SerialNumberReissueResource resource);

        string GetSernosRenumMessage();
    }
}