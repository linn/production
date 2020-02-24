namespace Linn.Production.Domain.LinnApps.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class PurchaseOrderDomainService : IPurchaseOrderDomainService
    {
        private static readonly List<string> PurchaseOrderDocTypes = new List<string> { "PO", "P", "RO" };

        private readonly IQueryRepository<SernosIssued> sernosIssuedRepository;

        private readonly IQueryRepository<SernosBuilt> sernosBuiltRepository;

        private readonly IQueryRepository<PurchaseOrdersReceived> purchasedOrdersReceived;

        private readonly ISernosPack sernosPack;

        public PurchaseOrderDomainService(
            IQueryRepository<SernosIssued> sernosIssuedRepository,
            IQueryRepository<SernosBuilt> sernosBuiltRepository,
            ISernosPack sernosPack,
            IQueryRepository<PurchaseOrdersReceived> purchasedOrdersReceived)
        {
            this.sernosBuiltRepository = sernosBuiltRepository;
            this.sernosIssuedRepository = sernosIssuedRepository;
            this.purchasedOrdersReceived = purchasedOrdersReceived;
            this.sernosPack = sernosPack;
        }

        public PurchaseOrderWithSernosInfo BuildPurchaseOrderWithSernosInfo(PurchaseOrder purchaseOrder)
        {
            var detailsWithSernosInfo = new List<PurchaseOrderDetailWithSernosInfo>();
            PurchaseOrderWithSernosInfo purchaseOrderWithSernosInfo = new PurchaseOrderWithSernosInfo
                                                                          {
                                                                              OrderAddress = purchaseOrder.OrderAddress,
                                                                              OrderAddressId = purchaseOrder.OrderAddressId,
                                                                              OrderNumber = purchaseOrder.OrderNumber,
                                                                              DocumentType = purchaseOrder.DocumentType,
                                                                              DateOfOrder = purchaseOrder.DateOfOrder,
                                                                              Remarks = purchaseOrder.Remarks,
                                                                          };

            foreach (var detail in purchaseOrder.Details)
            {
                var sernosGroup = this.sernosBuiltRepository.FilterBy(s => s.ArticleNumber == detail.PartNumber)
                    .ToList().FirstOrDefault()?.SernosGroup;

                var orderNumber = detail.OrderNumber;
                var part = detail.PartNumber;

                var detailWithSernosInfo =
                    new PurchaseOrderDetailWithSernosInfo(detail)
                        {
                            NumberOfSernos = this.sernosPack.GetNumberOfSernos(part)
                        };

                var sernos = this.sernosIssuedRepository.FilterBy(
                    s => PurchaseOrderDocTypes.Contains(s.DocumentType)
                         && s.DocumentNumber == purchaseOrder.OrderNumber);

                var firstSernos = sernos.Any() ? sernos.Min(s => s.SernosNumber) : (int?)null;

                var lastSernos = sernos.Any() ? sernos.Max(s => s.SernosNumber) : (int?)null;

                detailWithSernosInfo.SernosIssued = this.sernosIssuedRepository.FilterBy(
                        s => PurchaseOrderDocTypes.Contains(s.DocumentType) && s.DocumentNumber == orderNumber)
                    .Count();

                detailWithSernosInfo.QuantityReceived = this.purchasedOrdersReceived
                    .FilterBy(p => p.OrderNumber == orderNumber && p.OrderLine == detail.OrderLine).ToList().FirstOrDefault()
                    ?.QuantityNetReceived;

                detailWithSernosInfo.SernosBuilt = this.sernosBuiltRepository.FilterBy(
                    s => s.SernosGroup == sernosGroup
                         && s.SernosNumber >= firstSernos
                         && s.SernosNumber <= lastSernos
                         && s.ArticleNumber == part).Count();

                detailWithSernosInfo.FirstSernos = firstSernos;

                detailWithSernosInfo.LastSernos = lastSernos;

                detailsWithSernosInfo.Add(detailWithSernosInfo);
            }

            purchaseOrderWithSernosInfo.DetaisWithSernosInfo = detailsWithSernosInfo;

            return purchaseOrderWithSernosInfo;
        }
    }
}