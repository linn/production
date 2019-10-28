namespace Linn.Production.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Triggers;

    public interface IProductionTriggersFacadeService
    {
        IResult<ProductionTriggersReport> GetProductionTriggerReport(string jobref, string citCode);

        IResult<ProductionTriggerFacts> GetProductionTriggerFacts(string jobref, string partNumber);
    }
}