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

    public class WhenAdding : ContextBase
    {
        private AssemblyFailResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new AssemblyFailResource
                                       {
                                           WorksOrderNumber = 99999999,
                                           EnteredBy = 12345678,
                                           EnteredByName = "Colin",
                                           PartNumber = "PART",
                                           PartDescription = "Something"
                                       };

            var assemblyFail = new AssemblyFail
                                   {
                                       Id = 1,
                                       WorksOrder = new WorksOrder
                                                                {
                                                                    OrderNumber = 99999999,
                                                                    PartNumber = "PART",
                                                                    Part = new Part
                                                                               {
                                                                                   PartNumber = "PART", Description = "Something"
                                                                               }
                                                                },
                                       EnteredBy = new Employee
                                                       {
                                                           Id = 12345678,
                                                           FullName = "Colin"
                                                       }
                                   };

            this.FacadeService.Add(Arg.Any<AssemblyFailResource>())
                .Returns(new CreatedResult<AssemblyFail>(assemblyFail));

            this.Response = this.Browser.Post(
                "/production/quality/assembly-fails",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(this.requestResource);
                    }).Result;
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCallService()
        {
            this.FacadeService
                .Received()
                .Add(Arg.Any<AssemblyFailResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<AssemblyFailResource>();
            resource.Id.Should().Be(1);
        }
    }
}
