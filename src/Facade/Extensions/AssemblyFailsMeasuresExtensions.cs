namespace Linn.Production.Facade.Extensions
{
    using Linn.Production.Domain.LinnApps.Reports.OptionTypes;
    using Linn.Production.Facade.Exceptions;

    public static class AssemblyFailsMeasuresExtensions
    {
        public static AssemblyFailGroupBy ParseOption(this string option)
        {
            switch (option)
            {
                case "board-part-number": return AssemblyFailGroupBy.BoardPartNumber;
                case "circuit-part-number": return AssemblyFailGroupBy.CircuitPartNumber;
                case "cit": return AssemblyFailGroupBy.Cit;
                case "fault": return AssemblyFailGroupBy.Fault;
                case "person": return AssemblyFailGroupBy.Person;
                default: throw new InvalidOptionException($"{option} is not a valid group by option");
            }
        }
    }
}