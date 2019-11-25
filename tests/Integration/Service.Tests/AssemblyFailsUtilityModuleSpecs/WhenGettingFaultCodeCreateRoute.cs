namespace Linn.Production.Service.Tests.AssemblyFailsUtilityModuleSpecs
{
    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingFaultCodeCreateRoute : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Response = this.Browser.Get(
                "/production/quality/assembly-fail-fault-codes/create",
                with => { with.Header("Accept", "application/json"); }).Result;
        }

        [Test]
        public void ShouldNotCallService()
        {
            this.FaultCodeService.DidNotReceive().GetById(Arg.Any<string>());
        }
    }
}