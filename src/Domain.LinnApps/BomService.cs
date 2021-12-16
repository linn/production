namespace Linn.Production.Domain.LinnApps
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;

    public class BomService : IBomService
    {
        private readonly IQueryRepository<Bom> bomRepository;

        public BomService(
            IQueryRepository<Bom> bomRepository)
        {
            this.bomRepository = bomRepository;
        }

        public IEnumerable<Bom> GetAllAssembliesOnBom(string bomName)
        {
            var result = new List<Bom>();

            // The root of the bom tree. Links to its children on its Details list. 
            var root = this.bomRepository.FindBy(x => x.BomName == bomName);

            var queue = new Queue<Bom>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                var n = queue.Count;

                while (n > 0)
                {
                    var node = queue.Dequeue();
                    result.Add(node);

                    foreach (var bomDetail in node.Details)
                    {
                        // only process a sub tree if this node is LIVE and not a COMPONENT
                        if (bomDetail.Part.BomType != "C" && bomDetail.ChangeState == "LIVE")
                        {
                            var bom = this.bomRepository.FindBy(
                                b => b.BomName == bomDetail.PartNumber);

                            if (bom != null)
                            {
                                queue.Enqueue(bom);
                            }
                        }
                    }

                    n -= 1;
                }
            }

            return result;
        }
    }
}
