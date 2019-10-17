namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Models;

    public interface IOrdersReportsFacadeService
    {
        IResult<ManufacturingCommitDateResults> ManufacturingCommitDateReport(string date);
    }
}