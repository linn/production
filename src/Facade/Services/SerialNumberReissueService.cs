using System;
using System.Linq.Expressions;
using Linn.Common.Facade;
using Linn.Common.Persistence;
using Linn.Production.Domain.LinnApps.SerialNumberReissue;
using Linn.Production.Resources;

namespace Linn.Production.Facade.Services
{
    public class SerialNumberReissueService : FacadeService<SerialNumberReissue, int, SerialNumberReissueResource, SerialNumberReissueResource>
    {
        public SerialNumberReissueService(IRepository<SerialNumberReissue, int> repository, ITransactionManager transactionManager) 
            : base(repository, transactionManager)
        {
        }

        protected override SerialNumberReissue CreateFromResource(SerialNumberReissueResource resource)
        {
            return new SerialNumberReissue(resource.SernosGroup, resource.ArticleNumber)
            {
                Comments = resource.Comments,
                CreatedBy = resource.CreatedBy,
                NewArticleNumber = resource.NewArticleNumber,
                NewSerialNumber = resource.NewSerialNumber,
                SerialNumber = resource.SerialNumber,
            };
        }

        protected override void UpdateFromResource(SerialNumberReissue entity, SerialNumberReissueResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<SerialNumberReissue, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
