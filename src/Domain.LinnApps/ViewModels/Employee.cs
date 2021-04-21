namespace Linn.Production.Domain.LinnApps.ViewModels
{
    using System;
    using System.Collections.Generic;

    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.Measures;

    public class Employee
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public DateTime? DateInvalid { get; set; }

        public List<AssemblyFail> AssemblyFailsEntered { get; set; }

        public List<PartFail> PartFailsEntered { get; set; }

        public List<AteTest> AteTestsEntered { get; set; }

        public List<AteTest> AteTestsPcbOperatorOn { get; set; }

        public List<AteTestDetail> AteTestDetailsPcbOperatorOn { get; set; }

        public List<AssemblyFail> AssemblyFailsReturned { get; set; }

        public List<AssemblyFail> AssemblyFailsResponsibleFor { get; set; }

        public List<AssemblyFail> AssemblyFailsCompleted { get; set; }

        public List<PartFail> PartFailsOwned { get; set; }
    }
}