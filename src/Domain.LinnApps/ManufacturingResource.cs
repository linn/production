namespace Linn.Production.Domain.LinnApps
{
    using System;

    public class ManufacturingResource
    {
        public ManufacturingResource(
            string resourceCode, 
            string description, 
            double? cost, 
            DateTime? dateInvalid)
        {
            this.ResourceCode = resourceCode;
            this.Description = description;
            this.Cost = cost;
            this.DateInvalid = dateInvalid;
        }

        public string ResourceCode { get; set; }

        public string Description { get; set; }

        public double? Cost { get; set; }

        public DateTime? DateInvalid { get; set; }
    }
}
