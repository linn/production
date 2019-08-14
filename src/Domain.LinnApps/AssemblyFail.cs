namespace Linn.Production.Domain.LinnApps
{
    using System;

    public class AssemblyFail
    {
        public int Id { get; set; }

        public string InSlot { get; set; }

        public DateTime DateTimeFound { get; set; }

        public DateTime? CompletedBy { get; set; }

        public DateTime? DateInvalid { get; set; }

        public int? SerialNumber { get; set; }

        public int? WorksOrderNumber { get; set; }

        public WorksOrder WorksOrder { get; set; }

        public string ReportedFault { get; set; }
    }
}