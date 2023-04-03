namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public class PartFailResourceBuilder : IResourceBuilder<PartFail>
    {
        public PartFailResource Build(PartFail model)
        {
            return new PartFailResource
                       {
                           Id = model.Id,
                           EnteredBy = model.EnteredBy.Id,
                           DateCreated = model.DateCreated.ToString("o"),
                           EnteredByName = model.EnteredBy.FullName,
                           PartNumber = model.Part.PartNumber,
                           PartDescription = model.Part.Description,
                           FaultDescription = model.FaultCode.Description,
                           Quantity = model.Quantity,
                           Batch = model.Batch,
                           FaultCode = model.FaultCode.FaultCode,
                           PurchaseOrderNumber = model.PurchaseOrderNumber,
                           ErrorType = model.ErrorType.ErrorType,
                           StoragePlace = model.StorageLocation?.LocationCode,
                           StoragePlaceDescription = model.StorageLocation?.Description,
                           Story = model.Story,
                           SerialNumber = model.SerialNumber,
                           WorksOrderNumber = model.WorksOrder?.OrderNumber,
                           MinutesWasted = model.MinutesWasted,
                           Comments = model.Comments,
                           Owner = model.Owner?.Id,
                           OwnerName = model.Owner?.FullName,
                           NoCost = model.NoCost == "Y",
                           Links = this.BuildLinks(model).ToArray()
            };
        }

        object IResourceBuilder<PartFail>.Build(PartFail fail) => this.Build(fail);

        public string GetLocation(PartFail fail)
        {
            return $"/production/quality/part-fails/{fail.Id}";
        }

        private IEnumerable<LinkResource> BuildLinks(PartFail fail)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(fail) };
        }
    }
}