namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class ProductionTriggerLevelResource : HypermediaResource
    {
        public string PartNumber { get; set; }

        public string Description { get; set; }
    }
}