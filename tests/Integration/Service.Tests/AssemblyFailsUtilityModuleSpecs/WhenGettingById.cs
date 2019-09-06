namespace Linn.Production.Service.Tests.AssemblyFailsUtilityModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingById : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new AssemblyFail
                        {
                            Id = 1,
                            WorksOrder = new WorksOrder { OrderNumber = 1, PartNumber = "A" },
                            EnteredBy = new Employee { Id = 1, FullName = "name" }
                        };

            this.FacadeService.GetById(1).Returns(new SuccessResult<AssemblyFail>(a));
            this.SalesArticleService.GetDescriptionFromPartNumber("A").Returns("desc");

            this.Response = this.Browser.Get(
                "/production/quality/assembly-fails/1",
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
            this.FacadeService.Received().GetById(1);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<AssemblyFailResource>();
            resource.Id.Should().Be(1);
            resource.PartDescription.Should().Be("desc");
        }
    }
}