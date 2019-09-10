namespace Linn.Production.Domain.LinnApps.WorksOrders
{
    using System;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.RemoteServices;

    public class WorksOrderFactory : IWorksOrderFactory
    {
        private IWorksOrderProxyService worksOrderProxyService;

        // TODO remove this
        private IRepository<WorksOrder, int> worksOrderRepository;

        private IRepository<Part, int> partsRepository;

        private IRepository<WorkStation, string> workStationRepository;

        private IRepository<ProductionTriggerLevel, string> productionTriggerLevelsRepository;

        private ISernosPack sernosPack;

        public WorksOrderFactory(
            IWorksOrderProxyService worksOrderProxyService,
            IRepository<WorksOrder, int> worksOrderRepository,
            IRepository<Part, int> partsRepository,
            IRepository<WorkStation, string> workStationRepository,
            IRepository<ProductionTriggerLevel, string> productionTriggerLevelsRepository,
            ISernosPack sernosPack)
        {
            this.worksOrderProxyService = worksOrderProxyService;
            this.worksOrderRepository = worksOrderRepository;
            this.partsRepository = partsRepository;
            this.workStationRepository = workStationRepository;
            this.productionTriggerLevelsRepository = productionTriggerLevelsRepository;
            this.sernosPack = sernosPack;
        }

        public WorksOrder RaiseWorksOrder(WorksOrder worksOrder)
        {
            var partNumber = worksOrder.PartNumber;
            var raisedByDepartment = worksOrder.RaisedByDepartment;

            worksOrder.DateRaised = DateTime.UtcNow;

            var part = this.partsRepository.FindBy(p => p.PartNumber == partNumber);

            if (part?.BomType == null)
            {
                throw new InvalidWorksOrderException($"No matching part found for Part Number {partNumber}");
            }

            if (part.IsPhantomPart())
            {
                throw new InvalidWorksOrderException($"Cannot raise a works order for phantom part {partNumber}");
            }

            // TODO still don't know where in the code this is used...
            var batchNumber2 = this.worksOrderProxyService.GetNextBatch(partNumber);

            if (this.RebuildPart(partNumber))
            {
                throw new InvalidWorksOrderException($"Use Works Order Rebuild Utility for this part {partNumber}");
            }

            if (part.AccountingCompany == "LINN")
            {
                var canRaiseWorksOrder = this.worksOrderProxyService.CanRaiseWorksOrder(partNumber);

                if (canRaiseWorksOrder != "SUCCESS")
                {
                    throw new InvalidWorksOrderException(canRaiseWorksOrder);
                }

                var productionTriggerLevel = this.productionTriggerLevelsRepository.FindById(partNumber);

                if (productionTriggerLevel.WsName != worksOrder.WorkStationCode)
                {
                    throw new InvalidWorksOrderException($"{worksOrder.WorkStationCode} is not a possible work station for {partNumber}");
                }

                var department = this.worksOrderProxyService.GetDepartment(partNumber, raisedByDepartment);

                if (department != "SUCCESS")
                {
                    throw new InvalidWorksOrderException(department);
                }

                worksOrder.RaisedByDepartment = raisedByDepartment;
                
                return worksOrder;
            }

            worksOrder.RaisedByDepartment = "PIK ASSY";

            return worksOrder;
        }

        public WorksOrder CancelWorksOrder(WorksOrder worksOrder, int? cancelledBy, string reasonCancelled)
        {
            if (cancelledBy == null || string.IsNullOrEmpty(reasonCancelled))
            {
                throw new InvalidWorksOrderException("You must provide a user number and reason when cancelling a works order");
            }

            worksOrder.CancelWorksOrder((int)cancelledBy, reasonCancelled, DateTime.UtcNow);

            return worksOrder;
        }

        public void IssueSerialNumber(
            string partNumber,
            int orderNumber,
            string docType,
            int createdBy,
            int quantity)
        {
            if (!this.worksOrderProxyService.ProductIdOnChip(partNumber))
            {
                if (this.sernosPack.SerialNumbersRequired(partNumber))
                {
                    this.sernosPack.IssueSernos(orderNumber, docType, 0, partNumber, createdBy, quantity, null);
                }
            }
        }

        private bool RebuildPart(string partNumber)
        {
            var partNumbers = new string[] { "ASAKA/R", "TROIKA/R", "ARKIV/R", "KARMA/R", "KLYDE/R", "NEW ARKIV/R" };

            return partNumbers.Contains(partNumber);
        }
    }
}