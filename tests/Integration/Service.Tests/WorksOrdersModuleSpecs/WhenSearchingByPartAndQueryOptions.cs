namespace Linn.Production.Service.Tests.WorksOrdersModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearchingByPartAndQueryOptions : ContextBase
    {
        private string searchTerm;

        [SetUp]
        public void SetUp()
        {
            this.searchTerm = "part1";
            var results = new List<WorksOrder>();

            for (int i = 0; i <= 4; i++)
            {
                results.Add(new WorksOrder { OrderNumber = i, Part = new Part { PartNumber = "part1" } });
            }

            this.WorksOrdersService.SearchByBoardNumber(this.searchTerm, 5, "dateRaised")
                .Returns(new SuccessResult<IEnumerable<WorksOrder>>(results));

            this.Response = this.Browser.Get(
                "/production/works-orders-for-part",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("searchTerm", this.searchTerm);
                    with.Query("limit", "5");
                    with.Query("orderByDesc", "dateRaised");
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
            this.WorksOrdersService.Received().SearchByBoardNumber(this.searchTerm, 5, "dateRaised");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<WorksOrderResource>>().ToList();
            resource.Should().HaveCount(5);
        }
    }
}