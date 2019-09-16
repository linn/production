namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    public interface IProductAuditPack
    {
        void GenerateProductAudits(int orderNumber);
    }
}
