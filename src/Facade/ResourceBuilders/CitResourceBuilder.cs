namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public class CitResourceBuilder : IResourceBuilder<Cit>
    {
        public CitResource Build(Cit cit)
        {
            return new CitResource
                       {
                           Code = cit.Code,
                           Name = cit.Name,
                           BuildGroup = cit.BuildGroup,
                           SortOrder = cit.SortOrder,
                           DateInvalid = cit.DateInvalid?.ToString(),
                           Links = this.BuildLinks(cit).ToArray()
                       };
        }

        public string GetLocation(Cit cit)
        {
            return $"/production/maintenance/cits/{cit.Code}";
        }

        object IResourceBuilder<Cit>.Build(Cit cit) => this.Build(cit);

        private IEnumerable<LinkResource> BuildLinks(Cit cit)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(cit) };
        }
    }
}
