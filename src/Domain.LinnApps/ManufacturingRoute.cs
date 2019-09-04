namespace Linn.Production.Domain.LinnApps
{
    using System.Collections.Generic;

    public class ManufacturingRoute
    {
        public ManufacturingRoute(string routeCode, string description, string notes)
        {
            this.RouteCode = routeCode;
            this.Description = description;
            this.Notes = notes;
        }

        public string RouteCode { get; set; }

        public string Description { get; set; }

        public string Notes { get; set; }

        public IEnumerable<ManufacturingOperation> Operations { get; set; }
    }
}
