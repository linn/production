namespace Linn.Production.Service.Tests.PartFailModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPartFailSuppliers : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var supplier1 = new PartFailSupplierView { SupplierId = 1, SupplierName = "one" };
            var supplier2 = new PartFailSupplierView { SupplierId = 2, SupplierName = "two" };

            var suppliers = new List<PartFailSupplierView> { supplier1, supplier2 };

            this.PartFailSupplierService.GetAll()
                .Returns(new SuccessResult<IEnumerable<PartFailSupplierView>>(suppliers));

            this.Response = this.Browser.Get(
                "/production/quality/part-fails/suppliers",
                with => { with.Header("Accept", "application/json"); }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.PartFailSupplierService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<PartFailSupplierResource>>();
            resource.Count().Should().Be(2);
        }
    }
}
