namespace Linn.Production.Service.Tests.WorksOrdersModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using Nancy;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearchingByPartAndTooManyResults : ContextBase
    {
        private string searchTerm;

        [SetUp]
        public void SetUp()
        {
            this.searchTerm = "part1";
            var results = new List<WorksOrder>();

            for (int i = 0; i <= 500; i++)
            {
                results.Add(new WorksOrder { OrderNumber = i, Part = new Part { PartNumber = "part1" } });
            }

            this.WorksOrdersService.SearchByBoardNumber(this.searchTerm, null, null)
                .Returns(new SuccessResult<IEnumerable<WorksOrder>>(results));

            this.Response = this.Browser.Get(
                "/production/works-orders-for-part",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("searchTerm", this.searchTerm);
                }).Result;
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.WorksOrdersService.Received().SearchByBoardNumber(this.searchTerm, null, null);
        }
    }
}