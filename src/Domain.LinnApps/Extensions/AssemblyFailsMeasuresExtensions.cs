namespace Linn.Production.Domain.LinnApps.Extensions
{
    using Linn.Common.Domain.Exceptions;
    using Linn.Production.Domain.LinnApps.Reports.OptionTypes;

    public static class AssemblyFailsMeasuresExtensions
    {
        public static string ParseOption(this AssemblyFailGroupBy option)
        {
            switch (option)
            {
                case AssemblyFailGroupBy.BoardPartNumber: return "board-part-number";
                case AssemblyFailGroupBy.CircuitPartNumber: return "circuit-part-number";
                case AssemblyFailGroupBy.CitCode: return "cit";
                case AssemblyFailGroupBy.FaultCode: return "fault";
                case AssemblyFailGroupBy.Person: return "person";
                default: throw new DomainException($"{option} is not a valid option");
            }
        }
    }
}