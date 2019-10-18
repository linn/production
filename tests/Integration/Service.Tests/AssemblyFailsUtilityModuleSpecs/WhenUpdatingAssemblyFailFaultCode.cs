namespace Linn.Production.Service.Tests.AssemblyFailsUtilityModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingAssemblyFailFaultCode : ContextBase
    {
        private AssemblyFailFaultCodeResource requestResource;

        private string code;

        private string description;

        private string explanation;

        [SetUp]
        public void SetUp()
        {
            this.code = "CODE";
            this.description = "DESCRIPTION";
            this.explanation = "Here is an explanation";

            this.requestResource = new AssemblyFailFaultCodeResource
                                       {
                                           FaultCode = this.code,
                                           Description = this.description,
                                           Explanation = this.explanation
                                       };

            var faultCode = new AssemblyFailFaultCode
                                {
                                    FaultCode = this.code,
                                    Description = this.description,
                                    Explanation = this.explanation
                                };

            this.FaultCodeService
                .Update(this.code, Arg.Any<AssemblyFailFaultCodeResource>())
                .Returns(new SuccessResult<AssemblyFailFaultCode>(faultCode));

            this.Response = this.Browser.Put(
                $"/production/quality/assembly-fail-fault-codes/{this.code}",
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
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.FaultCodeService
                .Received()
                .Update(this.code, Arg.Any<AssemblyFailFaultCodeResource>());
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