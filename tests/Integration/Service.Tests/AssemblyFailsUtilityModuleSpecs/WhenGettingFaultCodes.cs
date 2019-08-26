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

    public class WhenGettingFaultCodes : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var a = new AssemblyFailFaultCode
                        {
                           FaultCode = "f1",
                           Description = "desc1"
                        };

            var b = new AssemblyFailFaultCode
                        {
                            FaultCode = "f2",
                            Description = "desc2"
                        };


            this.faultCodeService.GetAll()
                .Returns(new SuccessResult<IEnumerable<AssemblyFailFaultCode>>(new List<AssemblyFailFaultCode> { a, b }));


            this.Response = this.Browser.Get(
                "/production/quality/assembly-fail-fault-codes",
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
            this.faultCodeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<AssemblyFailFaultCodeResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.FaultCode == "f1");
            resource.Should().Contain(a => a.FaultCode == "f2");
        }
    }
}