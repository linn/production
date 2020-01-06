namespace Linn.Production.Service.Tests.AteQualityModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAteTest : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var ateTest = new AteTest
                                   {
                                       TestId = 1,
                                       UserNumber = 1,
                                       WorksOrderNumber = 1,
                                       NumberTested = 1,
                                       NumberOfSmtComponents = 1,
                                       NumberOfPcbComponents = 1,
                                       NumberOfPcbFails = 1,
                                       NumberOfSmtFails = 1,
                                       NumberOfPcbBoardFails = 1,
                                       NumberOfSmtBoardFails = 1,
                                       PcbOperator = 1
                                   };
            this.AteTestService.GetById(1)
                .Returns(new SuccessResult<AteTest>(ateTest));

            this.Response = this.Browser.Get(
                "/production/quality/ate-tests/1",
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
            this.AteTestService.Received().GetById(1);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<AteTestResource>();
            resource.TestId.Should().Be(1);
        }
    }
}