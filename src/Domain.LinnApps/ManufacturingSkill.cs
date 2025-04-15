﻿namespace Linn.Production.Domain.LinnApps
{
    using System;

    public class ManufacturingSkill
    {
        public ManufacturingSkill(string skillCode, string description, int? hourlyRate, DateTime? dateInvalid)
        {
            this.SkillCode = skillCode;
            this.Description = description;
            this.HourlyRate = hourlyRate;
            this.DateInvalid = dateInvalid;
        }

        public string SkillCode { get; set; }

        public string Description { get; set; }

        public int? HourlyRate { get; set; }

        public DateTime? DateInvalid { get; set; }
    }
}
