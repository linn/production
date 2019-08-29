namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class WorksOrderResource : HypermediaResource
    {
        public int CancelledBy { get; set; }

        public string DateCancelled { get; set; }

        public string DateRaised { get; set; }

        public int? LabelsPrinted { get; set; }

        public int OrderNumber { get; set; }

        public string PartNumber { get; set; }

        public int QuantityOutstanding { get; set; }

        public int QuantityBuilt { get; set; }

        public int RaisedBy { get; set; }

        public string RaisedByDepartment { get; set; }

        public string ReasonCancelled { get; set; }

        public string Type { get; set; }

        public string WorkStationCode { get; set; }
    }
}
