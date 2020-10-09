namespace Linn.Production.Domain.LinnApps.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;

    public class AssemblyFailsDomainService : IAssemblyFailsDomainService
    {
        private readonly IRepository<AssemblyFail, int> assemblyFailsRepository;

        public AssemblyFailsDomainService(IRepository<AssemblyFail, int> assemblyFailsRepository)
        {
            this.assemblyFailsRepository = assemblyFailsRepository;
        }

        public IEnumerable<AssemblyFail> RefinedSearchAssemblyFails(
            string partNumber,
            int? productId,
            DateTime? date,
            string boardPart,
            string circuitPart)
        {
            var result = this.assemblyFailsRepository.FindAll();
            if (partNumber != null)
            {
                result = result.Where(f => f.WorksOrder.PartNumber == partNumber.ToUpper());
            }

            if (productId != null)
            {
                result = result.Where(f => f.SerialNumber == productId);
            }

            if (date != null)
            {
                result = result.Where(f => f.DateTimeFound.Date == ((DateTime)date).Date);
            }

            if (boardPart != null)
            {
                result = result.Where(f => f.BoardPart.PartNumber == boardPart.ToUpper());
            }

            if (circuitPart != null)
            {
                result = result.Where(f => f.CircuitPart == circuitPart.ToUpper());
            }

            return result;
        }
    }
}