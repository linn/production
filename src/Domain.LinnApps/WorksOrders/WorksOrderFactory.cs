namespace Linn.Production.Domain.LinnApps.WorksOrders
{
    using System;
    using System.Linq;

    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.PCAS;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class WorksOrderFactory : IWorksOrderFactory
    {
        private readonly IWorksOrderProxyService worksOrderProxyService;

        private readonly IRepository<Part, string> partsRepository;

        private readonly IRepository<ProductionTriggerLevel, string> productionTriggerLevelsRepository;

        private readonly IRepository<PcasBoardForAudit, string> pcasBoardsForAuditRepository;

        private readonly IRepository<PcasRevision, string> pcasRevisionsRepository;

        private readonly IRepository<Department, string> departmentRepository;

        private readonly IRepository<Cit, string> citRepository;

        private ISernosPack sernosPack;

        public WorksOrderFactory(
            IWorksOrderProxyService worksOrderProxyService,
            IRepository<Part, string> partsRepository,
            IRepository<ProductionTriggerLevel, string> productionTriggerLevelsRepository,
            IRepository<PcasBoardForAudit, string> pcasBoardsForAuditRepository,
            IRepository<PcasRevision, string> pcasRevisionsRepository,
            IRepository<Department, string> departmentRepository,
            IRepository<Cit, string> citRepository,
            ISernosPack sernosPack)
        {
            this.worksOrderProxyService = worksOrderProxyService;
            this.partsRepository = partsRepository;
            this.productionTriggerLevelsRepository = productionTriggerLevelsRepository;
            this.pcasBoardsForAuditRepository = pcasBoardsForAuditRepository;
            this.pcasRevisionsRepository = pcasRevisionsRepository;
            this.departmentRepository = departmentRepository;
            this.citRepository = citRepository;
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

                this.GetDepartment(partNumber);

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

        private void GetDepartment(string partNumber)
        {
            var productionTriggerLevel = this.productionTriggerLevelsRepository.FindById(partNumber);

            var cit = this.citRepository.FindById(productionTriggerLevel.CitCode);

            var department = this.departmentRepository.FindById(cit.DepartmentCode);

            if (department == null)
            {
                throw new InvalidWorksOrderException($"Department code not found for CIT {cit.Code}");
            }
        }

        private bool RebuildPart(string partNumber)
        {
            var partNumbers = new string[] { "ASAKA/R", "TROIKA/R", "ARKIV/R", "KARMA/R", "KLYDE/R", "NEW ARKIV/R" };

            return partNumbers.Contains(partNumber);
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