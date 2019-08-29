namespace Linn.Production.Service.ResponseProcessors
{
    using Common.Facade;
    using Common.Nancy.Facade;
    using Domain.LinnApps.Measures;

    public class OsrInfoResponseProcessor : JsonResponseProcessor<OsrInfo>
    {
        public OsrInfoResponseProcessor(IResourceBuilder<OsrInfo> resourceBuilder)
            : base(resourceBuilder)
        {
        }
    }
}