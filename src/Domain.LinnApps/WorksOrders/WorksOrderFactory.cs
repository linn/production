namespace Linn.Production.Domain.LinnApps.WorksOrders
{
    using System;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.RemoteServices;

    public class WorksOrderFactory : IWorksOrderFactory
    {
        private readonly IWorksOrderProxyService worksOrderProxyService;

        private readonly IRepository<Part, string> partsRepository;

        private readonly IRepository<ProductionTriggerLevel, string> productionTriggerLevelsRepository;

        private readonly IWorksOrderUtilities worksOrderUtilities;

        public WorksOrderFactory(
            IWorksOrderProxyService worksOrderProxyService,
            IRepository<Part, string> partsRepository,
            IRepository<ProductionTriggerLevel, string> productionTriggerLevelsRepository,
            IWorksOrderUtilities worksOrderUtilities)
        {
            this.worksOrderProxyService = worksOrderProxyService;
            this.partsRepository = partsRepository;
            this.productionTriggerLevelsRepository = productionTriggerLevelsRepository;
            this.worksOrderUtilities = worksOrderUtilities;
        }

        public WorksOrder RaiseWorksOrder(WorksOrder worksOrderToBeRaised)
        {
            var partNumber = worksOrderToBeRaised.PartNumber;
            var raisedByDepartment = worksOrderToBeRaised.RaisedByDepartment;

            worksOrderToBeRaised.DateRaised = DateTime.UtcNow;

            var part = this.partsRepository.FindBy(p => p.PartNumber == partNumber);

            if (part?.BomType == null)
            {
                throw new InvalidWorksOrderException($"No matching part found for Part Number {partNumber}");
            }

            if (part.IsPhantomPart())
            {
                throw new InvalidWorksOrderException($"Cannot raise a works order for phantom part {partNumber}");
            }

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

                if (productionTriggerLevel.WsName != worksOrderToBeRaised.WorkStationCode)
                {
                    throw new InvalidWorksOrderException($"{worksOrderToBeRaised.WorkStationCode} is not a possible work station for {partNumber}");
                }

                this.worksOrderUtilities.GetDepartment(partNumber);

                worksOrderToBeRaised.RaisedByDepartment = raisedByDepartment;
                
                return worksOrderToBeRaised;
            }

            worksOrderToBeRaised.RaisedByDepartment = "PIK ASSY";

            return worksOrderToBeRaised;
        }

        private bool RebuildPart(string partNumber)
        {
            var partNumbers = new string[] { "ASAKA/R", "TROIKA/R", "ARKIV/R", "KARMA/R", "KLYDE/R", "NEW ARKIV/R" };

            return partNumbers.Contains(partNumber);
        }
    }
}