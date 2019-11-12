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

    public class WhenGettingLabelsForPart : ContextBase
    {
        private string searchTerm;

        [SetUp]
        public void SetUp()
        {
            this.searchTerm = "PART";

            var l1 = new WorksOrderLabel { PartNumber = "PART", Sequence = 1 };
            var l2 = new WorksOrderLabel { PartNumber = "PART", Sequence = 2 };

            this.LabelService.Search(this.searchTerm)
                .Returns(new SuccessResult<IEnumerable<WorksOrderLabel>>(new List<WorksOrderLabel> { l1, l2 }));

            this.Response = this.Browser.Get(
                "/production/works-orders/labels",
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
            this.LabelService.Received().Search(this.searchTerm);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<WorksOrderLabelResource>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(a => a.Sequence == 1);
            resources.Should().Contain(a => a.Sequence == 2);
        }
    }
}