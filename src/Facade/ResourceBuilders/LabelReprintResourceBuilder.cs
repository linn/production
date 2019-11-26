namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class LabelReprintResourceBuilder : IResourceBuilder<LabelReprint>
    {
        public LabelReprintResource Build(LabelReprint model)
        {
            return new LabelReprintResource
                       {
                           DateIssued = model.DateIssued.ToString("o"),
                           LabelReprintId = model.LabelReprintId,
                           DocumentType = model.DocumentType,
                           DocumentNumber = model.DocumentNumber,
                           NewPartNumber = model.NewPartNumber,
                           Reason = model.Reason,
                           NumberOfProducts = model.NumberOfProducts,
                           LabelTypeCode = model.LabelTypeCode,
                           SerialNumber = model.SerialNumber,
                           PartNumber = model.PartNumber,
                           ReprintType = model.ReprintType,
                           Links = this.BuildLinks(model).ToArray()
                       };
        }

        object IResourceBuilder<LabelReprint>.Build(LabelReprint labelReprint) => this.Build(labelReprint);

        public string GetLocation(LabelReprint labelReprint)
        {
            return $"/production/maintenance/labels/reprint-serial-number/{labelReprint.LabelReprintId}";
        }

        private IEnumerable<LinkResource> BuildLinks(LabelReprint labelReprint)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(labelReprint) };

            yield return new LinkResource("requested-by", $"/employees/{labelReprint.RequestedBy}");
        }
    }
}