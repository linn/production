namespace Linn.Production.Domain.LinnApps
{
    using System;

    public class Build
    {
        public string Tref { get; set; }

        public string DepartmentCode { get; set; }

        public string PartNumber { get; set; } 

        public decimal MaterialPrice { get; set; }

        public decimal LabourPrice { get; set; }

        public decimal Quantity { get; set; }

        public DateTime BuildDate { get; set; }
    }
}