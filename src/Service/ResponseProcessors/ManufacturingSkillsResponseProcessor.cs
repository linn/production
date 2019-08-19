namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class ManufacturingSkillsResponseProcessor : JsonResponseProcessor<IEnumerable<ManufacturingSkill>>
    {
        public ManufacturingSkillsResponseProcessor(IResourceBuilder<IEnumerable<ManufacturingSkill>> resourceBuilder)
            : base(resourceBuilder, "manufacturing-skills", 1)
        {
        }
    }
}
