namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public class WorksOrderLabelsResourceBuilder : IResourceBuilder<IEnumerable<WorksOrderLabel>>
    {
        private readonly WorksOrderLabelResourceBuilder resourceBuilder = new WorksOrderLabelResourceBuilder();

        public IEnumerable<WorksOrderLabelResource> Build(IEnumerable<WorksOrderLabel> labels)
        {
            return labels.Select(w => this.resourceBuilder.Build(w));
        }

        object IResourceBuilder<IEnumerable<WorksOrderLabel>>.Build(IEnumerable<WorksOrderLabel> labels) =>
            this.Build(labels);

        public string GetLocation(IEnumerable<WorksOrderLabel> model)
        {
            throw new System.NotImplementedException();
        }
    }
}