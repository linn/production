using Linn.Production.Domain.LinnApps;

namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Resources;

    public class ManufacturingSkillsResourceBuilder : IResourceBuilder<IEnumerable<ManufacturingSkill>>
    {
        private readonly ManufacturingSkillResourceBuilder manufacturingSkillResourceBuilder = new ManufacturingSkillResourceBuilder();

        public IEnumerable<ManufacturingSkillResource> Build(IEnumerable<ManufacturingSkill> manufacturingSkills)
        {
            return manufacturingSkills
                .OrderBy(b => b.SkillCode)
                .Select(a => this.manufacturingSkillResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<ManufacturingSkill>>.Build(IEnumerable<ManufacturingSkill> manufacturingSkills) => this.Build(manufacturingSkills);

        public string GetLocation(IEnumerable<ManufacturingSkill> manufacturingSkills)
        {
            throw new NotImplementedException();
        }
    }
}