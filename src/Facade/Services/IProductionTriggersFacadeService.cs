namespace Linn.Production.Facade.Services
{
    using System.Collections;
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Triggers;

    public interface IProductionTriggersFacadeService
    {
        IResult<ProductionTriggersReport> GetProductionTriggerReport(string jobref, string citCode, string reportType);
    }
}