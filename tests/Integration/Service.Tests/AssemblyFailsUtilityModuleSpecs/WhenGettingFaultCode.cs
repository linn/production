namespace Linn.Production.Service.Tests.AssemblyFailsUtilityModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingFaultCode : ContextBase
    {
        private string code;

        private string description;

        private string explanation;

        [SetUp]
        public void SetUp()
        {
            this.code = "CODE";
            this.description = "DESCRIPTION";
            this.explanation = "Here is an explanation";

            var faultCode = new AssemblyFailFaultCode
                                {
                                    FaultCode = this.code,
                                    Description = this.description,
                                    Explanation = this.explanation
                                };

            this.FaultCodeService.GetById(this.code).Returns(new SuccessResult<AssemblyFailFaultCode>(faultCode));

            this.Response = this.Browser.Get(
                "/production/quality/assembly-fail-fault-codes/CODE",
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
            this.FaultCodeService.Received().GetById(this.code);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<AssemblyFailFaultCodeResource>();
            resource.FaultCode.Should().Be(this.code);
            resource.Description.Should().Be(this.description);
            resource.Explanation.Should().Be(this.explanation);
        }
    }
}