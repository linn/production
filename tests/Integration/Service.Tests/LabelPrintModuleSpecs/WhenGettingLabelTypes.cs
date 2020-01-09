namespace Linn.Production.Service.Tests.LabelPrintModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingLabelTypes : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.LabelPrintService.GetLabelTypes().Returns(new SuccessResult<IEnumerable<IdAndName>>(new List<IdAndName>() { new IdAndName(1, "trumpeached label") }));


            this.Response = this.Browser.Get(
                "/production/maintenance/labels/label-types",
                with =>
                    {
                        with.Header("Accept", "application/json");
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
            this.LabelPrintService.Received().GetLabelTypes();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<IdAndNameResource>>().ToList();
            resource.Count().Should().Be(1);
            resource.Any(x => x.Id == 1 & x.Name == "trumpeached label").Should().BeTrue();
        }
    }
}