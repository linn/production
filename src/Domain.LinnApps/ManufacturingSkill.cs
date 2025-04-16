namespace Linn.Production.Domain.LinnApps
{
    using System;

    public class ManufacturingSkill
    {
        public string SkillCode { get; set; }

        public string Description { get; set; }

        public int? HourlyRate { get; set; }

        public DateTime? DateInvalid { get; set; }
    }
}
