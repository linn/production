namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Authorisation;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class ProductionTriggerLevelService : FacadeService<ProductionTriggerLevel, string, ProductionTriggerLevelResource, ProductionTriggerLevelResource>
    {
        //private readonly IFacadeService<ManufacturingRoute, string, ManufacturingRouteResource, ManufacturingRouteResource> manufacturingRouteService;
        //private readonly IFacadeService<Cit, string, CitResource, CitResource> citService;
        //private readonly IFacadeService<Part, string, PartResource, PartResource> partService;
        //private readonly IFacadeService<Employee, int, EmployeeResource, EmployeeResource> employeeService;

        public ProductionTriggerLevelService(IRepository<ProductionTriggerLevel, string> repository,
                                             ITransactionManager transactionManager
                                             //IFacadeService<ManufacturingRoute, string, ManufacturingRouteResource, ManufacturingRouteResource> manufacturingRouteService,
                                             //IFacadeService<Cit, string, CitResource, CitResource> citService,
                                             //IFacadeService<Part, string, PartResource, PartResource> partService,
                                             //IFacadeService<Employee, int, EmployeeResource, EmployeeResource> employeeService
            )
            : base(repository, transactionManager)
        {
            //this.manufacturingRouteService = manufacturingRouteService;
            //this.citService = citService;
            //this.partService = partService;
            //this.employeeService = employeeService;
        }

        protected override ProductionTriggerLevel CreateFromResource(ProductionTriggerLevelResource resource)
        {
            //if (this.employeeService.GetById(resource.EngineerId) is NotFoundResult<Employee>)
            //{
            //    throw new Exc
            //}
            //check dependencies - routecode, cit, engineer id and part number
            //maybe I don't need to do this actually
             
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
                WorkStation = resource.WorkStation,
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
            entity.WorkStation = updateResource.WorkStation;
            entity.Temporary = updateResource.Temporary;
            entity.EngineerId = updateResource.EngineerId;
            entity.Story = updateResource.Story;
            entity.RouteCode = updateResource.RouteCode;
        }

        protected override Expression<Func<ProductionTriggerLevel, bool>> SearchExpression(string searchTerm)
        {
            return w => w.PartNumber.ToUpper().Contains(searchTerm.ToUpper());
        }
    }
}
