namespace Linn.Production.Domain.LinnApps.Reports
{
    using Linn.Production.Domain.LinnApps.Models;

    public interface IOrdersReports
    {
        ManufacturingCommitDateResults ManufacturingCommitDate(string date);
    }
}
