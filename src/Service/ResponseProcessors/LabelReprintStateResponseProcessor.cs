namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    using Nancy.Responses.Negotiation;

    public class LabelReprintStateResponseProcessor : JsonResponseProcessor<ResponseModel<LabelReprint>>
    {
        public LabelReprintStateResponseProcessor(IResourceBuilder<ResponseModel<LabelReprint>> resourceBuilder)
            : base(resourceBuilder, new List<MediaRange> { new MediaRange("application/vnd.linn.application-state+json;version=1") })
        {
        }
    }
}