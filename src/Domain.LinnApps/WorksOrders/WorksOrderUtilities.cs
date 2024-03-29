﻿namespace Linn.Production.Domain.LinnApps.WorksOrders
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

    public class WorksOrderUtilities : IWorksOrderUtilities
    {
        private readonly IRepository<Part, string> partsRepository;

        private readonly IRepository<PcasBoardForAudit, string> pcasBoardsForAuditRepository;

        private readonly IRepository<PcasRevision, string> pcasRevisionsRepository;

        private readonly IRepository<ProductionTriggerLevel, string> productionTriggerLevelsRepository;

        private readonly IRepository<Department, string> departmentRepository;

        private readonly IRepository<Cit, string> citRepository;

        private readonly ISalesArticleService salesArticleService;

        private readonly ISernosPack sernosPack;

        private readonly IRepository<WorksOrderLabel, WorksOrderLabelKey> labelService;

        public WorksOrderUtilities(
            IRepository<Part, string> partsRepository,
            IRepository<PcasBoardForAudit, string> pcasBoardsForAuditRepository,
            IRepository<PcasRevision, string> pcasRevisionsRepository,
            IRepository<ProductionTriggerLevel, string> productionTriggerLevelsRepository,
            ISernosPack sernosPack,
            IRepository<Cit, string> citRepository,
            IRepository<Department, string> departmentRepository,
            ISalesArticleService salesArticleService,
            IRepository<WorksOrderLabel, WorksOrderLabelKey> labelService)
        {
            this.partsRepository = partsRepository;
            this.pcasBoardsForAuditRepository = pcasBoardsForAuditRepository;
            this.pcasRevisionsRepository = pcasRevisionsRepository;
            this.productionTriggerLevelsRepository = productionTriggerLevelsRepository;
            this.sernosPack = sernosPack;
            this.citRepository = citRepository;
            this.departmentRepository = departmentRepository;
            this.salesArticleService = salesArticleService;
            this.labelService = labelService;
        }

        public void IssueSerialNumber(
            string partNumber,
            int orderNumber,
            string docType,
            int createdBy,
            int quantity)
        {
            try
            {
                if (!this.salesArticleService.ProductIdOnChip(partNumber))
                {
                    if (this.sernosPack.SerialNumbersRequired(partNumber))
                    {
                        this.sernosPack.IssueSernos(orderNumber, docType, 0, partNumber, createdBy, quantity, null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new IssueSerialNumberException(
                    $"Error Issuing serial numbers. Does {partNumber} have a PRODUCT ANALYSIS CODE and SERNOS SEQUENCE set?  Message for IT: " + ex.Message, ex);
            }
        }

        public WorksOrderPartDetails GetWorksOrderDetails(string partNumber)
        {
            var part = this.partsRepository.FindById(partNumber);

            if (part == null)
            {
                throw new DomainException($"No part found for part number {partNumber}");
            }

            var auditDisclaimer = this.GetAuditDisclaimer(partNumber);

            var workStationCode = this.GetWorkStationCode(partNumber);

            var department = this.GetDepartment(partNumber);

            var quantity = this.GetQuantityToBuild(partNumber);

            return new WorksOrderPartDetails
                       {
                           PartNumber = partNumber,
                           PartDescription = part.Description,
                           WorkStationCode = workStationCode,
                           AuditDisclaimer = auditDisclaimer,
                           DepartmentCode = department.DepartmentCode,
                           DepartmentDescription = department.Description,
                           QuantityToBuild = quantity
                       };
        }

        public Department GetDepartment(string partNumber)
        {
            var productionTriggerLevel = this.productionTriggerLevelsRepository.FindById(partNumber);

            if (productionTriggerLevel == null)
            {
                throw new InvalidWorksOrderException($"Production Trigger Level code not found for part {partNumber}");
            }

            var cit = this.citRepository.FindById(productionTriggerLevel.CitCode);

            var department = this.departmentRepository.FindById(cit.DepartmentCode);

            if (department == null)
            {
                throw new InvalidWorksOrderException($"Department code not found for CIT {cit.Code}");
            }

            return department;
        }

        public int GetNextLabelSeqForPart(string partNumber)
        {
            var labels = this.labelService.FilterBy(l => l.PartNumber == partNumber);
            if (labels.Any())
            {
                return this.labelService.FilterBy(l => l.PartNumber == partNumber).Select(l => l.Sequence).Max() + 1;
            }

            return 1;
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

            return productionTriggerLevel?.WorkStationName;
        }

        private int GetQuantityToBuild(string partNumber)
        {
            var productionTriggerLevel = this.productionTriggerLevelsRepository.FindById(partNumber);

            return productionTriggerLevel?.KanbanSize ?? 0;
        }
    }
}