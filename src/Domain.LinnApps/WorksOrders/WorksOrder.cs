namespace Linn.Production.Domain.LinnApps.WorksOrders
{
    using System;
    using System.Collections.Generic;

    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.Measures;

    public class WorksOrder
    {
        public int OrderNumber { get; set; }

        public int? BatchNumber { get; set; }

        public int? CancelledBy { get; set; }

        public DateTime? DateCancelled { get; set; }

        public DateTime DateRaised { get; set; }

        public string KittedShort { get; set; }

        public int? LabelsPrinted { get; set; }

        public string Outstanding { get; set; }

        public Part Part { get; set; }

        public string PartNumber { get; set; }

        public int Quantity { get; set; }

        public int? QuantityOutstanding { get; set; }

        public int? QuantityBuilt { get; set; }

        public int RaisedBy { get; set; }

        public string RaisedByDepartment { get; set; }

        public string ReasonCancelled { get; set; }

        public string StartedByShift { get; set; }

        public string DocType { get; set; }

        public string WorkStationCode { get; set; }

        public string ZoneName { get; set; }

        public List<AssemblyFail> AssemblyFails { get; set; }

        public List<PartFail> PartFails { get; set; }

        public void CancelWorksOrder(int? cancelledBy, string reasonCancelled)
        {
            if (cancelledBy == null || string.IsNullOrEmpty(reasonCancelled))
            {
                throw new InvalidWorksOrderException("You must provide a user number and reason when cancelling a works order");
            }

            this.CancelledBy = cancelledBy;
            this.ReasonCancelled = reasonCancelled;
            this.DateCancelled = DateTime.UtcNow;
        }

        public void UpdateWorksOrder(int quantity)
        {
            this.Quantity = quantity;
        }
    }
}
