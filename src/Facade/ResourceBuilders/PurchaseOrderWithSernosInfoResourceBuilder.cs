namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
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
                            Address1 = model.OrderAddress.Line1,
                            Address2 = model.OrderAddress.Line2,
                            Address3 = model.OrderAddress.Line3,
                            Address4 = model.OrderAddress.Line4,
                            Remarks = model.Remarks,
                            PostCode = model.OrderAddress.PostCode,
                            Country = model.OrderAddress.Country?.Name,
                            DetailSernosInfos = model.DetaisWithSernosInfo.Select(x => new PurchaseOrderDetaiWithSernosInfoResource
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
                                                                                                QuantityReceived = x.QuantityReceived,
                                                                                                NumberOfSernos = x.NumberOfSernos
                                                                                           }).OrderBy(d => d.OrderLine).ToList()
                       };
        }

        object IResourceBuilder<PurchaseOrderWithSernosInfo>.Build(PurchaseOrderWithSernosInfo s) => this.Build(s);

        public string GetLocation(PurchaseOrderWithSernosInfo model)
        {
            throw new System.NotImplementedException();
        }
    }
}