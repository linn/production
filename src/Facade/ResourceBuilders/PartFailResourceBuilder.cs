namespace Linn.Production.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
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
                           Quantity = model.Quantity,
                           Batch = model.Batch,
                           FaultCode = model.FaultCode.FaultCode,
                           FaultDescription = model.FaultCode.Description,
                           PurchaseOrderNumber = model.PurchaseOrderNumber,
                           ErrorType = model.ErrorType.ErrorType,
                           StoragePlace = model.StorageLocation.LocationCode,
                           Story = model.Story
                       };
        }

        object IResourceBuilder<PartFail>.Build(PartFail fail) => this.Build(fail);

        public string GetLocation(PartFail model)
        {
            throw new System.NotImplementedException();
        }
    }
}