namespace Linn.Production.Domain.Tests.AteReportsSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions.Extensions;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.Repositories;
    using Linn.Production.Domain.LinnApps.Services;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected AteReportsService Sut { get; set; }

        protected IReportingHelper ReportingHelper { get; private set; }

        protected IRepository<AteTest, int> AteTestRepository { get; private set; }

        protected List<AteTest> AteTests { get; set; }

        protected ILinnWeekRepository LinnWeekRepository { get; private set; }

        protected ILinnWeekService LinnWeekService { get; private set; }

        protected List<LinnWeek> Weeks { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.AteTestRepository = Substitute.For<IRepository<AteTest, int>>();
            this.ReportingHelper = new ReportingHelper();
            this.LinnWeekRepository = Substitute.For<ILinnWeekRepository>();
            this.LinnWeekService = new LinnWeekService(this.LinnWeekRepository);

            this.Weeks = new List<LinnWeek>
                             {
                                 new LinnWeek { LinnWeekNumber = 20, StartDate = 1.June(2020), EndDate = 6.June(2020), WWSYY = "25/20" },
                                 new LinnWeek { LinnWeekNumber = 21, StartDate = 7.June(2020), EndDate = 13.June(2020), WWSYY = "26/20" },
                                 new LinnWeek { LinnWeekNumber = 22, StartDate = 14.June(2020), EndDate = 20.June(2020), WWSYY = "27/20" }
                             };

            this.AteTests = new List<AteTest>
                                    {
                                        new AteTest
                                            {
                                                TestId = 1,
                                                DateTested = 1.June(2020),
                                                PlaceFound = "ATE",
                                                WorksOrder = new WorksOrder { PartNumber = "part 1" },
                                                Details = new List<AteTestDetail>
                                                              {
                                                                  new AteTestDetail
                                                                      {
                                                                          TestId = 1,
                                                                          AteTestFaultCode = "fault 1",
                                                                          SmtOrPcb = "SMT",
                                                                          NumberOfFails = 1,
                                                                          PartNumber = "comp 2",
                                                                          ItemNumber = 1,
                                                                          BatchNumber = "bn 1",
                                                                          CircuitRef = "circuit 12"
                                                                      }
                                                              }
                                            },
                                        new AteTest
                                            {
                                                TestId = 2,
                                                DateTested = 9.June(2020),
                                                PlaceFound = "ATE",
                                                WorksOrder = new WorksOrder { PartNumber = "part 2" },
                                                Details = new List<AteTestDetail>
                                                              {
                                                                  new AteTestDetail
                                                                      {
                                                                          TestId = 2,
                                                                          AteTestFaultCode = "fault 1",
                                                                          SmtOrPcb = "SMT",
                                                                          NumberOfFails = 1,
                                                                          PartNumber = "comp 1",
                                                                          ItemNumber = 1,
                                                                          BatchNumber = "bn 45"
                                                                      }
                                                              }
                                            },
                                        new AteTest
                                            {
                                                TestId = 3,
                                                DateTested = 20.June(2020),
                                                PlaceFound = "ATE",
                                                WorksOrder = new WorksOrder { PartNumber = "part 1" },
                                                Details = new List<AteTestDetail>
                                                              {
                                                                  new AteTestDetail
                                                                      {
                                                                          TestId = 3,
                                                                          AteTestFaultCode = "fault 2",
                                                                          SmtOrPcb = "SMT",
                                                                          NumberOfFails = 1,
                                                                          PartNumber = "comp 1",
                                                                          ItemNumber = 1,
                                                                          BatchNumber = "bn 1",
                                                                          CircuitRef = "circuit 1"
                                                                      }
                                                              }
                                            },
                                        new AteTest
                                            {
                                                TestId = 4,
                                                DateTested = 19.June(2020),
                                                PlaceFound = "ATE",
                                                WorksOrder = new WorksOrder { PartNumber = "part 1" },
                                                Details = new List<AteTestDetail>
                                                              {
                                                                  new AteTestDetail
                                                                      {
                                                                          TestId = 4,
                                                                          AteTestFaultCode = "fault 2",
                                                                          SmtOrPcb = "SMT",
                                                                          NumberOfFails = 2,
                                                                          PartNumber = "comp 2",
                                                                          ItemNumber = 1,
                                                                          CircuitRef = "circuit 1"
                                                                      }
                                                              }
                                            }
                                    };

            this.AteTestRepository.FilterBy(Arg.Any<Expression<Func<AteTest, bool>>>())
                .Returns(this.AteTests.AsQueryable());
            this.LinnWeekService.GetWeeks(1.June(2020), 30.June(2020))
                .Returns(this.Weeks.AsQueryable());

            this.Sut = new AteReportsService(this.AteTestRepository, this.ReportingHelper, this.LinnWeekService);
        }
    }
}