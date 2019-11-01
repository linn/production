namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public class WorksOrderLabelResourceBuilder : IResourceBuilder<WorksOrderLabel>
    {
        public WorksOrderLabelResource Build(WorksOrderLabel model)
        {
            return new WorksOrderLabelResource
                       {
                           PartNumber = model.PartNumber, Sequence = model.Sequence, LabelText = model.LabelText,
                           Links = this.BuildLinks(model).ToArray()
                       };
        }

        public string GetLocation(WorksOrderLabel model)
        {
            return $"/production/works-orders/labels/{model.PartNumber}/{model.Sequence}";
        }

        object IResourceBuilder<WorksOrderLabel>.Build(WorksOrderLabel worksOrder) => this.Build(worksOrder);

        private IEnumerable<LinkResource> BuildLinks(WorksOrderLabel worksOrder)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(worksOrder) };
        }
    }
}