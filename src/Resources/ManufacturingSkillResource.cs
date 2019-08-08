using Linn.Common.Resources;

namespace Linn.Production.Resources
{
    public class ManufacturingSkillResource : HypermediaResource
    {
        public string SkillCode { get; set; }   

        public string Description { get; set; }

        public int? HourlyRate { get; set; }
    }
}
