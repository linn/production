namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Models;

    public interface IShortageSummaryFacadeService
    {
        IResult<ShortageSummary> ShortageSummaryByCit(string citCode, string ptlJobref);
    }
}