namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class LabelPrintResourceBuilder : IResourceBuilder<LabelPrint>
    {
        public LabelPrintResource Build(LabelPrint model)
        {
            return new LabelPrintResource
                       {
                         LabelType = model.LabelType,
                         LinesForPrinting = model.LinesForPrinting,
                         Printer = model.Printer,
                         Quantity = model.Quantity,
                         Links = this.BuildLinks(model).ToArray()
                       };
        }

        object IResourceBuilder<LabelPrint>.Build(LabelPrint labelPrint) => this.Build(labelPrint);

        public string GetLocation(LabelPrint l)
        {
            return "/production/maintenance/labels/print";
        }

        private IEnumerable<LinkResource> BuildLinks(LabelPrint model)
        {
                yield return new LinkResource { Rel = "self", Href = this.GetLocation(model) };
        }
    }
}