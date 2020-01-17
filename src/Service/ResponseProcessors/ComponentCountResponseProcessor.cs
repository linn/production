namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.Measures;

    public class ComponentCountResponseProcessor : JsonResponseProcessor<ComponentCount>
    {
        public ComponentCountResponseProcessor(IResourceBuilder<ComponentCount> resourceBuilder)
            : base(resourceBuilder, "component-count", 1)
        {
        }
    }
}