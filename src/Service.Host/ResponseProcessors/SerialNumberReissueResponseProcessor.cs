namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.SerialNumberReissue;

    public class SerialNumberReissueResponseProcessor : JsonResponseProcessor<SerialNumberReissue>
    {
        public SerialNumberReissueResponseProcessor(IResourceBuilder<SerialNumberReissue> resourceBuilder) 
            : base(resourceBuilder, "serial-number-issue", 1)
        {
        }
    }
}
