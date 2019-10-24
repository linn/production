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

        public int? Quantity { get; set; }

        public string Batch { get; set; }

        public PartFailFaultCode FaultCode { get; set; }

        public PartFailErrorType ErrorType { get; set; }

        public string Story { get; set; }

        public WorksOrder WorksOrder { get; set; }

        public StorageLocation StorageLocation { get; set; }

        public int? MinutesWasted { get; set; }

        public void UpdateFrom(PartFail partFail, PartFail updated)
        {
            partFail.Part = updated.Part;
            partFail.PurchaseOrderNumber = updated.PurchaseOrderNumber;
            partFail.WorksOrder = updated.WorksOrder;
            partFail.Batch = updated.Batch;
            partFail.ErrorType = updated.ErrorType;
            partFail.FaultCode = updated.FaultCode;
            partFail.MinutesWasted = updated.MinutesWasted;
            partFail.Quantity = updated.Quantity;
            partFail.Story = updated.Story;
            partFail.StorageLocation = updated.StorageLocation;
        }
    }
}