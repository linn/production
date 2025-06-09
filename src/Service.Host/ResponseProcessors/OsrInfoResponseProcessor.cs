namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.Measures;

    public class OsrInfoResponseProcessor : JsonResponseProcessor<OsrInfo>
    {
        public OsrInfoResponseProcessor(IResourceBuilder<OsrInfo> resourceBuilder)
            : base(resourceBuilder, "osr-information", 1)
        {
        }
    }
}