namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class BomDetailRepository : IQueryRepository<BomDetail>
    {
        private readonly ServiceDbContext serviceDbContext;

        public BomDetailRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        // public IQueryable<BomDetail> GetAllDetailsForABom(string bomName)
        // {
        //     var result = new List<BomDetail>();
        //     var currentDetails = this.serviceDbContext.BomDetails.Include(d => d.Bom)
        //         .Where(x => x.Bom.BomName == bomName).ToList();
        //
        //     foreach (var det in currentDetails)
        //     {
        //         if (this.GetChildren(det) != null)
        //         {
        //             result.AddRange(this.GetChildren(det).Where(x => x.Part.BomType == "A"));
        //             currentDetails = this.GetChildren(det).ToList();
        //         }
        //     }
        //
        //     return result.AsQueryable();
        // }
        //
        //
        // private IEnumerable<BomDetail> GetChildren(BomDetail detail)
        // {
        //     return this.serviceDbContext.Boms.Include(b => b.Details)
        //         .ThenInclude(d => d.Part)
        //         .Where(b => b.BomName == detail.PartNumber)
        //         .ToList().FirstOrDefault()?.Details;
        // }
        public BomDetail FindBy(Expression<Func<BomDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BomDetail> FilterBy(Expression<Func<BomDetail, bool>> expression)
        {
            return this.serviceDbContext.BomDetails.Include(x => x.Bom).Where(expression);
        }

        public IQueryable<BomDetail> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
