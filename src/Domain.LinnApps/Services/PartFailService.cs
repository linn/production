﻿namespace Linn.Production.Domain.LinnApps.Services
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    public class PartFailService : IPartFailService
    {
        private readonly IRepository<WorksOrder, int> worksOrderRepository;

        private readonly IRepository<PurchaseOrder, int> purchaseOrderRepository;

        private readonly IRepository<Part, string> partRepository;

        public PartFailService(
            IRepository<WorksOrder, int> worksOrderRepository,
            IRepository<PurchaseOrder, int> purchaseOrderRepository,
            IRepository<Part, string> partRepository)
        {
            this.worksOrderRepository = worksOrderRepository;
            this.partRepository = partRepository;
            this.purchaseOrderRepository = purchaseOrderRepository;
        }

        public PartFail Create(PartFail candidate)
        {
            if (candidate.WorksOrder != null)
            {
                var worksOrder = this.worksOrderRepository.FindById(candidate.WorksOrder.OrderNumber)
                                 ?? throw new InvalidWorksOrderException("Invalid Works Order Number supplied.");
                candidate.WorksOrder = worksOrder;
            }

            if (candidate.Part != null)
            {
                var part = this.partRepository.FindById(candidate.Part.PartNumber)
                    ?? throw new InvalidPartNumberException("Invalid Part Number Supplied");
                candidate.Part = part;
            }

            if (candidate.PurchaseOrderNumber != null)
            {
                var purchaseOrder = this.purchaseOrderRepository.FindById((int)candidate.PurchaseOrderNumber) 
                    ?? throw new InvalidPurchaseOrderException("Invalid Purchase Order Number Supplied");

                if (!purchaseOrder.ContainsPart(candidate.Part?.PartNumber))
                {
                    throw new InvalidPurchaseOrderException("Part Number supplied does not match Purchase Order");
                }
            }

            if (candidate.Story?.Length > 200)
            {
                throw new PartFailException(
                    $"Story cannot be longer than 200 characters (you entered {candidate.Story.Length} characters)");
            }

            return candidate;
        }
    }
}
