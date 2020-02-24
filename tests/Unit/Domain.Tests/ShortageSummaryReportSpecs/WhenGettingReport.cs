namespace Linn.Production.Domain.Tests.ShortageSummaryReportSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentAssertions;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.BackOrders;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Domain.LinnApps.Triggers;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingReport : ContextBase
    {
        private ShortageSummary results;

        [SetUp]
        public void SetUp()
        {
            this.PtlMasterRepository.GetRecord().Returns(new PtlMaster() { LastFullRunJobref = "AAAAAA" });
            this.CitRepository.FindById("S").Returns(new Cit() { Code = "S", Name = "Final Assembly" });

            var triggers = new List<ProductionTrigger>
                               {
                                   new ProductionTrigger
                                       {
                                           Priority = "1",
                                           PartNumber = "530/B",
                                           CanBuild = 3,
                                           ReqtForInternalAndTriggerLevelBT = 6,
                                           KanbanSize = 1
                                       },
                                   new ProductionTrigger
                                       {
                                           Priority = "1",
                                           PartNumber = "S3 301/G",
                                           CanBuild = 0,
                                           ReqtForInternalAndTriggerLevelBT = 2,
                                           KanbanSize = 1
                                       },
                                   new ProductionTrigger
                                       {
                                           Priority = "1",
                                           PartNumber = "LINGO 4 PSU",
                                           CanBuild = 1,
                                           ReqtForInternalAndTriggerLevelBT = 1,
                                           KanbanSize = 1
                                       },
                                   new ProductionTrigger
                                       {
                                           Priority = "2",
                                           PartNumber = "LP12B/3",
                                           CanBuild = 0,
                                           ReqtForInternalAndTriggerLevelBT = 1,
                                           KanbanSize = 1
                                       },
                                   new ProductionTrigger
                                       {
                                           Priority = "3",
                                           PartNumber = "AKITO/3B",
                                           CanBuild = 2,
                                           ReqtForInternalAndTriggerLevelBT = 3,
                                           KanbanSize = 6
                                       }
                               };
            this.ProductionTriggerRepository.FilterBy(Arg.Any<Expression<Func<ProductionTrigger, bool>>>())
                .Returns(triggers.AsQueryable());

            var linn = new AccountingCompany {Name = "Linn", LatestSosJobId = 1};
            this.AccountingCompanyRepository.FindById("LINN").Returns(linn);

            var backOrders = new List<ProductionBackOrder>
                                 {
                                     new ProductionBackOrder
                                         {
                                             ArticleNumber = "530/B",
                                             OrderNumber = 1,
                                             BackOrderQty = 1,
                                             RequestedDeliveryDate = new DateTime(2020, 1, 6)
                                         },
                                     new ProductionBackOrder
                                         {
                                             ArticleNumber = "530/B",
                                             OrderNumber = 2,
                                             BackOrderQty = 1,
                                             RequestedDeliveryDate = new DateTime(2019, 12, 17)
                                         },
                                     new ProductionBackOrder
                                         {
                                             ArticleNumber = "530/B",
                                             OrderNumber = 3,
                                             BackOrderQty = 1,
                                             RequestedDeliveryDate = new DateTime(2020, 1, 28)
                                         },
                                     new ProductionBackOrder
                                         {
                                             ArticleNumber = "530/B",
                                             OrderNumber = 4,
                                             BackOrderQty = 1,
                                             RequestedDeliveryDate = new DateTime(2019, 12, 19)
                                         },
                                     new ProductionBackOrder
                                         {
                                             ArticleNumber = "530/B",
                                             OrderNumber = 5,
                                             BackOrderQty = 1,
                                             RequestedDeliveryDate = new DateTime(2020, 1, 22)
                                         },
            };
            this.ProductionBackOrderRepository.FilterBy(Arg.Any<Expression<Func<ProductionBackOrder, bool>>>())
                .Returns(backOrders.AsQueryable());

            var wswShortages = new List<WswShortage>
            {
                new WswShortage
                {
                    PartNumber = "530/B", ShortPartNumber = "SPKR 090", ShortPartDescription = "19MM TWEETER",
                    ShortageCategory = "PROC", Required = 10, Stock = 33, AdjustedAvailable = 0, QtyReserved = 4, CanBuild = 0
                },
                new WswShortage
                {
                    PartNumber = "530/B", ShortPartNumber = "530 AMP MOD",
                    ShortPartDescription = "SERIES 5 - 530 AMP MOD", ShortageCategory = "PROC", Required = 10,
                    Stock = 33, AdjustedAvailable = 0, QtyReserved = 4
                },
                new WswShortage
                {
                    PartNumber = "S3 301/G", ShortPartNumber = "MCP 2466/2",
                    ShortPartDescription = "SERIES 3 CABINET", ShortageCategory = "PROC", Required = 5,
                    Stock = 11, AdjustedAvailable = 2, QtyReserved = 0
                },
                new WswShortage
                {
                    PartNumber = "LP12B/3", ShortPartNumber = "T-PLATE/1",
                    ShortPartDescription = "BENT TOP PLATE", ShortageCategory = "CP", Required = 1,
                    Stock = 0, AdjustedAvailable = 0, QtyReserved = 0
                }
            };
            this.ShortageRepository.FilterBy(Arg.Any<Expression<Func<WswShortage, bool>>>())
                .Returns(wswShortages.AsQueryable());

            this.results = this.Sut.ShortageSummaryByCit("S", "AAAAAA");
        }

        [Test]
        public void ShouldMakeShortageSummary()
        {
            this.results.Should().NotBeNull();
        }

        [Test]
        public void ShouldHaveCitName()
        {
            this.results.CitName.Should().Be("Final Assembly");
        }

        [Test]
        public void ShouldHaveCorrectSummaryTotals()
        {
            this.results.OnesTwos.Should().Be(4);
            this.results.NumShortages().Should().Be(3);
            this.results.BAT().Should().Be(0);
            this.results.Metalwork().Should().Be(1);
            this.results.Procurement().Should().Be(2);
        }

        [Test]
        public void ShouldHaveCorrectNumOfShortageResults()
        {
            this.results.Shortages.Count().Should().Be(3);
        }

        [Test]
        public void FirstShortageHasRightResults()
        {
            var shortage = this.results.Shortages.First();
            shortage.Priority.Should().Be("1");
            shortage.PartNumber.Should().Be("530/B");
            shortage.CanBuild.Should().Be(3);
            shortage.Kanban.Should().Be(1);
            shortage.BackOrderQty.Should().Be(5);
            shortage.EarliestRequestedDate.Should().Be(new DateTime(2019, 12, 17));
        }

        [Test]
        public void FirstShortageHasResultTable()
        {
            var report = this.results.Shortages.First()?.Results;
            report.Should().NotBeNull();
            report.Rows.Count().Should().Be(2);
            report.GetGridTextValue(0, report.ColumnIndex("shortPartNumber")).Should().Be("SPKR 090");
            report.GetGridTextValue(0, report.ColumnIndex("description")).Should().Be("19MM TWEETER");
            report.GetGridTextValue(0, report.ColumnIndex("category")).Should().Be("PROC");
            report.GetGridTextValue(0, report.ColumnIndex("reqt")).Should().Be("10");
            report.GetGridTextValue(0, report.ColumnIndex("stock")).Should().Be("33");
            report.GetGridTextValue(0, report.ColumnIndex("avail")).Should().Be("0");
            report.GetGridTextValue(0, report.ColumnIndex("res")).Should().Be("4");
            report.GetGridTextValue(0, report.ColumnIndex("canBuild")).Should().Be("0");
            report.GetGridTextValue(1, report.ColumnIndex("shortPartNumber")).Should().Be("530 AMP MOD");
        }
    }
}
