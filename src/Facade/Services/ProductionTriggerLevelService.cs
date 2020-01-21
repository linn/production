namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ProductionTriggerLevelService : FacadeService<ProductionTriggerLevel, string, ProductionTriggerLevelResource, ProductionTriggerLevelResource>,
                                                 IProductionTriggerLevelsService
    {
        private readonly IRepository<ProductionTriggerLevel, string> repository;

        private readonly ITransactionManager transactionManager;

        public ProductionTriggerLevelService(IRepository<ProductionTriggerLevel, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
            this.repository = repository;
            this.transactionManager = transactionManager;
        }

        public IResult<ResponseModel<IEnumerable<ProductionTriggerLevel>>> Search(ProductionTriggerLevelsSearchRequestResource searchTerms, IEnumerable<string> privileges)
        {
            try
            {
                return new SuccessResult<ResponseModel<IEnumerable<ProductionTriggerLevel>>>(
                    new ResponseModel<IEnumerable<ProductionTriggerLevel>>(this.repository.FilterBy(this.SearchExpression(searchTerms)), privileges));
            }
            catch (NotImplementedException)
            {
                return new BadRequestResult<ResponseModel<IEnumerable<ProductionTriggerLevel>>>("Search is not implemented");
            }
        }

        public IResult<ResponseModel<ProductionTriggerLevel>> Remove(string partNumber, IEnumerable<string> privileges)
        {
            var entity = this.repository.FindById(partNumber);
            try
            {
                this.repository.Remove(entity);
            }
            catch (DomainException ex)
            {
                return new BadRequestResult<ResponseModel<ProductionTriggerLevel>>(($"Error deleting trigger level part number {partNumber} - {ex}"));
            }
            this.transactionManager.Commit();
            return new SuccessResult<ResponseModel<ProductionTriggerLevel>>(new ResponseModel<ProductionTriggerLevel>(entity, privileges));
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

        protected override Expression<Func<ProductionTriggerLevel, bool>> SearchExpression(string searchTerms)
        {
            throw new NotImplementedException();
        }

        protected Expression<Func<ProductionTriggerLevel, bool>> SearchExpression(ProductionTriggerLevelsSearchRequestResource searchTerms)
        {
            return w =>
                (string.IsNullOrWhiteSpace(searchTerms.SearchTerm) || w.PartNumber.ToUpper().Contains(searchTerms.SearchTerm.ToUpper()))
                && (string.IsNullOrWhiteSpace(searchTerms.CitSearchTerm) || w.CitCode == searchTerms.CitSearchTerm)
                && (string.IsNullOrWhiteSpace(searchTerms.OverrideSearchTerm) || searchTerms.OverrideSearchTerm == "null" || w.OverrideTriggerLevel > int.Parse(searchTerms.OverrideSearchTerm))
                && (string.IsNullOrWhiteSpace(searchTerms.AutoSearchTerm) || searchTerms.AutoSearchTerm == "null" || w.VariableTriggerLevel > int.Parse(searchTerms.AutoSearchTerm));
        }
    }
}
