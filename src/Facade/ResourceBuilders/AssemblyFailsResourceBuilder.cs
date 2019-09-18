namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public class AssemblyFailsResourceBuilder : IResourceBuilder<IEnumerable<AssemblyFail>>
    {
        private readonly AssemblyFailResourceBuilder assemblyFailResourceBuilder = new AssemblyFailResourceBuilder();

        public IEnumerable<AssemblyFailResource> Build(IEnumerable<AssemblyFail> assemblyFails)
        {
            return assemblyFails.Select(a => this.assemblyFailResourceBuilder.Build(a));
        }

        public string GetLocation(IEnumerable<AssemblyFail> model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<IEnumerable<AssemblyFail>>.Build(IEnumerable<AssemblyFail> assemblyFails) => this.Build(assemblyFails);

    }
}