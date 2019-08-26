namespace Linn.Production.Service.Tests.EmployeesModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ViewModels;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAll : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var e1 = new Employee { Id = 1, FullName = "e1" };
            var e2 = new Employee { Id = 2, FullName = "e2" };
            this.EmployeeService.GetAll()
                .Returns(new SuccessResult<IEnumerable<Employee>>(new List<Employee> { e1, e2 }));

            this.Response = this.Browser.Get(
                "/production/maintenance/employees",
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
            this.EmployeeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<Employee>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(a => a.FullName == "e1");
            resources.Should().Contain(a => a.FullName == "e2");
        }
    }
}