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
            var root = this.bomRepository.FindBy(x => x.BomName == bomName);

            var stack = new Stack<Bom>();
            stack.Push(root);
            while (stack.Count != 0)
            {
                var n = stack.Count;

                while (n > 0)
                {
                    var node = stack.Pop();
                    result.Add(node);

                    foreach (var bomDetail in node.Details)
                    {
                        // only process a sub tree if this node is LIVE and not a component
                        if (bomDetail.Part.BomType != "C" && bomDetail.ChangeState == "LIVE")
                        {
                            var bom = this.bomRepository.FindBy(
                                b => b.BomName == bomDetail.PartNumber);

                            if (bom != null)
                            {
                                stack.Push(bom);
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
