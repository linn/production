namespace Linn.Production.Domain.LinnApps.Models
{
    using System;

    public class AteTestReportDetail
    {
        public int TestId { get; set; }

        public DateTime DateTested { get; set; }

        public string BoardPartNumber{ get; set; }

        public int NumberTested { get; set; }

        public int ItemNumber { get; set; }

        public string ComponentPartNumber { get; set; }

        public int? NumberOfFails { get; set; }

        public string CircuitRef { get; set; }

        public string AteTestFaultCode { get; set; }

        public string SmtOrPcb { get; set; }

        public string BatchNumber { get; set; }
    }
}