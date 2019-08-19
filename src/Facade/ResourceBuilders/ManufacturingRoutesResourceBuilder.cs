namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ManufacturingRoutesResourceBuilder : IResourceBuilder<IEnumerable<ManufacturingRoute>>
    {
        private readonly ManufacturingRouteResourceBuilder manufacturingRouteResourceBuilder = new ManufacturingRouteResourceBuilder();

        public IEnumerable<ManufacturingRouteResource> Build(IEnumerable<ManufacturingRoute> manufacturingRoutes)
        {
            return manufacturingRoutes
                .OrderBy(b => b.RouteCode)
                .Select(a => this.manufacturingRouteResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<ManufacturingRoute>>.Build(IEnumerable<ManufacturingRoute> manufacturingRoutes) => this.Build(manufacturingRoutes);

        public string GetLocation(IEnumerable<ManufacturingRoute> manufacturingRoutes)
        {
            throw new NotImplementedException();
        }
    }
}
