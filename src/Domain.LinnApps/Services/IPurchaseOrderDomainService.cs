namespace Linn.Production.Domain.LinnApps.Services
{
    using Linn.Production.Domain.LinnApps.Models;

    public interface IPurchaseOrderDomainService
    {
        PurchaseOrderWithSernosInfo BuildPurchaseOrderWithSernosInfo(PurchaseOrder purchaseOrder);
    }
}