namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.Measures;

    public class PartFailResponseProcessor : JsonResponseProcessor<PartFail>
    {
        public PartFailResponseProcessor(IResourceBuilder<PartFail> resourceBuilder)
            : base(resourceBuilder, "part-fail", 1)
        {
        }
    }
}