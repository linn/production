namespace Linn.Production.Service.Tests.AssemblyFailsUtilityModuleSpecs
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
            var a = new AssemblyFail
                       {
                           Id = 2,
                           WorksOrder = new WorksOrder { OrderNumber = 1, PartNumber = "A", Part = new Part { Description = "desc" } },
                           EnteredBy = new Employee { Id = 1, FullName = "name" }
                       };
            var b = new AssemblyFail
                       {
                           Id = 22,
                           WorksOrder = new WorksOrder { OrderNumber = 1, PartNumber = "A", Part = new Part { Description = "desc" } },
                           EnteredBy = new Employee { Id = 1, FullName = "name" }
                       };
            var c = new AssemblyFail
                       {
                           Id = 222,
                           WorksOrder = new WorksOrder { OrderNumber = 1, PartNumber = "A", Part = new Part { Description = "desc" } },
                           EnteredBy = new Employee { Id = 1, FullName = "name" }
                       };

            var results = new List<AssemblyFail> { a, b, c };

            this.FacadeService.Search("2").Returns(new SuccessResult<IEnumerable<AssemblyFail>>(results));

            this.Response = this.Browser.Get(
                "/production/quality/assembly-fails",
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
            var resource = this.Response.Body.DeserializeJson<IEnumerable<AssemblyFailResource>>();
            foreach (var assemblyFailResource in resource)
            {
                assemblyFailResource.Id.ToString().Contains("2").Should().BeTrue();
            }
        }
    }
}