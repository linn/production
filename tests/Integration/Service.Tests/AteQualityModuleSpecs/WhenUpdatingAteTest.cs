namespace Linn.Production.Service.Tests.AteQualityModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingAteTest : ContextBase
    {
        private AteTestResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new AteTestResource { TestId = 1 };
            this.AteTestService.Update(1, Arg.Any<AteTestResource>()).Returns(
                new SuccessResult<AteTest>(
                    new AteTest
                        {
                            TestId = 1,
                            NumberTested = 1,
                            WorksOrder = new WorksOrder { AteTests = new List<AteTest>() },
                            User = new Employee { Id = 1, FullName = "Name" },
                            PcbOperator = new Employee { Id = 1, FullName = "Name" }
                        }));

            this.Response = this.Browser.Put(
                "/production/quality/ate-tests/1",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
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
            this.AteTestService
                .Received()
                .Update(1, Arg.Is<AteTestResource>(r => r.TestId == this.requestResource.TestId));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<AteTestResource>();
            resource.TestId.Should().Be(1);
        }
    }
}