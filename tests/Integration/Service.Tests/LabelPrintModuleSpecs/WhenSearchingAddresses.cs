namespace Linn.Production.Service.Tests.LabelPrintModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenSearchingAddresses : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.AddressService.SearchReturnTen(Arg.Any<string>()).Returns(new SuccessResult<IEnumerable<Address>>(new List<Address>() { new Address() { Id = 15, Addressee = "Drumph" } }));


            this.Response = this.Browser.Get(
                "/production/maintenance/labels/addresses",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "drum");
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
            this.AddressService.Received().SearchReturnTen(Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<Address>>().ToList();
            resource.Count().Should().Be(1);
            resource.Any(x => x.Id == 15 & x.Addressee == "Drumph").Should().BeTrue();
        }
    }
}
