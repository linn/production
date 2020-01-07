namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class WorkStationResourceBuilder : IResourceBuilder<WorkStation>
    {
        public WorkStationResource Build(WorkStation workStation)
        {
            return new WorkStationResource
            {
                WorkStationCode = workStation.WorkStationCode,
                Description = workStation.Description,
                CitCode = workStation.CitCode,
                VaxWorkStation = workStation.VaxWorkStation,
                AlternativeWorkStationCode = workStation.AlternativeWorkStationCode,
                ZoneType = workStation.ZoneType,
                Links = this.BuildLinks(workStation).ToArray()
            };
        }

        public string GetLocation(WorkStation workStation)
        {
            return $"/production/maintenance/work-stations/{Uri.EscapeDataString(workStation.WorkStationCode)}";
        }

        object IResourceBuilder<WorkStation>.Build(WorkStation workStation) => this.Build(workStation);

        private IEnumerable<LinkResource> BuildLinks(WorkStation workStation)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(workStation) };
        }
    }
}
