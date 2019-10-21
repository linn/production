namespace Linn.Production.Domain.LinnApps.Reports
{
    using Linn.Production.Domain.LinnApps.Models;

    public interface IManufacturingCommitDateReport
    {
        ManufacturingCommitDateResults ManufacturingCommitDate(string date);
    }
}
