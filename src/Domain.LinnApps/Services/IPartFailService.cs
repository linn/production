namespace Linn.Production.Domain.LinnApps.Services
{
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public interface IPartFailService
    {
        PartFail Check(PartFail candidate);
    }
}