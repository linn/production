namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    public class AteTestDetailResourceBuilder : IResourceBuilder<AteTestDetail>
    {
        public AteTestDetailResource Build(AteTestDetail detail)
        {
            int.TryParse(detail.BatchNumber, out var batchNumber);
            return new AteTestDetailResource
                       {
                           ItemNumber = detail.ItemNumber,
                           TestId = detail.TestId,
                           PartNumber = detail.PartNumber,
                           NumberOfFails = detail.NumberOfFails,
                           CircuitRef = detail.CircuitRef,
                           AteTestFaultCode = detail.AteTestFaultCode,
                           SmtOrPcb = detail.SmtOrPcb,
                           Shift = detail.Shift,
                           BatchNumber = batchNumber,
                           PcbOperator = detail.PcbOperator?.Id,
                           PcbOperatorName = detail.PcbOperator?.FullName,
                           Comments = detail.Comments,
                           Machine = detail.Machine,
                           BoardFailNumber = detail.BoardFailNumber,
                           AoiEscape = detail.AoiEscape,
                           CorrectiveAction = detail.CorrectiveAction,
                           SmtFailId = detail.SmtFailId,
                           BoardSerialNumber = detail.BoardSerialNumber
                       };
        }

        public string GetLocation(AteTestDetail buildPlan)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<AteTestDetail>.Build(AteTestDetail detail) => this.Build(detail);

        private IEnumerable<LinkResource> BuildLinks(AteTestDetail detail)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(detail) };
        }
    }
}