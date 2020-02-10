namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Resources;

    public interface IPurchaseOrderService : IFacadeService<PurchaseOrder, int, PurchaseOrderResource, PurchaseOrderResource>
    {
        SuccessResult<PurchaseOrderWithSernosInfo> GetPurchaseOrderWithSernosInfo(int id);
    }
}
