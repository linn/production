namespace Linn.Production.Service.Tests.PartFailModuleSpecs
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
            var a = new PartFailFaultCode { FaultCode = "CODE A" };
            var b = new PartFailFaultCode { FaultCode = "CODE B" };

            var errorTypes = new List<PartFailFaultCode> { a, b };
            this.FaultCodeService.GetAll().Returns(new SuccessResult<IEnumerable<PartFailFaultCode>>(errorTypes));

            this.Response = this.Browser.Get(
                "/production/quality/part-fail-fault-codes",
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
            this.FaultCodeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<PartFailFaultCodeResource>>();
            resource.Count().Should().Be(2);
        }
    }
}