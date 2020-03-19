namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class MechPartSourceResponseProcessor : JsonResponseProcessor<MechPartSource>
    {
        public MechPartSourceResponseProcessor(IResourceBuilder<MechPartSource> resourceBuilder)
            : base(resourceBuilder, "mech-part-source", 1)
        {
        }
    }
}