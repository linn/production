namespace Linn.Production.Service.Tests.AssemblyFailsUtilityModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdating : ContextBase
    {
        private AssemblyFailResource requestResource;

        [SetUp]
        public void SetUp()
        {
            var a = new AssemblyFail
                        {
                            Id = 1,
                            WorksOrder = new WorksOrder { OrderNumber = 1, PartNumber = "A", Part = new Part { Description = "desc" } },
                            EnteredBy = new Employee { Id = 1, FullName = "name" }
                        };

            this.requestResource = new AssemblyFailResource()
                                       {
                                           Id = 1,
                                           WorksOrderNumber = 1,
                                           PartNumber = "A",
                                           EnteredBy = 1
                                       };

            this.FacadeService.Update(1, Arg.Any<AssemblyFailResource>()).Returns(new SuccessResult<AssemblyFail>(a));

            this.Response = this.Browser.Put(
                "/production/quality/assembly-fails/1",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.JsonBody(this.requestResource);
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
            this.FacadeService
                .Received()
                .Update(1, Arg.Is<AssemblyFailResource>(r => r.Id == this.requestResource.Id));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<AssemblyFailResource>();
            resource.Id.Should().Be(1);
        }
    }
}
