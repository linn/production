namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.Measures;

    public class AssemblyFailsResponseProcessor : JsonResponseProcessor<IEnumerable<AssemblyFail>>
    {
    public AssemblyFailsResponseProcessor(IResourceBuilder<IEnumerable<AssemblyFail>> resourceBuilder)
        : base(resourceBuilder, "assembly-failss", 1)
    {
    }
    }
}