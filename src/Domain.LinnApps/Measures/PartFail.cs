namespace Linn.Production.Domain.LinnApps.Measures
{
    using System;

    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    public class PartFail
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public Employee EnteredBy { get; set; }

        public Part Part { get; set; }

        public int? PurchaseOrderNumber { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }

        public decimal? Quantity { get; set; }

        public string Batch { get; set; }

        public PartFailFaultCode FaultCode { get; set; }

        public PartFailErrorType ErrorType { get; set; }

        public string Story { get; set; }

        public WorksOrder WorksOrder { get; set; }

        public StorageLocation StorageLocation { get; set; }

        public int? MinutesWasted { get; set; }

        public int? SerialNumber { get; set; }
        
        public string Comments { get; set; }

        public Employee Owner { get; set; }

        public string NoCost { get; set; }

        public string SentenceDecision { get; set; }

        public string SentenceReason { get; set; }

        public DateTime? DateSentenced { get; set; }

        public void UpdateFrom(PartFail updated)
        {
            this.PurchaseOrderNumber = updated.PurchaseOrderNumber;
            this.WorksOrder = updated.WorksOrder;
            this.Part = updated.Part;
            this.Batch = updated.Batch;
            this.ErrorType = updated.ErrorType;
            this.FaultCode = updated.FaultCode;
            this.MinutesWasted = updated.MinutesWasted;
            this.Quantity = updated.Quantity;
            this.Story = updated.Story;
            this.StorageLocation = updated.StorageLocation;
            this.SerialNumber = updated.SerialNumber;
            this.Comments = updated.Comments;
            this.Owner = updated.Owner;
            this.NoCost = updated.NoCost;
            this.SentenceDecision = updated.SentenceDecision;
            this.SentenceReason = updated.SentenceReason;
            this.DateSentenced = updated.DateSentenced;
        }
    }
}
