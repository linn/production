namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ManufacturingRouteResourceBuilder : IResourceBuilder<ResponseModel<ManufacturingRoute>>
    {
        private readonly ManufacturingOperationsResourceBuilder manufacturingOperationsResourceBuilder = new ManufacturingOperationsResourceBuilder();
        private readonly IAuthorisationService authorisationService;

        public ManufacturingRouteResourceBuilder(IAuthorisationService authorisationService)
        {
            this.authorisationService = authorisationService;
        }

        public ManufacturingRouteResource Build(ResponseModel<ManufacturingRoute> model)
        {
            var manufacturingRoute = model.ResponseData;
            return new ManufacturingRouteResource
            {
                RouteCode = manufacturingRoute.RouteCode,
                Description = manufacturingRoute.Description,
                Notes = manufacturingRoute.Notes,
                Links = this.BuildLinks(model).ToArray(),
                Operations = manufacturingRoute.Operations != null ? this.BuildOperations(manufacturingRoute.Operations) : null
            };
        }

        public string GetLocation(ResponseModel<ManufacturingRoute> model)
        {
            return $"/production/resources/manufacturing-routes/{Uri.EscapeDataString(model.ResponseData.RouteCode)}";
        }

        object IResourceBuilder<ResponseModel<ManufacturingRoute>>.Build(ResponseModel<ManufacturingRoute> model) => this.Build(model);

        private IEnumerable<LinkResource> BuildLinks(ResponseModel<ManufacturingRoute> model)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(model) };

            if (this.authorisationService.HasPermissionFor(AuthorisedAction.ManufacturingRouteUpdate, model.Privileges))
            {
                yield return new LinkResource { Rel = "edit", Href = this.GetLocation(model) };
            }
        }

        private IEnumerable<ManufacturingOperationResource> BuildOperations(IEnumerable<ManufacturingOperation> operations)
        {
            return this.manufacturingOperationsResourceBuilder.Build(operations);
        }
    }
}
