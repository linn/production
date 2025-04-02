namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ManufacturingOperationResourceBuilder : IResourceBuilder<ManufacturingOperation>
    {
        public ManufacturingOperationResource Build(ManufacturingOperation manufacturingOperation)
        {
            return new ManufacturingOperationResource
            {
                RouteCode = manufacturingOperation.RouteCode,
                ManufacturingId = manufacturingOperation.ManufacturingId,
                OperationNumber = manufacturingOperation.OperationNumber,
                Description = manufacturingOperation.Description,
                SkillCode = manufacturingOperation.SkillCode,
                ResourceCode = manufacturingOperation.ResourceCode,
                SetAndCleanTime = manufacturingOperation.SetAndCleanTime,
                ResourcePercentage = manufacturingOperation.ResourcePercentage,
                CycleTime = manufacturingOperation.CycleTime,
                LabourPercentage = manufacturingOperation.LabourPercentage,
                CITCode = manufacturingOperation.CITCode,
                Links = this.BuildLinks(manufacturingOperation).ToArray()
            };
        }

        public string GetLocation(ManufacturingOperation manufacturingOperation)
        {
            return $"/production/resources/manufacturing-routes/{Uri.EscapeDataString(manufacturingOperation.ResourceCode)}";
        }

        object IResourceBuilder<ManufacturingOperation>.Build(ManufacturingOperation manufacturingOperation) => this.Build(manufacturingOperation);

        private IEnumerable<LinkResource> BuildLinks(ManufacturingOperation manufacturingOperation)
        {
            return new List<LinkResource>
                       {
                           new LinkResource { Rel = "self", Href = this.GetLocation(manufacturingOperation) },
                           new LinkResource { Rel = "cit", Href = $"/production/maintenance/cits/{manufacturingOperation.CITCode}" }
                       };
        }
    }
}
