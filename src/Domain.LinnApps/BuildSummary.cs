namespace Domain.LinnApps
{
    using System;

    public class BuildSummary
    {
        public DateTime WeekEnd { get; set; }

        public decimal Value { get; set; }

        public string Department { get; set; }

        public decimal DaysToBuild { get; set; }
    }
}