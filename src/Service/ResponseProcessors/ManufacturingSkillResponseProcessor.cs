namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class ManufacturingSkillResponseProcessor : JsonResponseProcessor<ManufacturingSkill>
    {
        public ManufacturingSkillResponseProcessor(IResourceBuilder<ManufacturingSkill> resourceBuilder)
            : base(resourceBuilder, "manufacturing-skill", 1)
        {
        }
    }
}
