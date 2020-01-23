namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public interface IPurchaseOrderService : IFacadeService<PurchaseOrder, int, PurchaseOrderResource, PurchaseOrderResource>
    {
        int GetFirstSernos(int documentNumber);

        int GetLastSernos(int documentNumber);

        int GetSernosIssued(int documentNumber);

        int GetSernosBuilt(int documentNumber, string partNumber, int firstSernos, int lastSernos);
    }
}
