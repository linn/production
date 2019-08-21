namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Resources;

    public class PcasRevisionsResourceBuilder : IResourceBuilder<IEnumerable<PcasRevision>>
    {
        private readonly PcasRevisionResourceBuilder pcasRevisionResourceBuilder = new PcasRevisionResourceBuilder();

        public IEnumerable<PcasRevisionResource> Build(IEnumerable<PcasRevision> pcasRevisions)
        {
            return pcasRevisions
                .OrderBy(b => b.Cref)
                .Select(a => this.pcasRevisionResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<PcasRevision>>.Build(IEnumerable<PcasRevision> pcasRevisions) => this.Build(pcasRevisions);

        public string GetLocation(IEnumerable<PcasRevision> pcasRevisions)
        {
            throw new NotImplementedException();
        }
    }
}