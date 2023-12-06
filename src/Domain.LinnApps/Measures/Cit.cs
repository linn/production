namespace Linn.Production.Domain.LinnApps.Measures
{
    using System;
    using System.Collections.Generic;

    using Linn.Production.Domain.LinnApps.ViewModels;

    public class Cit
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string BuildGroup { get; set; }

        public int? SortOrder { get; set; }

        public string DepartmentCode { get; set; }

        public DateTime? DateInvalid { get; set; }

        public Employee CitLeader { get; set; }

        public ProductionMeasures Measures { get; set; }

        public List<AssemblyFail> AssemblyFails { get; set; }

        public List<ProductionTriggerLevel> ProductionTriggerLevels { get; set; }
    }
}
