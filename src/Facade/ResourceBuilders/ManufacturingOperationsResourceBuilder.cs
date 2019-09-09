namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ManufacturingOperationsResourceBuilder : IResourceBuilder<IEnumerable<ManufacturingOperation>>
    {
        private readonly ManufacturingOperationResourceBuilder manufacturingOperationResourceBuilder = new ManufacturingOperationResourceBuilder();

        public IEnumerable<ManufacturingOperationResource> Build(IEnumerable<ManufacturingOperation> manufacturingOperations)
        {
            return manufacturingOperations
                .OrderBy(b => b.ResourceCode)
                .Select(a => this.manufacturingOperationResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<ManufacturingOperation>>.Build(IEnumerable<ManufacturingOperation> manufacturingOperations) => this.Build(manufacturingOperations);

        public string GetLocation(IEnumerable<ManufacturingOperation> manufacturingOperations)
        {
            throw new NotImplementedException();
        }
    }
}
