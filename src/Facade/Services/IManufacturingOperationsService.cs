namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public interface IManufacturingOperationsService : IFacadeService<ManufacturingOperation, int,
        ManufacturingOperationResource, ManufacturingOperationResource>
    {
        void RemoveOperation(ManufacturingOperation entity);
    }
}
