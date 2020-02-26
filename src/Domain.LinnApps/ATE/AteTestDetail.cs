﻿namespace Linn.Production.Domain.LinnApps.ATE
{
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class AteTestDetail
    {
        public int TestId { get; set; }

        public int ItemNumber { get; set; }

        public string PartNumber { get; set; }

        public int? NumberOfFails { get; set; }

        public string CircuitRef { get; set; }

        public string AteTestFaultCode { get; set; }

        public string SmtOrPcb { get; set; }

        public string Shift { get; set; }

        public int? BatchNumber { get; set; }

        public Employee PcbOperator { get; set; }

        public string Comments { get; set; }

        public string Machine { get; set; }

        public int? BoardFailNumber { get; set; }

        public string AoiEscape { get; set; }

        public string CorrectiveAction { get; set; }

        public int? SmtFailId { get; set; }

        public string BoardSerialNumber { get; set; }
    }
}