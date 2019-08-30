namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ManufacturingRouteResourceBuilder : IResourceBuilder<ManufacturingRoute>
    {
        private readonly ManufacturingOperationsResourceBuilder manufacturingOperationsResourceBuilder = new ManufacturingOperationsResourceBuilder();
        public ManufacturingRouteResource Build(ManufacturingRoute manufacturingRoute)
        {
            return new ManufacturingRouteResource
                       {
                           RouteCode = manufacturingRoute.RouteCode,
                           Description = manufacturingRoute.Description,
                           Links = this.BuildLinks(manufacturingRoute).ToArray(),
                           Operations = this.BuildOperations(manufacturingRoute.Operations)
                       };
        }

        public string GetLocation(ManufacturingRoute manufacturingRoute)
        {
            return $"/production/resources/manufacturing-routes/{Uri.EscapeDataString(manufacturingRoute.RouteCode)}";
        }

        object IResourceBuilder<ManufacturingRoute>.Build(ManufacturingRoute manufacturingRoute) => this.Build(manufacturingRoute);

        private IEnumerable<LinkResource> BuildLinks(ManufacturingRoute manufacturingRoute)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(manufacturingRoute) };
        }

        private IEnumerable<ManufacturingOperationResource> BuildOperations(IEnumerable<ManufacturingOperation> operations)
        {
            return this.manufacturingOperationsResourceBuilder.Build(operations);
        }
    }
}