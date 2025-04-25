namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class ManufacturingSkillResource : HypermediaResource
    {
        public string SkillCode { get; set; }

        public string Description { get; set; }

        public int? HourlyRate { get; set; }

        public string DateInvalid { get; set; }
    }
}
