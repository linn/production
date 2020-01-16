namespace Linn.Production.Service.Tests.AteQualityModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ATE;
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
            var test1 = new AteTest
                            {
                                TestId = 1,
                                NumberTested = 1,
                                WorksOrder = new WorksOrder(),
                                User = new Employee { Id = 1, FullName = "Name" },
                                PcbOperator = new Employee { Id = 1, FullName = "Name" }
                            };

            this.AteTestService.Search("1")
                .Returns(new SuccessResult<IEnumerable<AteTest>>(new List<AteTest> { test1 }));


            this.Response = this.Browser.Get(
                "/production/quality/ate-tests",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "1");
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
            this.AteTestService.Received().Search("1");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<AteTestResource>>().ToList();
            resource.Should().HaveCount(1);
            resource.Should().Contain(a => a.TestId == 1);
        }
    }
}