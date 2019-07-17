namespace Linn.Production.Service.Tests.AteQualityModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAteFaultCodes : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var ateFaultCode1 = new AteFaultCode("F1");
            var ateFaultCode2 = new AteFaultCode("F2");
            this.AteFaultCodeService.GetAll()
                .Returns(new SuccessResult<IEnumerable<AteFaultCode>>(new List<AteFaultCode> { ateFaultCode1, ateFaultCode2 }));

            this.Response = this.Browser.Get(
                "/production/quality/ate/fault-codes/",
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
            this.AteFaultCodeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<AteFaultCodeResource>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(a => a.FaultCode == "F1");
            resources.Should().Contain(a => a.FaultCode == "F2");
        }
    }
}