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

    public class WhenGettingReport : ContextBase
    {
        private IEnumerable<ResultsModel> results;

        [SetUp]
        public void SetUp()
        {
            this.WhoBuiltWhatRepository.FilterBy(Arg.Any<Expression<Func<WhoBuiltWhat, bool>>>())
                .Returns(
                    new List<WhoBuiltWhat>
                                 {
                                     new WhoBuiltWhat { ArticleNumber = "a", CreatedBy = 1, UserName = "name1", QtyBuilt = 2 },
                                     new WhoBuiltWhat { ArticleNumber = "a", CreatedBy = 1, UserName = "name1", QtyBuilt = 1 },
                                     new WhoBuiltWhat { ArticleNumber = "b", CreatedBy = 1, UserName = "name1", QtyBuilt = 1 },
                                     new WhoBuiltWhat { ArticleNumber = "a", CreatedBy = 2, UserName = "name2", QtyBuilt = 1 },
                                     new WhoBuiltWhat { ArticleNumber = "c", CreatedBy = 2, UserName = "name2", QtyBuilt = 1 }
                                 }.AsQueryable());
            this.results = this.Sut.WhoBuiltWhat(1.May(2020).ToString("o"), 31.May(2020).ToString("o"), "S");
        }

        [Test]
        public void ShouldGetData()
        {
            this.WhoBuiltWhatRepository.Received().FilterBy(Arg.Any<Expression<Func<WhoBuiltWhat, bool>>>());
        }

        [Test]
        public void ShouldReturnResults()
        {
            this.results.Should().HaveCount(2);
            var report1 = this.results.First(a => a.ReportTitle.DisplayValue == "name1");
            report1.Rows.Should().HaveCount(2);
            report1.GetGridValue("a", "qty").Should().Be(3);
            report1.GetGridValue("b", "qty").Should().Be(1);
            var report2 = this.results.First(a => a.ReportTitle.DisplayValue == "name2");
            report2.Rows.Should().HaveCount(2);
            report2.GetGridValue("a", "qty").Should().Be(1);
            report2.GetGridValue("c", "qty").Should().Be(1);
        }
    }
}