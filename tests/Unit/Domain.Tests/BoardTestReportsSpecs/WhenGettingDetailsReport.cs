namespace Linn.Production.Domain.Tests.BoardTestReportsSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.BoardTests;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDetailsReport : ContextBase
    {
        private ResultsModel results;

        [SetUp]
        public void SetUp()
        {
            var boardTests = new List<BoardTest>
                                 {
                                     new BoardTest
                                         {
                                             BoardSerialNumber = "1",
                                             BoardName = "A1",
                                             Seq = 1,
                                             TestMachine = "G1",
                                             Status = "FAIL",
                                             TimeTested = "12:43:34",
                                             DateTested = 1.April(2021),
                                             FailType = new BoardFailType { Type = 1, Description = "Bad Fail" }
                                         },
                                     new BoardTest
                                         {
                                             BoardSerialNumber = "1",
                                             BoardName = "A2",
                                             Seq = 2,
                                             TestMachine = "G1",
                                             Status = "FAIL",
                                             DateTested = 1.April(2021),
                                             FailType = new BoardFailType { Type = 2 }
                                         },
                                     new BoardTest
                                         {
                                             BoardSerialNumber = "1",
                                             BoardName = "A2",
                                             Seq = 3,
                                             TestMachine = "G1",
                                             Status = "PASS",
                                             DateTested = 2.April(2021)
                                         }
                                 };
            this.BoardTestRepository.FilterBy(Arg.Any<Expression<Func<BoardTest, bool>>>())
                .Returns(boardTests.AsQueryable());
            this.results = this.Sut.GetBoardTestDetailsReport("1");
        }

        [Test]
        public void ShouldGetData()
        {
            this.BoardTestRepository.Received().FilterBy(Arg.Any<Expression<Func<BoardTest, bool>>>());
        }

        [Test]
        public void ShouldReturnResults()
        {
            this.results.Rows.Should().HaveCount(3);
            this.results.GetGridTextValue(this.results.RowIndex("1/1"), this.results.ColumnIndex("Board CitName")).Should().Be("A1");
            this.results.GetGridTextValue(this.results.RowIndex("1/1"), this.results.ColumnIndex("Board Serial Number")).Should().Be("1");
            this.results.GetGridTextValue(this.results.RowIndex("1/1"), this.results.ColumnIndex("Sequence")).Should().Be("1");
            this.results.GetGridTextValue(this.results.RowIndex("1/1"), this.results.ColumnIndex("Test Machine")).Should().Be("G1");
            this.results.GetGridTextValue(this.results.RowIndex("1/1"), this.results.ColumnIndex("Test Date")).Should().Be("01-Apr-2021");
            this.results.GetGridTextValue(this.results.RowIndex("1/1"), this.results.ColumnIndex("Time Tested")).Should().Be("12:43:34");
            this.results.GetGridTextValue(this.results.RowIndex("1/1"), this.results.ColumnIndex("Status")).Should().Be("FAIL");
            this.results.GetGridTextValue(this.results.RowIndex("1/1"), this.results.ColumnIndex("Fail Type")).Should().Be("1 - Bad Fail");
            this.results.GetGridTextValue(this.results.RowIndex("1/2"), this.results.ColumnIndex("Board CitName")).Should().Be("A2");
            this.results.GetGridTextValue(this.results.RowIndex("1/2"), this.results.ColumnIndex("Test Date")).Should().Be("01-Apr-2021");
            this.results.GetGridTextValue(this.results.RowIndex("1/3"), this.results.ColumnIndex("Board CitName")).Should().Be("A2");
            this.results.GetGridTextValue(this.results.RowIndex("1/3"), this.results.ColumnIndex("Test Date")).Should().Be("02-Apr-2021");
            this.results.GetGridTextValue(this.results.RowIndex("1/3"), this.results.ColumnIndex("Status")).Should().Be("PASS");
        }
    }
}