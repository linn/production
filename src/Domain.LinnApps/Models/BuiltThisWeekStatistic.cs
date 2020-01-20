namespace Linn.Production.Domain.LinnApps.Models
{
    public class BuiltThisWeekStatistic
    {
        public string CitCode { get; set; }

        public string CitName { get; set; }

        public string PartNumber { get; set; }

        public string Description { get; set; }

        public int BuiltThisWeek { get; set; }

        public decimal Value { get; set; }

        public double Days { get; set; }
    }
}