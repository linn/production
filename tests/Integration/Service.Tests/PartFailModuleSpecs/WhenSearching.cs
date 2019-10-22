namespace Linn.Production.Service.Tests.PartFailModuleSpecs
{
    using System.Collections.Generic;

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

    public class WhenSearching : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new PartFail
                        {
                            Id = 2,
                            WorksOrder = new WorksOrder { OrderNumber = 1, PartNumber = "A", Part = new Part { Description = "desc" } },
                            EnteredBy = new Employee { Id = 1, FullName = "name" },
                            Part = new Part { PartNumber = "A", Description = "B" },
                            StorageLocation = new StorageLocation { LocationId = 1, LocationCode = "LOC-A" },
                            ErrorType = new PartFailErrorType { ErrorType = "Error A", DateInvalid = null },
                            FaultCode = new PartFailFaultCode { FaultCode = "F", Description = "Fault" }
                        };
            var b = new PartFail
                        {
                            Id = 22,
                            WorksOrder = new WorksOrder { OrderNumber = 1, PartNumber = "A", Part = new Part { Description = "desc" } },
                            EnteredBy = new Employee { Id = 1, FullName = "name" },
                            Part = new Part { PartNumber = "A", Description = "B" },
                            StorageLocation = new StorageLocation { LocationId = 1, LocationCode = "LOC-B" },
                            ErrorType = new PartFailErrorType { ErrorType = "Error B", DateInvalid = null },
                            FaultCode = new PartFailFaultCode { FaultCode = "F", Description = "Fault" }
                        };
            var c = new PartFail
                        {
                            Id = 222,
                            WorksOrder = new WorksOrder { OrderNumber = 1, PartNumber = "A", Part = new Part { Description = "desc" } },
                            EnteredBy = new Employee { Id = 1, FullName = "name" },
                            Part = new Part { PartNumber = "A", Description = "B" },
                            StorageLocation = new StorageLocation { LocationId = 1, LocationCode = "LOC-C" },
                            ErrorType = new PartFailErrorType { ErrorType = "Error C", DateInvalid = null },
                            FaultCode = new PartFailFaultCode { FaultCode = "F", Description = "Fault" }
                        };

            var results = new List<PartFail> { a, b, c };

            this.FacadeService.Search("2").Returns(new SuccessResult<IEnumerable<PartFail>>(results));

            this.Response = this.Browser.Get(
                "/production/quality/part-fails",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("searchTerm", "2");
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
            this.FacadeService.Received().Search("2");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<PartFailResource>>();
            foreach (var assemblyFailResource in resource)
            {
                assemblyFailResource.Id.ToString().Contains("2").Should().BeTrue();
            }
        }
    }
}