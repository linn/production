﻿namespace Linn.Production.Service.Tests.PartFailModuleSpecs
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
        private PartFailResource requestResource;

        [SetUp]
        public void SetUp()
        {
            var a = new PartFail
            {
                Id = 1,
                WorksOrder = new WorksOrder { OrderNumber = 1, PartNumber = "A", Part = new Part { Description = "desc" } },
                EnteredBy = new Employee { Id = 1, FullName = "name" },
                Part = new Part { PartNumber = "A", Description = "B" },
                StorageLocation = new StorageLocation { LocationId = 1, LocationCode = "LOC" },
                ErrorType = new PartFailErrorType { ErrorType = "Error", DateInvalid = null },
                FaultCode = new PartFailFaultCode { FaultCode = "F", Description = "Fault" }
            };

            this.requestResource = new PartFailResource()
            {
                Id = 1,
                WorksOrderNumber = 1,
                PartNumber = "A",
                EnteredBy = 1
            };

            this.FacadeService.Update(1, Arg.Any<PartFailResource>()).Returns(new SuccessResult<PartFail>(a));

            this.Response = this.Browser.Put(
                "/production/quality/part-fails/1",
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
                .Update(1, Arg.Is<PartFailResource>(r => r.Id == this.requestResource.Id));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PartFailResource>();
            resource.Id.Should().Be(1);
        }
    }
}
