namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class ManufacturingResourceResource : HypermediaResource
    {
        public string ResourceCode { get; set; }

        public string Description { get; set; }

        public decimal? Cost { get; set; }

        public string DateInvalid { get; set; }
    }
}
