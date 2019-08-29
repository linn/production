namespace Linn.Production.Resources
{
    using System.Collections.Generic;

    using Linn.Common.Resources;

    public class ManufacturingRouteResource : HypermediaResource
    {
        public string RouteCode { get; set; }   

        public string Description { get; set; }

        public string Notes { get; set; }

        public IEnumerable<ManufacturingOperationResource> Operations { get; set; }

    }
}
