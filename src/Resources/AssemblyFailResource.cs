namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class AssemblyFailResource : HypermediaResource
    {
        public int Id { get; set; }

        public int EnteredBy { get; set; }

        public string EnteredByName { get; set; }

        public int WorksOrderNumber { get; set; }

        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public int NumberOfFails { get; set; }

        public int? SerialNumber { get; set; }

        public string DateTimeFound { get; set; }

        public string InSlot { get; set; }

        public string Machine { get; set; }

        public string ReportedFault { get; set; }

        public string Analysis { get; set; }

        public string EngineeringComments { get; set; }

        public string BoardPartNumber { get; set; }

        public string BoardDescription { get; set; }

        public string BoardSerial { get; set; }

        public string Shift { get; set; }

        public string Batch { get; set; }

        public string CircuitRef { get; set; }

        public string CircuitPartNumber { get; set; }

        public string CitResponsible { get; set; }

        public string CitResponsibleName { get; set; }

        public int? PersonResponsible { get; set; }

        public string PersonResponsibleName { get; set; }

        public string FaultCode { get; set; }

        public string FaultCodeDescription { get; set; }

        public string DateTimeComplete { get; set; }

        public int? CompletedBy { get; set; }

        public string CompletedByName { get; set; }

        public string OutSlot { get; set; }

        public int? ReturnedBy { get; set; }

        public string ReturnedByName { get; set; }

        public string CorrectiveAction { get; set; }

        public string CaDate { get; set; }

        public string DateInvalid { get; set; }
    }
}