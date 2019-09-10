namespace Linn.Production.Domain.Tests.AssemblyFailsReportsSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions.Extensions;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Domain.LinnApps.Repositories;
    using Linn.Production.Domain.LinnApps.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected AssemblyFailsReportService Sut { get; set; }

        protected ILinnWeekPack LinnWeekPack { get; private set; }

        protected ILinnWeekService LinnWeekService { get; private set; }

        protected IReportingHelper ReportingHelper { get; private set; }

        protected IRepository<AssemblyFail, int> AssemblyFailRepository { get; private set; }

        protected ILinnWeekRepository LinnWeekRepository { get; private set; }

        protected List<AssemblyFail> AssemblyFails { get; set; }

        protected List<LinnWeek> Weeks { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.LinnWeekPack = Substitute.For<ILinnWeekPack>();
            this.LinnWeekRepository = Substitute.For<ILinnWeekRepository>();
            this.LinnWeekService = new LinnWeekService(this.LinnWeekRepository);
            this.AssemblyFailRepository = Substitute.For<IRepository<AssemblyFail, int>>();
            this.ReportingHelper = new ReportingHelper();

            this.AssemblyFails = new List<AssemblyFail>
                                    {
                                        new AssemblyFail
                                            {
                                                Id = 1,
                                                DateTimeFound = 1.June(2020),
                                                BoardPartNumber = "Board 1",
                                                NumberOfFails = 1,
                                                FaultCode = new AssemblyFailFaultCode { FaultCode = "F1", Description = "Fault 1" },
                                                CitResponsible = new Cit { Code = "C", Name = "Cit 1" },
                                                CircuitPart = "Circuit Part 1",
                                                WorksOrder = new WorksOrder { OrderNumber = 24, PartNumber = "W O Part" },
                                                ReportedFault = "report",
                                                Analysis = "analysis"
                                            },
                                        new AssemblyFail
                                            {
                                                Id = 2,
                                                DateTimeFound = 11.June(2020),
                                                BoardPartNumber = "Board 2",
                                                NumberOfFails = 2,
                                                FaultCode = new AssemblyFailFaultCode { FaultCode = "F2", Description = "Fault 2" },
                                                CitResponsible = new Cit { Code = "D", Name = "Cit 3" },
                                                CircuitPart = "Circuit Part 2"
                                            },
                                        new AssemblyFail
                                            {
                                                Id = 3,
                                                DateTimeFound = 2.June(2020),
                                                BoardPartNumber = "Board 1",
                                                NumberOfFails = 2,
                                                FaultCode = new AssemblyFailFaultCode { FaultCode = "F1", Description = "Fault 1" },
                                                CitResponsible = new Cit { Code = "C", Name = "Cit 1" },
                                                CircuitPart = "Circuit Part 1",
                                                WorksOrder = new WorksOrder { OrderNumber = 45, PartNumber = "W O Part" }
                                            }
                                    };
            this.Weeks = new List<LinnWeek>
                             {
                                 new LinnWeek { LinnWeekNumber = 20, StartDate = 1.June(2020), EndDate = 6.June(2020), WWSYY = "25/20" },
                                 new LinnWeek { LinnWeekNumber = 21, StartDate = 7.June(2020), EndDate = 13.June(2020), WWSYY = "26/20" },
                                 new LinnWeek { LinnWeekNumber = 22, StartDate = 14.June(2020), EndDate = 20.June(2020), WWSYY = "27/20" }
                             };

            this.LinnWeekService.GetWeeks(1.June(2020), 30.June(2020)).Returns(this.Weeks.AsQueryable());
            this.AssemblyFailRepository.FilterBy(Arg.Any<Expression<Func<AssemblyFail, bool>>>())
                .Returns(this.AssemblyFails.AsQueryable());

            this.Sut = new AssemblyFailsReportService(
                this.LinnWeekPack,
                this.AssemblyFailRepository,
                this.LinnWeekService,
                this.ReportingHelper);
        }
    }
}