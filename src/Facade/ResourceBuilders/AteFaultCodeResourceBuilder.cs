namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    public class AteFaultCodeResourceBuilder : IResourceBuilder<AteFaultCode>
    {
        public AteFaultCodeResource Build(AteFaultCode ateFaultCode)
        {
            return new AteFaultCodeResource
                       {
                           FaultCode = ateFaultCode.FaultCode,
                           Description = ateFaultCode.Description,
                           DateInvalid = ateFaultCode.DateInvalid?.ToString("o"),
                           Links = this.BuildLinks(ateFaultCode).ToArray()
                       };
        }

        public string GetLocation(AteFaultCode ateFaultCode)
        {
            return $"/production/quality/ate/fault-codes/{Uri.EscapeDataString(ateFaultCode.FaultCode)}";
        }

        object IResourceBuilder<AteFaultCode>.Build(AteFaultCode ateFaultCode) => this.Build(ateFaultCode);

        private IEnumerable<LinkResource> BuildLinks(AteFaultCode ateFaultCode)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(ateFaultCode) };
        }
    }
}