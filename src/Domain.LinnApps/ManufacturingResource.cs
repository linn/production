namespace Linn.Production.Domain.LinnApps
{
    using System;

    public class ManufacturingResource
    {
        public string ResourceCode { get; set; }

        public string Description { get; set; }

        public double? Cost { get; set; }

        public DateTime? DateInvalid { get; set; }
    }
}
