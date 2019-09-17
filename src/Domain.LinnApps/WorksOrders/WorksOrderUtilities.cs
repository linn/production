namespace Linn.Production.Domain.LinnApps.WorksOrders
{
    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.PCAS;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class WorksOrderUtilities : IWorksOrderUtilities
    {
        private readonly IRepository<Part, string> partsRepository;

        private readonly IRepository<PcasBoardForAudit, string> pcasBoardsForAuditRepository;

        private readonly IRepository<PcasRevision, string> pcasRevisionsRepository;

        private readonly IRepository<ProductionTriggerLevel, string> productionTriggerLevelsRepository;

        private readonly IWorksOrderProxyService worksOrderProxyService;

        private readonly ISernosPack sernosPack;

        public WorksOrderUtilities(
            IRepository<Part, string> partsRepository,
            IRepository<PcasBoardForAudit, string> pcasBoardsForAuditRepository,
            IRepository<PcasRevision, string> pcasRevisionsRepository,
            IRepository<ProductionTriggerLevel, string> productionTriggerLevelsRepository,
            IWorksOrderProxyService worksOrderProxyService,
            ISernosPack sernosPack)
        {
            this.partsRepository = partsRepository;
            this.pcasBoardsForAuditRepository = pcasBoardsForAuditRepository;
            this.pcasRevisionsRepository = pcasRevisionsRepository;
            this.productionTriggerLevelsRepository = productionTriggerLevelsRepository;
            this.worksOrderProxyService = worksOrderProxyService;
            this.sernosPack = sernosPack;
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

        public WorksOrderDetails GetWorksOrderDetails(string partNumber)
        {
            var part = this.partsRepository.FindById(partNumber);

            if (part == null)
            {
                throw new DomainException($"No part found for part number {partNumber}");
            }

            var auditDisclaimer = this.GetAuditDisclaimer(partNumber);

            var workStationCode = this.GetWorkStationCode(partNumber);

            return new WorksOrderDetails
                       {
                           PartNumber = partNumber,
                           PartDescription = part.Description,
                           WorkStationCode = workStationCode,
                           AuditDisclaimer = auditDisclaimer
                       };
        }

        private string GetAuditDisclaimer(string partNumber)
        {
            var pcasRevision = this.pcasRevisionsRepository.FindBy(p => p.PcasPartNumber == partNumber);

            if (pcasRevision == null)
            {
                return null;
            }

            var pcasBoardForAudit =
                this.pcasBoardsForAuditRepository.FindBy(p => p.BoardCode == pcasRevision.BoardCode);

            if (pcasBoardForAudit == null || pcasBoardForAudit.ForAudit != "Y")
            {
                return null;
            }

            if (pcasBoardForAudit.CutClinch == "Y")
            {
                return "Board requires audit. Use cut and clinch tool";
            }

            return "Board requires audit";
        }

        private string GetWorkStationCode(string partNumber)
        {
            var productionTriggerLevel = this.productionTriggerLevelsRepository.FindById(partNumber);

            return productionTriggerLevel?.WsName;
        }
    }
}