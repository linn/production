namespace Linn.Production.Domain.LinnApps
{
    public class ManufacturingSkill
    {
        public string SkillCode { get; set; }

        public string Description { get; set; }

        public int? HourlyRate { get; set; }

        public ManufacturingSkill(string skillCode, string description, int? hourlyRate)
        {
            this.SkillCode = skillCode;
            this.Description = description;
            this.HourlyRate = hourlyRate;
        }
    }
}
