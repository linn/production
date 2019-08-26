namespace Linn.Production.Domain.Tests.WhoBuiltWhatSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.ViewModels;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDetailReport : ContextBase
    {
        private ResultsModel results;

        [SetUp]
        public void SetUp()
        {
            this.WhoBuiltWhatRepository.FilterBy(Arg.Any<Expression<Func<WhoBuiltWhat, bool>>>())
                .Returns(
                    new List<WhoBuiltWhat>
                        {
                            new WhoBuiltWhat
                                {
                                    ArticleNumber = "a",
                                    CreatedBy = 808,
                                    UserName = "name1",
                                    QtyBuilt = 2,
                                    SernosNumber = 1
                                },
                            new WhoBuiltWhat
                                {
                                    ArticleNumber = "a",
                                    CreatedBy = 808,
                                    UserName = "name1",
                                    QtyBuilt = 1,
                                    SernosNumber = 2
                                },
                            new WhoBuiltWhat
                                {
                                    ArticleNumber = "b",
                                    CreatedBy = 808,
                                    UserName = "name1",
                                    QtyBuilt = 1,
                                    SernosNumber = 3
                                }
                        }.AsQueryable());
            this.results = this.Sut.WhoBuiltWhatDetails(1.May(2020).ToString("o"), 31.May(2020).ToString("o"), 808);
        }

        [Test]
        public void ShouldGetData()
        {
            this.WhoBuiltWhatRepository.Received().FilterBy(Arg.Any<Expression<Func<WhoBuiltWhat, bool>>>());
        }

        [Test]
        public void ShouldReturnResults()
        {
            this.results.ReportTitle.DisplayValue.Should().Be("Products built by name1 between 01-May-2020 and 31-May-2020");
            this.results.Rows.Should().HaveCount(3);
            this.results.GetGridTextValue(this.results.RowIndex("1"), this.results.ColumnIndex("sernosNumber"))
                .Should().Be("1");
            this.results.GetGridTextValue(this.results.RowIndex("1"), this.results.ColumnIndex("articleNumber"))
                .Should().Be("a");
            this.results.GetGridTextValue(this.results.RowIndex("3"), this.results.ColumnIndex("sernosNumber"))
                .Should().Be("3");
            this.results.GetGridTextValue(this.results.RowIndex("3"), this.results.ColumnIndex("articleNumber"))
                .Should().Be("b");
        }
    }
}