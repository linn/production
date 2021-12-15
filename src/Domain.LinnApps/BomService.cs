namespace Linn.Production.Domain.LinnApps
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;

    public class BomService : IBomService
    {
        private readonly IQueryRepository<BomDetail> detailRepository;
        private readonly IQueryRepository<Bom> bomRepository;


        public BomService(
            IQueryRepository<BomDetail> detailRepository,
            IQueryRepository<Bom> bomRepository)
        {
            this.detailRepository = detailRepository;
            this.bomRepository = bomRepository;
        }

        public IEnumerable<BomDetail> GetAllAssembliesOnBom(string bomName)
        {
            var result = new List<BomDetail>();
            var currentDetails = this.detailRepository
                .FilterBy(x => x.Bom.BomName == bomName).ToList();

            foreach (var det in currentDetails)
            {
                if (this.GetChildren(det) != null)
                {
                    var temp = this.GetChildren(det).Where(x => x.Part.BomType == "A").ToList();
                    result.AddRange(temp);
                    currentDetails = temp;
                }
            }

            return result;
        }

        private IEnumerable<BomDetail> GetChildren(BomDetail detail)
        {
            return this.bomRepository
                .FindBy(b => b.BomName == detail.PartNumber)
                ?.Details;
        }
    }
}
