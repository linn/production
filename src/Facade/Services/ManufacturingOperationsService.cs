namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using System;
    using System.Linq.Expressions;

    public class ManufacturingOperationsService : FacadeService<ManufacturingOperation, string, ManufacturingOperationResource, ManufacturingOperationResource>, IManufacturingOperationsFacade
    {
        private readonly IRepository<ManufacturingOperation, string> manufacturingOperationRepository;

        public ManufacturingOperationsService(IRepository<ManufacturingOperation, string> repository, ITransactionManager transactionManager) : base(repository, transactionManager)
        {
            this.manufacturingOperationRepository = repository;
        }

        public IResult<ManufacturingOperation> Update(string routeCode, string manufacturingId, ManufacturingOperationResource resource)
        {
            var result = this.GetById(routeCode, manufacturingId);

            if (!(result is SuccessResult<ManufacturingOperation>))
            {
                return result;
            }

            var successfulResult = (SuccessResult<ManufacturingOperation>)result;
            var operation = successfulResult.Data;

            this.UpdateFromResource(operation, resource);

            return new SuccessResult<ManufacturingOperation>(operation);
        }

        public IResult<ManufacturingOperation> GetById(string routeCode, string manufacturingId)
        {
            var operation = this.manufacturingOperationRepository.FindBy(
                x => x.RouteCode == routeCode && x.ManufacturingId == int.Parse(manufacturingId));

            if (operation == null)
            {
                return new BadRequestResult<ManufacturingOperation>("cannot find operation - wrong route code or manufacturing Id");
            }

            return new SuccessResult<ManufacturingOperation>(operation);
        }

        protected override ManufacturingOperation CreateFromResource(ManufacturingOperationResource resource)
        {
            return new ManufacturingOperation(resource.RouteCode, resource.ManufacturingId, resource.OperationNumber, resource.Description, resource.SkillCode, resource.ResourceCode, resource.SetAndCleanTime, resource.CycleTime, resource.LabourPercentage, resource.CITCode);
        }

        protected override void UpdateFromResource(ManufacturingOperation entity, ManufacturingOperationResource updateResource)
        {
            entity.RouteCode = updateResource.RouteCode;
            entity.ManufacturingId = updateResource.ManufacturingId;
            entity.OperationNumber = updateResource.OperationNumber;
            entity.Description = updateResource.Description;
            entity.SkillCode = updateResource.SkillCode;
            entity.ResourceCode = updateResource.ResourceCode;
            entity.SetAndCleanTime = updateResource.SetAndCleanTime;
            entity.CycleTime = updateResource.CycleTime;
            entity.LabourPercentage = updateResource.LabourPercentage;
            entity.CITCode = updateResource.CITCode;
        }

        protected override Expression<Func<ManufacturingOperation, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
