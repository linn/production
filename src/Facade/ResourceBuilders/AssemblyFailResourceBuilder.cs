namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Resources;

    public class AssemblyFailResourceBuilder : IResourceBuilder<AssemblyFail>
    {
        private readonly ISalesArticleService salesArticleService;

        public AssemblyFailResourceBuilder(ISalesArticleService salesArticleService)
        {
            this.salesArticleService = salesArticleService;
        }

        public AssemblyFailResource Build(AssemblyFail model)
        {
            return new AssemblyFailResource
                       {
                            Id = model.Id,
                            EnteredBy = model.EnteredBy.Id,
                            EnteredByName = model.EnteredBy?.FullName,
                            WorksOrderNumber = model.WorksOrder.OrderNumber,
                            PartNumber = model.WorksOrder?.PartNumber,
                            PartDescription = model.WorksOrder?.PartNumber != null 
                                                  ? this.salesArticleService.GetDescriptionFromPartNumber(model.WorksOrder.PartNumber) 
                                                  : null,
                            NumberOfFails = model.NumberOfFails,
                            SerialNumber = model.SerialNumber,
                            DateTimeFound = model.DateTimeFound.ToString("o"),
                            InSlot = model.InSlot,
                            Machine = model.Machine,
                            ReportedFault = model.ReportedFault,
                            Analysis = model.Analysis,
                            EngineeringComments = model.EngineeringComments,
                            BoardPartNumber = model.BoardPartNumber,
                            BoardDescription = model.BoardPart?.Description,
                            BoardSerial = model.BoardSerial,
                            Shift = model.Shift,
                            Batch = model.Batch,
                            CircuitRef = model.CircuitPartRef,
                            CircuitPartNumber = model.CircuitPart,
                            CitResponsible = model.CitResponsible?.Code,
                            CitResponsibleName = model.CitResponsible?.Name,
                            PersonResponsible = model.PersonResponsible?.Id,
                            PersonResponsibleName = model.PersonResponsible?.FullName,
                            FaultCode = model.FaultCode?.FaultCode,
                            FaultCodeDescription = model.FaultCode?.Description,
                            DateTimeComplete = model.DateTimeComplete?.ToString("o"),
                            CompletedBy = model.CompletedBy?.Id,
                            CompletedByName = model.CompletedBy?.FullName,
                            OutSlot = model.OutSlot,
                            ReturnedBy = model.ReturnedBy?.Id,
                            ReturnedByName = model.ReturnedBy?.FullName,
                            CorrectiveAction = model.CorrectiveAction,
                            CaDate = model.CaDate?.ToString("o"),
                            DateInvalid = model.DateInvalid?.ToString("o"),
                            AoiEscape = model.AoiEscape
                       };
        }

        public string GetLocation(AssemblyFail fail)
        {
            return $"/production/quality/assembly-fails/{fail.Id}";
        }

        object IResourceBuilder<AssemblyFail>.Build(AssemblyFail fail) => this.Build(fail);

        private IEnumerable<LinkResource> BuildLinks(AssemblyFail fail)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(fail) };
        }
    }
}