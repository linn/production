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
                         LinesForPrinting = new LabelPrintContentsResource
                                                {
                             SupplierId = model.LinesForPrinting.SupplierId,
                             Addressee = model.LinesForPrinting.Addressee,
                             Addressee2 = model.LinesForPrinting.Addressee2,
                             AddressId = model.LinesForPrinting.AddressId,
                             Line1 = model.LinesForPrinting.Line1,
                             Line2 = model.LinesForPrinting.Line2,
                             Line3 = model.LinesForPrinting.Line3,
                             Line4 = model.LinesForPrinting.Line4,
                             Line5 = model.LinesForPrinting.Line5,
                             Line6 = model.LinesForPrinting.Line6,
                             Line7 = model.LinesForPrinting.Line7,
                             PostalCode = model.LinesForPrinting.PostalCode,
                             Country = model.LinesForPrinting.Country,
                             FromPCNumber = model.LinesForPrinting.FromPCNumber,
                             ToPCNumber = model.LinesForPrinting.ToPCNumber,
                             PoNumber = model.LinesForPrinting.PoNumber,
                             PartNumber = model.LinesForPrinting.PartNumber,
                             Qty = model.LinesForPrinting.Qty,
                             Initials = model.LinesForPrinting.Initials,
                             Date = model.LinesForPrinting.Date
                         },
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