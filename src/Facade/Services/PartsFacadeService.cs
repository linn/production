namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class PartsFacadeService : IPartsFacadeService
    {
        private readonly IRepository<Part, string> repository;

        public PartsFacadeService(IRepository<Part, string> repository)
        {
            this.repository = repository;
        }

        public SuccessResult<IEnumerable<Part>> SearchParts(string searchTerm)
        {
            return new SuccessResult<IEnumerable<Part>>(this.repository.FilterBy(s => s.PartNumber.Contains(searchTerm.ToUpper())).Take(10));
        }

        public SuccessResult<IEnumerable<Part>> GetAll()
        {
            return new SuccessResult<IEnumerable<Part>>(this.repository.FindAll());
        }
    }
}