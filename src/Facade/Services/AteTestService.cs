﻿namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Proxy;
    using Linn.Production.Resources;

    public class AteTestService : FacadeService<AteTest, int, AteTestResource, AteTestResource>
    {
        private readonly IRepository<WorksOrder, int> worksOrderRepository;

        private readonly IRepository<Employee, int> employeeRepository;

        private readonly IFacadeService<AteTestDetail, AteTestDetailKey, AteTestDetailResource, AteTestDetailResource>
            detailService;

        private readonly IDatabaseService databaseService;

        private readonly IRepository<PcasRevision, string> pcasRevisionRepository;

        public AteTestService(
            IRepository<AteTest, int> repository,
            ITransactionManager transactionManager,
            IRepository<WorksOrder, int> worksOrderRepository,
            IRepository<Employee, int> employeeRepository,
            IRepository<PcasRevision, string> pcasRevisionRepository,
            IDatabaseService databaseService,
            IFacadeService<AteTestDetail, AteTestDetailKey, AteTestDetailResource, AteTestDetailResource> detailService)
            : base(repository, transactionManager)
        {
            this.worksOrderRepository = worksOrderRepository;
            this.employeeRepository = employeeRepository;
            this.detailService = detailService;
            this.databaseService = databaseService;
            this.pcasRevisionRepository = pcasRevisionRepository;
        }

        protected override AteTest CreateFromResource(AteTestResource resource)
        {
            var worksOrder = this.worksOrderRepository.FindById(resource.WorksOrderNumber);
            var id = this.databaseService.GetNextVal("ATE_TESTS_SEQ");
            var details = new List<AteTestDetail>();
            var itemNo = 1;
            foreach (var detail in resource.Details)
            {
                detail.TestId = id;
                details.Add(new AteTestDetail
                                {
                                    AteTestFaultCode = detail.AteTestFaultCode,
                                    AoiEscape = detail.AoiEscape,
                                    BatchNumber = detail.BatchNumber.ToString(),
                                    BoardFailNumber = detail.BoardFailNumber,
                                    NumberOfFails = detail.NumberOfFails,
                                    BoardSerialNumber = detail.BoardSerialNumber,
                                    CircuitRef = detail.CircuitRef,
                                    PartNumber = GetDetailPart(detail.CircuitRef, resource.PartNumber, this.pcasRevisionRepository),
                                    Comments = detail.Comments,
                                    CorrectiveAction = detail.CorrectiveAction,
                                    ItemNumber = itemNo,
                                    Machine = detail.Machine,
                                    Shift = detail.Shift,
                                    SmtOrPcb = detail.SmtOrPcb,
                                    PcbOperator = detail.PcbOperator.HasValue ?
                                        this.employeeRepository.FindById(detail.PcbOperator.Value) : null
                                });
                itemNo++;
            }

            return new AteTest
                       {
                            TestId = id,
                            User = this.employeeRepository.FindById(resource.UserNumber),
                            DateTested = resource.DateTested != null
                                            ? DateTime.Parse(resource.DateTested)
                                            : (DateTime?)null,
                           WorksOrder = worksOrder,
                           NumberTested = resource.NumberTested,
                           NumberOfSmtComponents = resource.NumberOfSmtComponents,
                           NumberOfSmtFails = resource.NumberOfSmtFails,
                           NumberOfPcbComponents = resource.NumberOfPcbComponents,
                           NumberOfPcbFails = resource.NumberOfPcbFails,
                           NumberOfPcbBoardFails = resource.NumberOfPcbBoardFails,
                           NumberOfSmtBoardFails = resource.NumberOfSmtBoardFails,
                           PcbOperator = this.employeeRepository.FindById(resource.PcbOperator),
                           MinutesSpent = resource.MinutesSpent,
                           Machine = resource.Machine,
                           PlaceFound = resource.PlaceFound,
                           DateInvalid = resource.DateInvalid != null
                                             ? DateTime.Parse(resource.DateInvalid)
                                             : (DateTime?)null,
                           FlowMachine = resource.FlowMachine,
                           FlowSolderDate = resource.FlowSolderDate != null
                                                ? DateTime.Parse(resource.FlowSolderDate)
                                                : (DateTime?)null,
                           Details = details
            };
        }

        protected override void UpdateFromResource(AteTest entity, AteTestResource updateResource)
        {
            entity.PcbOperator = this.employeeRepository.FindById(updateResource.PcbOperator);
            entity.NumberTested = updateResource.NumberTested;
            entity.DateTested = updateResource.DateTested != null
                                    ? DateTime.Parse(updateResource.DateTested)
                                    : (DateTime?)null;
            entity.FlowSolderDate = updateResource.FlowSolderDate != null
                                    ? DateTime.Parse(updateResource.DateTested)
                                    : (DateTime?)null;
            entity.NumberOfPcbBoardFails = updateResource.NumberOfPcbBoardFails;
            entity.NumberOfSmtBoardFails = updateResource.NumberOfSmtBoardFails;
            entity.NumberOfPcbComponents = updateResource.NumberOfPcbComponents;
            entity.NumberOfSmtFails = updateResource.NumberOfSmtFails;
            entity.NumberOfPcbFails = updateResource.NumberOfPcbFails;
            entity.MinutesSpent = updateResource.MinutesSpent;
            entity.FlowMachine = updateResource.FlowMachine;
            entity.Machine = updateResource.Machine;
            entity.PlaceFound = updateResource.PlaceFound;

            foreach (var detail in updateResource.Details)
            {
                detail.PartNumber = GetDetailPart(detail.CircuitRef, updateResource.PartNumber, this.pcasRevisionRepository);
                if (detail.ItemNumber == null || entity.Details.All(d => d.ItemNumber != detail.ItemNumber))
                {
                    detail.TestId = entity.TestId;
                    this.detailService.Add(detail);
                }
                else
                {
                    this.detailService.Update(new AteTestDetailKey { ItemNumber = (int)detail.ItemNumber, TestId = detail.TestId }, detail);
                }
            }
        }

        protected override Expression<Func<AteTest, bool>> SearchExpression(string searchTerm)
        {
            return test => test.WorksOrder.OrderNumber == int.Parse(searchTerm);
        }

        private static string GetDetailPart(string cref, string pcas, IRepository<PcasRevision, string> repo)
        {
            if (cref == null)
            {
                return null;
            }

            return repo.FindAll().Where(p => p.PcasPartNumber == pcas && p.Cref == cref).ToList().FirstOrDefault()?.PartNumber;
        }
    }
}