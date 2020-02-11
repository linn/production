namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class LabelPrintResponseResourceBuilder : IResourceBuilder<LabelPrintResponse>
    {
        public LabelPrintResponseResource Build(LabelPrintResponse response)
        {
            return new LabelPrintResponseResource
            {
                Message = response.Message
            };
        }

        public string GetLocation(LabelPrintResponse response)
        {
            return $"production/maintenance/labels/print";
        }

        object IResourceBuilder<LabelPrintResponse>.Build(LabelPrintResponse response) => this.Build(response);

        private IEnumerable<LinkResource> BuildLinks(LabelPrintResponse address)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(address) };
        }
    }
}
