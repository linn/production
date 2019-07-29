using System.Collections.Generic;
using Linn.Common.Facade;
using Linn.Production.Domain;

namespace Linn.Production.Facade.Services
{
    public interface IProductionMeasuresReportFacade
    {
        IResult<IEnumerable<ProductionMeasures>> GetProductionMeasuresForCits();
    }
}