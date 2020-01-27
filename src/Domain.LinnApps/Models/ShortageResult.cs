namespace Linn.Production.Domain.LinnApps.Models
{
    using System;
    using Linn.Common.Reporting.Models;

    public class ShortageResult
    {
        public string PartNumber { get; set; }

        public string Priority { get; set; }

        public int? Build { get; set; }

        public int? CanBuild { get; set; }

        public decimal? BackOrderQty { get; set; }

        public int Kanban { get; set; }

        public DateTime? EarliestRequestedDate { get; set; }

        public ResultsModel Results { get; set; }
    }
}