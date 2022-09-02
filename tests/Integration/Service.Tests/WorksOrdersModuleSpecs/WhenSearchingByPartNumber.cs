namespace Linn.Production.Service.Tests.WorksOrdersModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;
    using Linn.Production.Resources.RequestResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearchingByPartNumber : ContextBase
    {
        private string searchTerm;

        [SetUp]
        public void SetUp()
        {
            this.searchTerm = "ABC123";

            var worksOrder1 = new WorksOrder { OrderNumber = 1, Part = new Part { PartNumber = "part1" } };
            var worksOrder2 = new WorksOrder { OrderNumber = 2, Part = new Part { PartNumber = "part1" } };

            this.WorksOrdersService.FilterBy(Arg.Any<WorksOrderRequestResource>())
                .Returns(new SuccessResult<IEnumerable<WorksOrder>>(new List<WorksOrder> { worksOrder1, worksOrder2 }));

            this.Response = this.Browser.Get(
                "/production/works-orders",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", this.searchTerm);
                        with.Query("partNumber", "part1");
                        with.Query("fromDate", 1.January(2022).ToString("o"));
                        with.Query("toDate", 1.February(2022).ToString("o"));
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
            this.WorksOrdersService.Received().FilterBy(Arg.Any<WorksOrderRequestResource>());
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