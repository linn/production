namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ManufacturingRoutesResourceBuilder : IResourceBuilder<ResponseModel<IEnumerable<ManufacturingRoute>>>
    {
        private readonly ManufacturingRouteResourceBuilder manufacturingRouteResourceBuilder;

        public ManufacturingRoutesResourceBuilder(IAuthorisationService authorisationService)
        {
            this.manufacturingRouteResourceBuilder = new ManufacturingRouteResourceBuilder(authorisationService);
        }

        public IEnumerable<ManufacturingRouteResource> Build(ResponseModel<IEnumerable<ManufacturingRoute>> model)
        {
            var manufacturingRoutes = model.ResponseData;

            return manufacturingRoutes
                .OrderBy(b => b.RouteCode)
                .Select(a => this.manufacturingRouteResourceBuilder.Build(new ResponseModel<ManufacturingRoute>(a, model.Privileges)));
        }

        object IResourceBuilder<ResponseModel<IEnumerable<ManufacturingRoute>>>.Build(ResponseModel<IEnumerable<ManufacturingRoute>> manufacturingRoutes) => this.Build(manufacturingRoutes);

        public string GetLocation(ResponseModel<IEnumerable<ManufacturingRoute>> manufacturingRoutes)
        {
            throw new NotImplementedException();
        }
    }
}
