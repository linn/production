namespace Linn.Production.Domain.LinnApps
{
    using System;

    public class LinnWeek
    {
        public int LinnWeekNumber { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string WWYYYY { get; set; }

        public string WWSYY { get; set; }

        public string WeekEndingDDMON { get; set; }

        public int? LinnMonthEndWeekNumber { get; set; }
    }
}