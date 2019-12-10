namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ProductionTriggerLevelService : FacadeService<ProductionTriggerLevel, string, ProductionTriggerLevelResource, ProductionTriggerLevelResource>
    {
        public ProductionTriggerLevelService(IRepository<ProductionTriggerLevel, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override ProductionTriggerLevel CreateFromResource(ProductionTriggerLevelResource resource)
        {
            return new ProductionTriggerLevel
            {
                PartNumber = resource.PartNumber,
                Description = resource.Description,
                CitCode = resource.CitCode,
                BomLevel = resource.BomLevel,
                FaZoneType = resource.FaZoneType,
                KanbanSize = resource.KanbanSize,
                MaximumKanbans = resource.MaximumKanbans,
                OverrideTriggerLevel = resource.OverrideTriggerLevel,
                TriggerLevel = resource.TriggerLevel,
                VariableTriggerLevel = resource.VariableTriggerLevel,
                WorkStationName = resource.WorkStationName,
                Temporary = resource.Temporary,
                EngineerId = resource.EngineerId,
                Story = resource.Story,
                RouteCode = resource.RouteCode
            };
        }

        protected override void UpdateFromResource(ProductionTriggerLevel entity, ProductionTriggerLevelResource updateResource)
        {
            entity.PartNumber = updateResource.PartNumber;
            entity.Description = updateResource.Description;
            entity.CitCode = updateResource.CitCode;
            entity.BomLevel = updateResource.BomLevel;
            entity.FaZoneType = updateResource.FaZoneType;
            entity.KanbanSize = updateResource.KanbanSize;
            entity.MaximumKanbans = updateResource.MaximumKanbans;
            entity.OverrideTriggerLevel = updateResource.OverrideTriggerLevel;
            entity.TriggerLevel = updateResource.TriggerLevel;
            entity.VariableTriggerLevel = updateResource.VariableTriggerLevel;
            entity.WorkStationName = updateResource.WorkStationName;
            entity.Temporary = updateResource.Temporary;
            entity.EngineerId = updateResource.EngineerId;
            entity.Story = updateResource.Story;
            entity.RouteCode = updateResource.RouteCode;
        }

        protected override Expression<Func<ProductionTriggerLevel, bool>> SearchExpression(string searchTerm)
        {
            string[] searchTerms = searchTerm.Split(";").Select(str => str.Trim()).ToArray();
            if (searchTerms.Length != 4)
            {
                return w => w.PartNumber.ToUpper().Contains(searchTerm.ToUpper());
            }

            return w =>
                (string.IsNullOrWhiteSpace(searchTerms[0]) || w.PartNumber.ToUpper().Contains(searchTerms[0].ToUpper()))
                && (string.IsNullOrWhiteSpace(searchTerms[1]) || w.CitCode == searchTerms[1])
                && (searchTerms[2] == "null" || w.OverrideTriggerLevel > int.Parse(searchTerms[2]))
                && (searchTerms[3] == "null" || w.VariableTriggerLevel > int.Parse(searchTerms[3]));
        }
    }
}
