namespace Linn.Production.Domain.LinnApps.BuildPlans
{
    using Linn.Production.Domain.LinnApps.Exceptions;

    public class BuildPlanDetail
    {
        public string BuildPlanName { get; set; }

        public string PartNumber { get; set; }

        public int FromLinnWeekNumber { get; set; }

        public int? ToLinnWeekNumber { get; set; }

        public string RuleCode { get; set; }

        public int? Quantity { get; set; }

        public void Validate()
        {
            if (this.RuleCode == "TRIGGER")
            {
                this.Quantity = null;
            }
            else if (this.RuleCode == "FIXED" && this.Quantity == null)
            {
                throw new BuildPlanDetailInvalidException("You must specify a quantity for FIXED builds");
            }
        }
    }
}