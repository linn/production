namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class ManufacturingOperationResource : HypermediaResource
    {
        public string RouteCode { get; set; }

        public int ManufacturingId { get; set; }

        public int OperationNumber { get; set; }

        public string Description { get; set; }

        public string SkillCode { get; set; }

        public string ResourceCode { get; set; }

        public int SetAndCleanTime { get; set; }

        public int CycleTime { get; set; }

        public int LabourPercentage { get; set; }

        public string CITCode { get; set; }
    }
}
