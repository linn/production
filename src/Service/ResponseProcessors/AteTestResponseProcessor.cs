namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.ATE;

    public class AteTestResponseProcessor : JsonResponseProcessor<AteTest>
    {
        public AteTestResponseProcessor(IResourceBuilder<AteTest> resourceBuilder)
            : base(resourceBuilder, "ate-test", 1)
        {
        }
    }
}