namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class AccountingCompanyRepository : IRepository<AccountingCompany, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public AccountingCompanyRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public AccountingCompany FindById(string key)
        {
            return this.serviceDbContext.AccountingCompanies.Where(a => a.Name == key).ToList().FirstOrDefault();
        }

        public IQueryable<AccountingCompany> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(AccountingCompany entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(AccountingCompany entity)
        {
            throw new NotImplementedException();
        }

        public AccountingCompany FindBy(Expression<Func<AccountingCompany, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AccountingCompany> FilterBy(Expression<Func<AccountingCompany, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}