namespace Linn.Production.Domain.LinnApps.Measures
{
    using System;

    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    public class AssemblyFail
    {
        public int Id { get; set; }

        public Employee EnteredBy { get; set; }

        public int NumberOfFails { get; set; }

        public string InSlot { get; set; }

        public DateTime DateTimeFound { get; set; }

        public string Machine { get; set; }

        public string BoardSerial { get; set; }

        public string Shift { get; set; }

        public string Batch { get; set; }

        public string AoiEscape { get; set; }

        public string CircuitPartRef { get; set; }

        public string CircuitPart { get; set; }

        public Part BoardPart { get; set; }

        public Cit CitResponsible { get; set; }

        public string BoardPartNumber { get; set; }

        public Employee PersonResponsible { get; set; }

        public Employee CompletedBy { get; set; }

        public Employee ReturnedBy { get; set; }

        public DateTime? DateInvalid { get; set; }

        public DateTime? DateTimeComplete { get; set; }

        public DateTime? CaDate { get; set; }

        public int? SerialNumber { get; set; }

        public WorksOrder WorksOrder { get; set; }

        public string ReportedFault { get; set; }

        public string OutSlot { get; set; }

        public string CorrectiveAction { get; set; }

        public AssemblyFailFaultCode FaultCode { get; set; }

        public string Analysis { get; set; }

        public string EngineeringComments { get; set; }
    }
}