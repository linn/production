namespace Linn.Production.Domain.Tests.PartsReportSpecs
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Domain.LinnApps.ViewModels;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected PartsReportService Sut { get; set; }

        protected IRepository<PartFail, int> PartFailLogRepository { get; private set; }

        protected IQueryRepository<EmployeeDepartmentView> EmployeeDepartmentViewRepository { get; private set; }

        protected IReportingHelper ReportingHelper { get; private set; }

        protected List<PartFail> PartFailLogs { get; private set; }

        protected List<Part> Parts { get; private set; }

        protected ILinnWeekPack LinnWeekPack { get; private set; }

        protected IRepository<Supplier, int> SupplierRepository { get; private set; }

        protected IRepository<PurchaseOrder, int> PurchaseOrderRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.PartFailLogRepository = Substitute.For<IRepository<PartFail, int>>();
            this.EmployeeDepartmentViewRepository = Substitute.For<IQueryRepository<EmployeeDepartmentView>>();
            this.ReportingHelper = new ReportingHelper();
            this.LinnWeekPack = Substitute.For<ILinnWeekPack>();
            this.SupplierRepository = Substitute.For<IRepository<Supplier, int>>();
            this.PurchaseOrderRepository = Substitute.For<IRepository<PurchaseOrder, int>>();

            this.Sut = new PartsReportService(
                this.PartFailLogRepository,
                this.EmployeeDepartmentViewRepository,
                this.ReportingHelper,
                this.LinnWeekPack,
                this.SupplierRepository,
                this.PurchaseOrderRepository);

            this.Parts = new List<Part>
                             {
                                 new Part { PartNumber = "PART1", BaseUnitPrice = 50, PreferredSupplier = 123 },
                                 new Part { PartNumber = "PART2", BaseUnitPrice = 100, PreferredSupplier = 321 }
                             };

            this.PartFailLogs = new List<PartFail>
                                    {
                                        new PartFail
                                            {
                                                Batch = "BATCH",
                                                DateCreated = new DateTime(2019, 10, 28),
                                                EnteredBy = new Employee { Id = 33067, FullName = "name" },
                                                Quantity = 1,
                                                FaultCode = new PartFailFaultCode { FaultCode = "CODE1" },
                                                ErrorType = new PartFailErrorType { ErrorType = "TYPE1" },
                                                Id = 0,
                                                MinutesWasted = 1,
                                                Story = "STORY",
                                                Part = new Part
                                                           {
                                                               PartNumber = "PART1",
                                                               Description = "DESC1",
                                                               BaseUnitPrice = 3m
                                                           },
                                                NoCost = "N"
                                            },
                                        new PartFail
                                            {
                                                Batch = "BATCH",
                                                DateCreated = new DateTime(2019, 10, 28),
                                                EnteredBy = new Employee { Id = 33067 },
                                                Quantity = 1,
                                                FaultCode = new PartFailFaultCode { FaultCode = "CODE1" },
                                                ErrorType = new PartFailErrorType { ErrorType = "TYPE1" },
                                                Id = 1,
                                                MinutesWasted = 1,
                                                Story = "STORY",
                                                Part = new Part
                                                           {
                                                               PartNumber = "PART2",
                                                               Description = "DESC2",
                                                               BaseUnitPrice = 4m
                                                           },
                                                NoCost = "N"
                                            },
                                        new PartFail
                                            {
                                                Batch = "BATCH",
                                                DateCreated = new DateTime(2019, 10, 28),
                                                EnteredBy = new Employee { Id = 33067, FullName = "name" },
                                                Quantity = 1,
                                                FaultCode = new PartFailFaultCode { FaultCode = "CODE2" },
                                                ErrorType = new PartFailErrorType { ErrorType = "TYPE1" },
                                                Id = 2,
                                                MinutesWasted = 1,
                                                Story = "STORY",
                                                Part = new Part
                                                           {
                                                               PartNumber = "PART3",
                                                               Description = "DESC3",
                                                               BaseUnitPrice = 5m
                                                           },
                                                NoCost = "Y"
                                            },
                                        new PartFail
                                            {
                                                Batch = "BATCH",
                                                DateCreated = new DateTime(2019, 10, 28),
                                                EnteredBy = new Employee { Id = 33067, FullName = "name" },
                                                Quantity = 1,
                                                FaultCode = new PartFailFaultCode { FaultCode = "CODE1" },
                                                ErrorType = new PartFailErrorType { ErrorType = "TYPE2" },
                                                Id = 3,
                                                MinutesWasted = 1,
                                                Story = "STORY",
                                                Part = new Part
                                                           {
                                                               PartNumber = "PART4",
                                                               Description = "DESC4",
                                                               BaseUnitPrice = 6m
                                                           },
                                                NoCost = "Y"
                                            }
                                    };
        }
    }
}
