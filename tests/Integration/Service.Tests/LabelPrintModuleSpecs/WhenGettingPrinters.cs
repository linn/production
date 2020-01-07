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

    public class WhenGettingPrinters : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.LabelPrintService.GetPrinters().Returns(new SuccessResult<IEnumerable<IdAndName>>(new List<IdAndName>() { new IdAndName(14, "printer1") }));


            this.Response = this.Browser.Get(
                "/production/maintenance/labels/printers",
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
            this.LabelPrintService.Received().GetPrinters();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<IdAndNameResource>>().ToList();
            resource.Count().Should().Be(1);
            resource.Any(x => x.Id == 14 & x.Name == "printer1").Should().BeTrue();

        }
    }
}