namespace Linn.Production.Domain.LinnApps
{
    using System;

    public class PartFailLog
    {
        public int Id { get; set; }

        public DateTime? DateCreated { get; set; }

        public string PartNumber { get; set; }

        public string FaultCode { get; set; }

        public string Story { get; set; }

        public int? Quantity { get; set; }

        public int? MinutesWasted { get; set; }

        public string ErrorType { get; set; }

        public string Batch { get; set; }

        public int EnteredBy { get; set; }

        public Part Part { get; set; }
    }
}
