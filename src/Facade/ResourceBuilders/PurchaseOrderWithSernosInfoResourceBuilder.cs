namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Resources;

    public class PurchaseOrderWithSernosInfoResourceBuilder : IResourceBuilder<PurchaseOrderWithSernosInfo>
    {
        public PurchaseOrderWithSernosInfoResource Build(PurchaseOrderWithSernosInfo model)
        {
            return new PurchaseOrderWithSernosInfoResource
                       {
                            OrderNumber = model.OrderNumber,
                            DateOfOrder = model.DateOfOrder.ToString("o"),
                            Addressee = model.OrderAddress.Addressee,
                            Address1 = model.OrderAddress.Address1,
                            Address2 = model.OrderAddress.Address2,
                            Address3 = model.OrderAddress.Address3,
                            Address4 = model.OrderAddress.Address4,
                            PostCode = model.OrderAddress.PostCode,
                            DetailSernosInfos = model.DetaisWithSernosInfo.Select(x => new PurchaseOrderDetailSWithernosInfoResource
                                                                                           {
                                                                                                OrderLine = x.OrderLine,
                                                                                                PartNumber = x.PartNumber,
                                                                                                PartDescription = x.Part.Description,
                                                                                                OrderQuantity = x.OrderQuantity,
                                                                                                OurUnitOfMeasure = x.OurUnitOfMeasure,
                                                                                                IssuedSerialNumbers = x.IssuedSerialNumbers,
                                                                                                FirstSernos = x.FirstSernos,
                                                                                                LastSernos = x.LastSernos,
                                                                                                SernosBuilt = x.SernosBuilt,
                                                                                                SernosIssued = x.SernosIssued,
                                                                                                QuantityReceived = x.QuantityReceived
                                                                                           }).ToList()
                       };
        }

        object IResourceBuilder<PurchaseOrderWithSernosInfo>.Build(PurchaseOrderWithSernosInfo s) => this.Build(s);

        public string GetLocation(PurchaseOrderWithSernosInfo model)
        {
            throw new System.NotImplementedException();
        }
    }
}