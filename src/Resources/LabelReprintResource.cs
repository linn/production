namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class LabelReprintResource : HypermediaResource
    {
        public int LabelReprintId { get; set; }

        public string DateIssued { get; set; }

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
