namespace Linn.Production.Facade.Common
{
    using System.Collections.Generic;

    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Persistence.LinnApps.Repositories;

    public abstract class SingleRecordFacadeService<T, TUpdateResource> : ISingleRecordFacadeService<T, TUpdateResource>
    {
        private readonly ISingleRecordRepository<T> repository;

        private readonly ITransactionManager transactionManager;

        protected SingleRecordFacadeService(ISingleRecordRepository<T> repository, ITransactionManager transactionManager)
        {
            this.repository = repository;
            this.transactionManager = transactionManager;
        }

        public IResult<T> Get()
        {
            var entity = this.repository.GetRecord();
            if (entity == null)
            {
                return new NotFoundResult<T>();
            }

            return new SuccessResult<T>(entity);
        }

        public IResult<ResponseModel<T>> Get(IEnumerable<string> privileges)
        {
            var entity = this.repository.GetRecord();
            if (entity == null)
            {
                return new NotFoundResult<ResponseModel<T>>();
            }

            return new SuccessResult<ResponseModel<T>>(new ResponseModel<T>(entity, privileges));
        }

        public IResult<T> Update(TUpdateResource updateResource)
        {
            var entity = this.repository.GetRecord();
            if (entity == null)
            {
                return new NotFoundResult<T>();
            }

            try
            {
                this.UpdateFromResource(entity, updateResource);
            }
            catch (DomainException exception)
            {
                return new BadRequestResult<T>($"Error updating - {exception.Message}");
            }

            this.transactionManager.Commit();

            return new SuccessResult<T>(entity);
        }

        public IResult<ResponseModel<T>> Update(TUpdateResource updateResource, IEnumerable<string> privileges)
        {
            var entity = this.repository.GetRecord();
            if (entity == null)
            {
                return new NotFoundResult<ResponseModel<T>>();
            }

            try
            {
                this.UpdateFromResource(entity, updateResource);
            }
            catch (DomainException exception)
            {
                return new BadRequestResult<ResponseModel<T>>($"Error updating - {exception.Message}");
            }

            this.transactionManager.Commit();

            return new SuccessResult<ResponseModel<T>>(new ResponseModel<T>(entity, privileges));
        }

        protected abstract void UpdateFromResource(T entity, TUpdateResource updateResource);
    }
}
