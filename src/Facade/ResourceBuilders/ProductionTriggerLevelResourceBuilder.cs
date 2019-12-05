namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ProductionTriggerLevelResourceBuilder : IResourceBuilder<ResponseModel<ProductionTriggerLevel>>
    {
        private readonly IAuthorisationService authorisationService;

        public ProductionTriggerLevelResourceBuilder(IAuthorisationService authorisationService)
        {
            this.authorisationService = authorisationService;
        }
        public ProductionTriggerLevelResource Build(ResponseModel<ProductionTriggerLevel> model)
        {
            return new ProductionTriggerLevelResource
                       {
                           PartNumber = model.ResponseData.PartNumber,
                           Description = model.ResponseData.Description,
                           CitCode = model.ResponseData.CitCode,
                           BomLevel = model.ResponseData.BomLevel,
                           FaZoneType = model.ResponseData.FaZoneType,
                           KanbanSize = model.ResponseData.KanbanSize,
                           MaximumKanbans = model.ResponseData.MaximumKanbans,
                           OverrideTriggerLevel = model.ResponseData.OverrideTriggerLevel,
                           TriggerLevel = model.ResponseData.TriggerLevel,
                           VariableTriggerLevel = model.ResponseData.VariableTriggerLevel,
                           WorkStation = model.ResponseData.WorkStation,
                           Temporary = model.ResponseData.Temporary,
                           EngineerId = model.ResponseData.EngineerId,
                           Story = model.ResponseData.Story,
                           RouteCode = model.ResponseData.RouteCode,
                           Links = this.BuildLinks(model).ToArray()
                       };
        }

        public string GetLocation(ResponseModel<ProductionTriggerLevel> model)
        {
            return $"/production/maintenance/production-trigger-levels/{Uri.EscapeDataString(model.ResponseData.PartNumber)}";
        }

        object IResourceBuilder<ResponseModel<ProductionTriggerLevel>>.Build(ResponseModel<ProductionTriggerLevel> model) => this.Build(model);

        private IEnumerable<LinkResource> BuildLinks(ResponseModel<ProductionTriggerLevel> model)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(model) };

            if (this.authorisationService.HasPermissionFor(AuthorisedAction.ProductionTriggerLevelUpdate, model.Privileges))
            {
                yield return new LinkResource { Rel = "edit", Href = this.GetLocation(model) };
            }
        }
    }
}