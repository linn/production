namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class BuildPlanResource : HypermediaResource
    {
        public string BuildPlanName { get; set; }

        public string Description { get; set; }

        public string DateCreated { get; set; }

        public string DateInvalid { get; set; }

        public string LastMrpJobRef { get; set; }

        public string LastMrpDateStarted { get; set; }

        public string LastMrpDateFinished { get; set; }
    }
}
