namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;

    public class SmtShiftsResourceBuilder : IResourceBuilder<IEnumerable<SmtShift>>
    {
        private readonly SmtShiftResourceBuilder resourceBuilder = new SmtShiftResourceBuilder();

        public object Build(IEnumerable<SmtShift> shifts)
        {
            return shifts.OrderBy(s => s.Shift).Select(m => this.resourceBuilder.Build(m));
        }

        public string GetLocation(IEnumerable<SmtShift> model)
        {
            throw new System.NotImplementedException();
        }
    }
}