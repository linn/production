namespace Linn.Production.Domain.LinnApps.WorksOrders
{
    using System;
    using System.Collections.Generic;

    using Linn.Production.Domain.LinnApps.Measures;

    public class WorksOrder
    {
        public int? BatchNumber { get; set; }

        public int? CancelledBy { get; set; }

        public DateTime? DateCancelled { get; set; }

        public DateTime DateRaised { get; set; }

        public string KittedShort { get; set; }

        public int? LabelsPrinted { get; set; }

        public string Outstanding { get; set; }

        public int OrderNumber { get; set; }

        public string PartNumber { get; set; }

        public int QuantityOutstanding { get; set; }

        public int QuantityBuilt { get; set; }

        public int RaisedBy { get; set; }

        public string RaisedByDepartment { get; set; }

        public string ReasonCancelled { get; set; }

        public string StartedByShift { get; set; }

        public string DocType { get; set; }

        public string WorkStationCode { get; set; }

        public List<AssemblyFail> AssemblyFails { get; set; }

        public void CancelWorksOrder(int cancelledBy, string reasonCancelled, DateTime dateCancelled)
        {
            this.CancelledBy = cancelledBy;
            this.ReasonCancelled = reasonCancelled;
            this.DateCancelled = dateCancelled;
        }

        public void UpdateWorksOrder(
            int? batchNumber,
            int? cancelledBy,
            DateTime? dateCancelled,
            string kittedShort,
            int? labelsPrinted,
            string outstanding,
            int quantityOutstanding,
            int quantityBuilt,
            string reasonCancelled,
            string startedByShift,
            string docType,
            string workStationCode)
        {
            this.BatchNumber = batchNumber;
            this.CancelledBy = cancelledBy;
            this.DateCancelled = dateCancelled;
            this.KittedShort = kittedShort;
            this.LabelsPrinted = labelsPrinted;
            this.Outstanding = outstanding;
            this.QuantityOutstanding = quantityOutstanding;
            this.QuantityBuilt = quantityBuilt;
            this.ReasonCancelled = reasonCancelled;
            this.StartedByShift = startedByShift;
            this.DocType = docType;
            this.WorkStationCode = workStationCode;
        }
    }
}
