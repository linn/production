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

    public class WhenGettingAddress : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.AddressService.GetById(Arg.Any<int>()).Returns(new SuccessResult<Address>(new Address()
                                                                                               {
                                                                                                   Id = 15,
                                                                                                   Country = new Country(),
                                                                                                   Addressee = "Drumph"
                                                                                               }));


            this.Response = this.Browser.Get(
                "/production/maintenance/labels/address/15",
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
            this.AddressService.Received().GetById(Arg.Any<int>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<Address>();
            resource.Id.Should().Be(15);
            resource.Addressee.Should().Be("Drumph");

        }
    }
}
