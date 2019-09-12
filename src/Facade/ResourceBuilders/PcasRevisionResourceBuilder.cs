namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Resources;

    public class PcasRevisionResourceBuilder : IResourceBuilder<PcasRevision>
    {
        public PcasRevisionResource Build(PcasRevision pcasRevision)
        {
            return new PcasRevisionResource
                       {
                           PartNumber = pcasRevision.PartNumber,
                           Cref = pcasRevision.Cref
                       };
        }

        public string GetLocation(PcasRevision pcasRevision)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<PcasRevision>.Build(PcasRevision pcasRevision) => this.Build(pcasRevision);

        private IEnumerable<LinkResource> BuildLinks(PcasRevision pcasRevision)
        {
            throw new NotImplementedException();
        }
    }
}