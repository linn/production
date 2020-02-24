namespace Linn.Production.Service.Tests.ManufacturingOperationsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingManufacturingOperations : ContextBase
    {
        private ManufacturingOperation manufacturingOperation;
        private ManufacturingOperation manufacturingOperation2;

        [SetUp]
        public void SetUp()
        {

            this.manufacturingOperation = new ManufacturingOperation(
                "routecode 1",
                77,
                15,
                "descrip of op",
                "codeOfOperation",
                "res Code",
                27,
                54,
                5,
                "cit code test");
            this.manufacturingOperation2 = new ManufacturingOperation(
                "routecode 2",
                58,
                22,
                "descrip of op2",
                "codeOfOperation2",
                "res Code2",
                272,
                542,
                52,
                "cit code test2");

            this.ManufacturingOperationService.GetAll()
                .Returns(
                    new SuccessResult<IEnumerable<ManufacturingOperation>>(
                        new List<ManufacturingOperation>
                            {
                                this.manufacturingOperation, this.manufacturingOperation2
                            }));

            this.Response = this.Browser.Get(
                "/production/resources/manufacturing-operations",
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
            this.ManufacturingOperationService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<ManufacturingOperationResource>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(a => a.RouteCode == this.manufacturingOperation.RouteCode && a.ManufacturingId == this.manufacturingOperation.ManufacturingId);
            resources.Should().Contain(a => a.RouteCode == this.manufacturingOperation2.RouteCode && a.ManufacturingId == this.manufacturingOperation2.ManufacturingId);
        }
    }
}
