namespace Linn.Production.Domain.LinnApps
{
    public class ManufacturingResource
    {
        public ManufacturingResource(string resourceCode, string description, double? cost)
        {
            this.ResourceCode = resourceCode;
            this.Description = description;
            this.Cost = cost;
        }

        public string ResourceCode { get; set; }

        public string Description { get; set; }

        public double? Cost { get; set; }
    }
}
