namespace Linn.Production.Domain.LinnApps
{
    using System.Collections.Generic;

    public interface IBomService
    {
        IEnumerable<BomDetail> GetAllAssembliesOnBom(string bomName);
    }
}
