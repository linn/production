namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Production.Domain;

    public interface IProductionMeasuresReportFacade
    {
        IResult<IEnumerable<ProductionMeasures>> GetProductionMeasuresForCits();

        IResult<IEnumerable<IEnumerable<string>>> GetProductionMeasuresCsv();
    }
}