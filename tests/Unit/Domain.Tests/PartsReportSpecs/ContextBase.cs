namespace Linn.Production.Domain.Tests.PartsReportSpecs
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Domain.LinnApps.ViewModels;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected PartsReportService Sut { get; set; }

        protected IQueryRepository<PartFailLog> PartFailLogRepository { get; private set; }

        protected IQueryRepository<EmployeeDepartmentView> EmployeeDepartmentViewRepository { get; private set; }

        protected IReportingHelper ReportingHelper { get; private set; }

        protected List<PartFailLog> PartFailLogs { get; private set; }

        protected List<Part> Parts { get; private set; }

        protected ILinnWeekPack LinnWeekPack { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.PartFailLogRepository = Substitute.For<IQueryRepository<PartFailLog>>();
            this.EmployeeDepartmentViewRepository = Substitute.For<IQueryRepository<EmployeeDepartmentView>>();
            this.ReportingHelper = new ReportingHelper();
            this.LinnWeekPack = Substitute.For<ILinnWeekPack>();

            this.Sut = new PartsReportService(
                this.PartFailLogRepository,
                this.EmployeeDepartmentViewRepository,
                this.ReportingHelper,
                this.LinnWeekPack);

            this.Parts = new List<Part>
                             {
                                 new Part { PartNumber = "PART1", BaseUnitPrice = 50, PreferredSupplier = 123 },
                                 new Part { PartNumber = "PART2", BaseUnitPrice = 100, PreferredSupplier = 321 }
                             };

            this.PartFailLogs = new List<PartFailLog>
                                    {
                                        new PartFailLog
                                            {
                                                Batch = "BATCH",
                                                DateCreated = new DateTime(2019, 10, 28),
                                                EnteredBy = 33067,
                                                PartNumber = "PART1",
                                                Quantity = 1,
                                                FaultCode = "CODE1",
                                                ErrorType = "TYPE1",
                                                Id = 0,
                                                MinutesWasted = 1,
                                                Story = "STORY",
                                                Part = new Part
                                                           {
                                                               PartNumber = "PART1",
                                                               Description = "DESC1"
                                                           }
                                            },
                                        new PartFailLog
                                            {
                                                Batch = "BATCH",
                                                DateCreated = new DateTime(2019, 10, 28),
                                                EnteredBy = 33067,
                                                PartNumber = "PART2",
                                                Quantity = 1,
                                                FaultCode = "CODE1",
                                                ErrorType = "TYPE1",
                                                Id = 1,
                                                MinutesWasted = 1,
                                                Story = "STORY",
                                                Part = new Part
                                                           {
                                                               PartNumber = "PART2",
                                                               Description = "DESC2"
                                                           }
                                            },
                                        new PartFailLog
                                            {
                                                Batch = "BATCH",
                                                DateCreated = new DateTime(2019, 10, 28),
                                                EnteredBy = 33067,
                                                PartNumber = "PART3",
                                                Quantity = 1,
                                                FaultCode = "CODE2",
                                                ErrorType = "TYPE1",
                                                Id = 2,
                                                MinutesWasted = 1,
                                                Story = "STORY",
                                                Part = new Part
                                                           {
                                                               PartNumber = "PART3",
                                                               Description = "DESC3"
                                                           }
                                            },
                                        new PartFailLog
                                            {
                                                Batch = "BATCH",
                                                DateCreated = new DateTime(2019, 10, 28),
                                                EnteredBy = 33067,
                                                PartNumber = "PART4",
                                                Quantity = 1,
                                                FaultCode = "CODE1",
                                                ErrorType = "TYPE2",
                                                Id = 3,
                                                MinutesWasted = 1,
                                                Story = "STORY",
                                                Part = new Part
                                                           {
                                                               PartNumber = "PART4",
                                                               Description = "DESC4"
                                                           }
                                            }
                                    };
        }
    }
}
