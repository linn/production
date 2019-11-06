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

    public class WhenSearchingByPart : ContextBase
    {
        private string searchTerm;

        [SetUp]
        public void SetUp()
        {
            this.searchTerm = "part1";

            var worksOrder1 = new WorksOrder { OrderNumber = 1, Part = new Part { PartNumber = "part1" } };
            var worksOrder2 = new WorksOrder { OrderNumber = 2, Part = new Part { PartNumber = "part1" } };

            this.WorksOrdersService.SearchByBoardNumber(this.searchTerm)
                .Returns(new SuccessResult<IEnumerable<WorksOrder>>(new List<WorksOrder> { worksOrder1, worksOrder2 }));

            this.Response = this.Browser.Get(
                "/production/works-orders-for-part",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("searchTerm", this.searchTerm);
                }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.WorksOrdersService.Received().SearchByBoardNumber(this.searchTerm);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<WorksOrderResource>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(a => a.OrderNumber == 1);
            resources.Should().Contain(a => a.OrderNumber == 2);
        }
    }
}