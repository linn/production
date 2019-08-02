using Linn.Production.Domain.LinnApps;

namespace Linn.Production.Service.Tests.DepartmentModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDepartments : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var dept1 = new Department { DepartmentCode = "001", Description = "dept1", PersonnelDepartment = "Y" };
            var dept2 = new Department { DepartmentCode = "002", Description = "dept2", PersonnelDepartment = "Y" };
            this.DepartmentService.Search("Y")
                .Returns(new SuccessResult<IEnumerable<Department>>(new List<Department> { dept1, dept2 }));

            this.Response = this.Browser.Get(
                "/production/departments",
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
            this.DepartmentService.Received().Search("Y");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<Department>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(a => a.Description == "dept2");
            resources.Should().Contain(a => a.Description == "dept2");
        }
    }
}