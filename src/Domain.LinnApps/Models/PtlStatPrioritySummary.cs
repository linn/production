namespace Linn.Production.Domain.LinnApps.Models
{
    using System;
    using System.ComponentModel.Design;
    using System.Data.SqlTypes;
    using System.Xml.Schema;

    public class PtlStatPrioritySummary
    {
         public PtlStatPrioritySummary(int priority)
        {
            this.Priority = priority;
        }

        public int Priority { get; set; }

        public int Triggers { get; set; } = 0;

        public int OneDay { get; set; } = 0;

        public int TwoDay { get; set; } = 0;

        public int ThreeDay { get; set; } = 0;

        public int FourDay { get; set; } = 0;

        public int FiveDay { get; set; } = 0;

        public int Gt5Day { get; set; } = 0;

        public decimal Percentile95 { get; set; }

        public decimal PercBy5Day()
        {
            if (this.Triggers == 0)
            {
                return 0;
            }

            return Decimal.Round((((decimal)(this.Triggers - this.Gt5Day)) / (decimal)this.Triggers) * 100);
        }

        public decimal AvgTurnaround()
        {
            if (this.Triggers == 0)
            {
                return 0;
            }
            return Decimal.Round(this.TotalTurnover / this.Triggers,1);
        }

        public void AddStatToSummary(PtlStat stat)
        {
            this.Triggers++;
            this.TotalTurnover += stat.WorkingDays;

            if (stat.WorkingDays < 2) 
            {
                this.OneDay++;
            } 
            else if (stat.WorkingDays < 3)
            {
                this.TwoDay++;
            }
            else if (stat.WorkingDays < 4)
            {
                this.ThreeDay++;
            }
            else if (stat.WorkingDays < 5)
            {
                this.FourDay++;
            }
            else if (stat.WorkingDays < 6)
            {
                this.FiveDay++;
            }
            else
            {
                this.Gt5Day++;
            }
        }

        private decimal TotalTurnover { get; set; }
    }
}
