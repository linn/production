namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface IDeliveryPerfResultFacadeService
    {
        IResult<ResultsModel> GenerateDelPerfSummaryForCit(string citCode);

        IResult<ResultsModel> GetDelPerfDetail(string citCode, int? priority);
    }
}