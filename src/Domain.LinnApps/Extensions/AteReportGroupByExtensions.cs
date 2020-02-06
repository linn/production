namespace Linn.Production.Domain.LinnApps.Extensions
{
    using Linn.Common.Domain.Exceptions;
    using Linn.Production.Domain.LinnApps.Reports.OptionTypes;

    public static class AteReportGroupByExtensions
    {
        public static string ParseOption(this AteReportGroupBy option)
        {
            switch (option)
            {
                case AteReportGroupBy.FaultCode: return "fault-code";
                case AteReportGroupBy.Component: return "component";
                case AteReportGroupBy.Board: return "board";
                case AteReportGroupBy.FailureRates: return "failure-rates";
                default: throw new DomainException($"{option} is not a valid option");
            }
        }
    }
}