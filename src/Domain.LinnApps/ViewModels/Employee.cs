namespace Linn.Production.Domain.LinnApps.ViewModels
{
    using System.Collections.Generic;

    public class Employee
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public List<AssemblyFail> AssemblyFailsEntered { get; set; }

        public List<AssemblyFail> AssemblyFailsReturned { get; set; }

        public List<AssemblyFail> AssemblyFailsResponsibleFor { get; set; }

        public List<AssemblyFail> AssemblyFailsCompleted { get; set; } 
    }
}