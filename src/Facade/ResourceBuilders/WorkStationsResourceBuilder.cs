namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class WorkStationsResourceBuilder : IResourceBuilder<IEnumerable<WorkStation>>
    {
        private readonly WorkStationResourceBuilder workStationResourceBuilder = new WorkStationResourceBuilder();

        public IEnumerable<WorkStationResource> Build(IEnumerable<WorkStation> workStations)
        {
            return workStations
                .OrderBy(b => b.WorkStationCode)
                .Select(a => this.workStationResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<WorkStation>>.Build(IEnumerable<WorkStation> workStations) => this.Build(workStations);

        public string GetLocation(IEnumerable<WorkStation> workStations)
        {
            throw new NotImplementedException();
        }
    }
}
