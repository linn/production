namespace Linn.Production.Domain.LinnApps.Services
{
    using Linn.Production.Domain.LinnApps.Measures;

    public interface IPartFailService
    {
        PartFail Create(PartFail candidate);
    }
}