namespace Linn.Production.Domain.Tests.ProductionBackOrdersReportSpecs
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
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Domain.LinnApps.ViewModels;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected ProductionBackOrdersReportService Sut { get; set; }

        protected IQueryRepository<ProductionBackOrdersView> ProductionBackOrdersViewRepository { get; private set; }

        protected IRepository<AccountingCompany, string> AccountingCompaniesRepository { get; private set; }

        protected IRepository<Cit, string> CitsRepository { get; private set; }

        protected IReportingHelper ReportingHelper { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ProductionBackOrdersViewRepository = Substitute.For<IQueryRepository<ProductionBackOrdersView>>();
            this.AccountingCompaniesRepository = Substitute.For<IRepository<AccountingCompany, string>>();
            this.CitsRepository = Substitute.For<IRepository<Cit, string>>();
            this.ReportingHelper = new ReportingHelper();

            this.AccountingCompaniesRepository.FindById("LINN")
                .Returns(new AccountingCompany { LatestSosJobId = 1234, Name = "LINN" });

            this.CitsRepository.FindById("S")
                .Returns(new Cit { Name = "Production", SortOrder = 10 });
            this.CitsRepository.FindById("T")
                .Returns(new Cit { Name = "Turning", SortOrder = 20 });

            this.ProductionBackOrdersViewRepository
                .FilterBy(Arg.Any<Expression<Func<ProductionBackOrdersView, bool>>>()).Returns(
                    new List<ProductionBackOrdersView>
                        {
                            new ProductionBackOrdersView
                                {
                                    ArticleNumber = "A",
                                    OrderQuantity = 3,
                                    OrderValue = 400.34m,
                                    InvoiceDescription = "A Desc",
                                    CanBuildQuantity = 3,
                                    CanBuildValue = 400.34m,
                                    OldestDate = 1.July(2020),
                                    JobId = 1234,
                                    CitCode = "S"
                                },
                            new ProductionBackOrdersView
                                {
                                    ArticleNumber = "B",
                                    OrderQuantity = 4,
                                    OrderValue = 2252.92m,
                                    InvoiceDescription = "B Desc",
                                    CanBuildQuantity = 2,
                                    CanBuildValue = 1126.46m,
                                    OldestDate = 1.December(2020),
                                    JobId = 1234,
                                    CitCode = "S"
                                },
                            new ProductionBackOrdersView
                                {
                                    ArticleNumber = "C",
                                    OrderQuantity = 1,
                                    OrderValue = 100m,
                                    InvoiceDescription = "C Desc",
                                    CanBuildQuantity = 1,
                                    CanBuildValue = 100m,
                                    OldestDate = 1.August(2020),
                                    JobId = 1234,
                                    CitCode = "T"
                                }
                        }.AsQueryable());

            this.Sut = new ProductionBackOrdersReportService(
                this.ProductionBackOrdersViewRepository,
                this.AccountingCompaniesRepository,
                this.CitsRepository,
                this.ReportingHelper);
        }
    }
}
