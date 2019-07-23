namespace Linn.Production.Domain.LinnApps
{
    using System;

    public class BuildsSummary
    {
        public DateTime WeekEnd { get; set; }

        public decimal Value { get; set; }

        public string Department { get; set; }

        public decimal DaysToBuild { get; set; }
    }
}