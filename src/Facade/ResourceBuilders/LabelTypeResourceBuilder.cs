namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class LabelTypeResourceBuilder : IResourceBuilder<LabelType>
    {
        public LabelTypeResource Build(LabelType labelType)
        {
            return new LabelTypeResource
            {
                LabelTypeCode = labelType.LabelTypeCode,
                Description = labelType.Description,
                BarcodePrefix = labelType.BarcodePrefix,
                NSBarcodePrefix = labelType.NSBarcodePrefix,
                Filename = labelType.Filename,
                DefaultPrinter = labelType.DefaultPrinter,
                CommandFilename = labelType.CommandFilename,
                TestFilename = labelType.TestFilename,
                TestPrinter = labelType.TestPrinter,
                TestCommandFilename = labelType.TestCommandFilename,
                Links = this.BuildLinks(labelType).ToArray()
            };
        }

        public string GetLocation(LabelType labelType)
        {
            return $"/production/resources/label-types/{Uri.EscapeDataString(labelType.LabelTypeCode)}";
        }

        object IResourceBuilder<LabelType>.Build(LabelType labelType) => this.Build(labelType);

        private IEnumerable<LinkResource> BuildLinks(LabelType labelType)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(labelType) };
        }
    }
}
