namespace Linn.Production.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ManufacturingSkillResourceBuilder : IResourceBuilder<ManufacturingSkill>
    {
        public ManufacturingSkillResource Build(ManufacturingSkill ManufacturingSkill)
        {
            return new ManufacturingSkillResource
            {
                SkillCode = ManufacturingSkill.SkillCode,
                Description = ManufacturingSkill.Description,
                HourlyRate = ManufacturingSkill.HourlyRate,
                Links = this.BuildLinks(ManufacturingSkill).ToArray()
            };
        }

        public string GetLocation(ManufacturingSkill manufacturingSkill)
        {
            return $"/production/manufacturing-skills/{Uri.EscapeDataString(manufacturingSkill.SkillCode)}";
        }

        object IResourceBuilder<ManufacturingSkill>.Build(ManufacturingSkill manufacturingSkill) => this.Build(manufacturingSkill);

        private IEnumerable<LinkResource> BuildLinks(ManufacturingSkill manufacturingSkill)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(manufacturingSkill) };
        }
    }
}
