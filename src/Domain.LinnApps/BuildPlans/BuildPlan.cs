namespace Linn.Production.Domain.LinnApps.BuildPlans
{
    using System;

    public class BuildPlan
    {
        public string BuildPlanName { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateInvalid { get; set; }

        public string LastMrpJobRef { get; set; }

        public DateTime? LastMrpDateStarted { get; set; }

        public DateTime? LastMrpDateFinished { get; set; }
    }
}
