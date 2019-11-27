namespace Linn.Production.Domain.LinnApps
{
    using System;

    public class LabelReprint
    {
        public int LabelReprintId { get; set; }

        public DateTime DateIssued { get; set; }

        public int RequestedBy { get; set; }

        public string Reason { get; set; }

        public string PartNumber { get; set; }

        public int? SerialNumber { get; set; }

        public string DocumentType { get; set; }

        public int? DocumentNumber { get; set; }

        public string LabelTypeCode { get; set; }

        public int NumberOfProducts { get; set; }

        public string ReprintType { get; set; }

        public string NewPartNumber { get; set; }
    }
}
