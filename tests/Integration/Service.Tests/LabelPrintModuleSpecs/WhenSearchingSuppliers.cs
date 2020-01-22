namespace Linn.Production.Service.Tests.LabelPrintModuleSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;
    using NUnit.Framework.Internal;
    using System.Collections.Generic;
    using System.Linq;

    public class WhenSearchingSuppliers : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.SupplierService.Search(Arg.Any<string>()).Returns(new SuccessResult<IEnumerable<Supplier>>(new List<Supplier>() { new Supplier() { SupplierId = 19, SupplierName = "Dunder Mifflin" } }));


            this.Response = this.Browser.Get(
                "/production/maintenance/labels/suppliers",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "Dunder");
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
            this.SupplierService.Received().Search(Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<Supplier>>().ToList();
            resource.Count().Should().Be(1);
            resource.Any(x => x.SupplierId == 19 & x.SupplierName == "Dunder Mifflin").Should().BeTrue();
        }
    }
}
