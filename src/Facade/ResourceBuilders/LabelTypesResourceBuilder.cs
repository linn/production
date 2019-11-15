namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class LabelTypesResourceBuilder : IResourceBuilder<IEnumerable<LabelType>>
    {
        private readonly LabelTypeResourceBuilder labelTypeResourceBuilder = new LabelTypeResourceBuilder();

        public IEnumerable<LabelTypeResource> Build(IEnumerable<LabelType> labelTypes)
        {
            return labelTypes
                .OrderBy(b => b.LabelTypeCode)
                .Select(a => this.labelTypeResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<LabelType>>.Build(IEnumerable<LabelType> labelTypes) => this.Build(labelTypes);

        public string GetLocation(IEnumerable<LabelType> labelTypes)
        {
            throw new NotImplementedException();
        }
    }
}
