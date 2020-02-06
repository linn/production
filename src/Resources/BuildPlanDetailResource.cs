namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class BuildPlanDetailResource : HypermediaResource
    {
        public string BuildPlanName { get; set; }

        public string PartNumber { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string RuleCode { get; set; }

        public int? Quantity { get; set; }

        public string PartDescription { get; set; }
    }
}
