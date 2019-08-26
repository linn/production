namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;

    public interface IProductionMeasuresReportFacade
    {
        IResult<IEnumerable<ProductionMeasures>> GetProductionMeasuresForCits();

        IResult<IEnumerable<IEnumerable<string>>> GetProductionMeasuresCsv();

        IResult<OsrInfo> GetOsrInfo();
    }
}