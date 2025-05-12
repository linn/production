namespace Linn.Production.Domain.LinnApps
{
    using System;

    public class ManufacturingResource
    {
        public ManufacturingResource(string resourceCode, string description, decimal? cost)
        {
            this.ResourceCode = resourceCode;
            this.Description = description;
            this.Cost = cost;
        }

        public string ResourceCode { get; set; }

        public string Description { get; set; }

        public decimal? Cost { get; set; }

        public DateTime? DateInvalid { get; set; }
    }
}
