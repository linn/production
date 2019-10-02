namespace Linn.Production.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    public class SmtShiftResourceBuilder : IResourceBuilder<SmtShift>
    {
        public SmtShiftResource Build(SmtShift model)
        {
            return new SmtShiftResource { Shift = model.Shift, Description = model.Description };
        }

        object IResourceBuilder<SmtShift>.Build(SmtShift shift) => this.Build(shift);

        public string GetLocation(SmtShift model)
        {
            throw new System.NotImplementedException();
        }
    }
}