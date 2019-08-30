namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
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
                CycleTime = manufacturingOperation.CycleTime,
                LabourPercentage = manufacturingOperation.LabourPercentage,
                CITCode = manufacturingOperation.CITCode
            };
        }

        public string GetLocation(ManufacturingOperation manufacturingOperation)
        {
            return $"/production/resources/manufacturing-routes/{Uri.EscapeDataString(manufacturingOperation.ResourceCode)}";
        }

        object IResourceBuilder<ManufacturingOperation>.Build(ManufacturingOperation manufacturingOperation) => this.Build(manufacturingOperation);

        private IEnumerable<LinkResource> BuildLinks(ManufacturingOperation manufacturingOperation)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(manufacturingOperation) };
        }
    }
}
