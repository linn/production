namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public interface IManufacturingResourceFacadeService : IFacadeFilterService<ManufacturingResource, string, ManufacturingResourceResource, ManufacturingResourceResource, ManufacturingResourceResource>
    {
        IResult<IEnumerable<ManufacturingResource>> GetValid();
    }
}