namespace Linn.Production.Facade.Extensions
{
    using Linn.Production.Domain.LinnApps.Reports.OptionTypes;
    using Linn.Production.Facade.Exceptions;

    public static class AteReportExtensions
    {
        public static AteReportGroupBy ParseAteReportOption(this string option)
        {
            switch (option)
            {
                case "component": return AteReportGroupBy.Component;
                case "board": return AteReportGroupBy.Board;
                case "fault-code": return AteReportGroupBy.FaultCode;
                default: throw new InvalidOptionException($"{option} is not a valid group by option");
            }
        }
    }
}