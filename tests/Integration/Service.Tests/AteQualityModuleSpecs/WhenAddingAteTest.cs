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

    public class WhenAddingAteTest : ContextBase
    {
        private AteTestResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new AteTestResource { TestId = 1 };
            var ateTest = new AteTest
                              {
                                  TestId = 1,
                                  NumberTested = 1,
                                  WorksOrder = new WorksOrder { AteTests = new List<AteTest>() },
                                  User = new Employee { Id = 1, FullName = "Name" },
                                  PcbOperator = new Employee { Id = 1, FullName = "Name" }
                              };
            this.AteTestService.Add(Arg.Any<AteTestResource>())
                .Returns(new CreatedResult<AteTest>(ateTest));

            this.Response = this.Browser.Post(
                "/production/quality/ate-tests/",
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
            this.AteTestService
                .Received()
                .Add(Arg.Is<AteTestResource>(r => r.TestId == this.requestResource.TestId));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<AteTestResource>();
            resource.TestId.Should().Be(1);
        }
    }
}