namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;

    public interface ICountComponentsFacadeService
    {
        SuccessResult<ComponentCount> CountComponents(string part);
    }
}