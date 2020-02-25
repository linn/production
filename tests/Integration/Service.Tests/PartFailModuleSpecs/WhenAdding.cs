namespace Linn.Production.Service.Tests.PartFailModuleSpecs
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
        private PartFailResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new PartFailResource
            {
                WorksOrderNumber = 99999999,
                EnteredBy = 12345678,
                EnteredByName = "Colin",
                PartNumber = "PART",
                PartDescription = "Something"
            };

            var partFail = new PartFail
                               {
                                   Id = 1,
                                   WorksOrder = new WorksOrder { OrderNumber = 1, PartNumber = "A", Part = new Part { Description = "desc" } },
                                   EnteredBy = new Employee { Id = 1, FullName = "name" },
                                   Part = new Part { PartNumber = "A", Description = "B" },
                                   StorageLocation = new StorageLocation { LocationId = 1, LocationCode = "LOC" },
                                   ErrorType = new PartFailErrorType { ErrorType = "Error", DateInvalid = null },
                                   FaultCode = new PartFailFaultCode { FaultCode = "F", Description = "Fault" }
                               };

            this.FacadeService.Add(Arg.Any<PartFailResource>())
                .Returns(new CreatedResult<PartFail>(partFail));

            this.Response = this.Browser.Post(
                "/production/quality/part-fails",
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
                .Add(Arg.Any<PartFailResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PartFailResource>();
            resource.Id.Should().Be(1);
        }
    }
}
