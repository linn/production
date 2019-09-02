﻿namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public interface IManufacturingOperationsFacade : IFacadeService<ManufacturingOperation, string, ManufacturingOperationResource, ManufacturingOperationResource>
    {
        IResult<ManufacturingOperation> Update(
            string routeCode,
            string manufacturingId,
            ManufacturingOperationResource resource);

        IResult<ManufacturingOperation> GetById(string routeCode, string manufacturingId);
    }
}
