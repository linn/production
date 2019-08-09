namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ManufacturingSkillResourceBuilder : IResourceBuilder<ManufacturingSkill>
    {
        public ManufacturingSkillResource Build(ManufacturingSkill manufacturingSkill)
        {
            return new ManufacturingSkillResource
            {
                SkillCode = manufacturingSkill.SkillCode,
                Description = manufacturingSkill.Description,
                HourlyRate = manufacturingSkill.HourlyRate,
                Links = this.BuildLinks(manufacturingSkill).ToArray()
            };
        }

        public string GetLocation(ManufacturingSkill manufacturingSkill)
        {
            return $"/production/resources/manufacturing-skills/{Uri.EscapeDataString(manufacturingSkill.SkillCode)}";
        }

        object IResourceBuilder<ManufacturingSkill>.Build(ManufacturingSkill manufacturingSkill) => this.Build(manufacturingSkill);

        private IEnumerable<LinkResource> BuildLinks(ManufacturingSkill manufacturingSkill)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(manufacturingSkill) };
        }
    }
}
