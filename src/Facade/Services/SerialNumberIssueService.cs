using System;
using System.Linq.Expressions;
using Linn.Common.Facade;
using Linn.Common.Persistence;
using Linn.Production.Domain.LinnApps.SerialNumberIssue;
using Linn.Production.Resources;

namespace Linn.Production.Facade.Services
{
    public class SerialNumberIssueService : FacadeService<SerialNumberIssue, int, SerialNumberIssueResource, SerialNumberIssueResource>
    {
        public SerialNumberIssueService(IRepository<SerialNumberIssue, int> repository, ITransactionManager transactionManager) 
            : base(repository, transactionManager)
        {
        }

        protected override SerialNumberIssue CreateFromResource(SerialNumberIssueResource resource)
        {
            return new SerialNumberIssue(resource.SernosGroup, resource.ArticleNumber)
            {
                Comments = resource.Comments,
                CreatedBy = resource.CreatedBy,
                NewArticleNumber = resource.NewArticleNumber,
                NewSerialNumber = resource.NewSerialNumber,
                SerialNumber = resource.SerialNumber,
            };
        }

        protected override void UpdateFromResource(SerialNumberIssue entity, SerialNumberIssueResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<SerialNumberIssue, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
