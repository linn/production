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

        public int Quantity { get; set; }

        public string Batch { get; set; }

        public PartFailFaultCode FaultCode { get; set; }

        public string ErrorType { get; set; }

        public string Story { get; set; }

        public WorksOrder WorksOrder { get; set; }

        public StoragePlace StoragePlace { get; set; }

        public int MinutesWasted { get; set; }
    }
}