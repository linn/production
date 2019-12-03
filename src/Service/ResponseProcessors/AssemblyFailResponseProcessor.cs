namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.Measures;

    public class AssemblyFailResponseProcessor : JsonResponseProcessor<AssemblyFail>
    {
        public AssemblyFailResponseProcessor(IResourceBuilder<AssemblyFail> resourceBuilder)
            : base(resourceBuilder, "assembly-fails-fault_codes", 1)
        {
        }
    }
}
