namespace Linn.Production.Resources.RequestResources
{
    public class AssemblyFailsDetailsReportRequestResource : FromToDateRequestResource
    {
        public string BoardPartNumber { get; set; }

        public string CircuitPartNumber { get; set; }

        public string FaultCode { get; set; }

        public string CitCode { get; set; }

        public string Board { get; set; }

        public int? Person { get; set; }
    }
}