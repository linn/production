﻿namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Proxy;
    using Linn.Production.Resources;

    public class ManufacturingOperationsService : FacadeService<ManufacturingOperation, int,
        ManufacturingOperationResource, ManufacturingOperationResource>, IServiceWithRemove<ManufacturingOperation, int,
                                                      ManufacturingOperationResource, ManufacturingOperationResource>
    {
        private readonly IDatabaseService databaseService;
        private readonly IRepository<ManufacturingOperation, int> manufacturingOperationsRepository;

        public ManufacturingOperationsService(
            IRepository<ManufacturingOperation, int> repository,
            ITransactionManager transactionManager,
            IDatabaseService databaseService)
            : base(repository, transactionManager)
        {
            this.manufacturingOperationsRepository = repository;
            this.databaseService = databaseService;
        }

        public IResult<ManufacturingOperation> Remove(ManufacturingOperation entity)
        {
            this.manufacturingOperationsRepository.Remove(entity);
            return new SuccessResult<ManufacturingOperation>(entity);
        }

        protected override ManufacturingOperation CreateFromResource(ManufacturingOperationResource resource)
        {
            return new ManufacturingOperation(
                resource.RouteCode,
                this.databaseService.GetIdSequence("AN_ARBITRARY_SEQUENCE"),
                resource.OperationNumber,
                resource.Description,
                resource.SkillCode,
                resource.ResourceCode,
                resource.SetAndCleanTime,
                decimal.Round(resource.CycleTime, 1),
                resource.LabourPercentage,
                resource.CITCode,
                resource.ResourcePercentage);
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
            entity.CycleTime = decimal.Round(updateResource.CycleTime, 1);
            entity.LabourPercentage = updateResource.LabourPercentage;
            entity.CITCode = updateResource.CITCode;
            entity.ResourcePercentage = updateResource.ResourcePercentage;
        }

        protected override Expression<Func<ManufacturingOperation, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
