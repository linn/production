namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class SmtShiftsResponseProcessor : JsonResponseProcessor<IEnumerable<SmtShift>>
    {
        public SmtShiftsResponseProcessor(IResourceBuilder<IEnumerable<SmtShift>> resourceBuilder)
            : base(resourceBuilder, "smt-shifts-resource-builder", 1)
        {
        }
    }
}