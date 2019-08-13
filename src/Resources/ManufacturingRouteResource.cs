namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class ManufacturingRouteResource : HypermediaResource
    {
        public string RouteCode { get; set; }   

        public string Description { get; set; }

        public string Notes { get; set; }
    }
}
