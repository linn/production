namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;

    public interface IWwdResultFacadeService
    {
        IResult<WwdResult> GenerateWwdResultForTrigger(string partNumber, int? qty, string ptlJobref);
    }
}