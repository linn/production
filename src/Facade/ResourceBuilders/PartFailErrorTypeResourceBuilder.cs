namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public class PartFailErrorTypeResourceBuilder : IResourceBuilder<PartFailErrorType>
    {
        public PartFailErrorTypeResource Build(PartFailErrorType type)
        {
            return new PartFailErrorTypeResource
                       {
                           ErrorType = type.ErrorType,
                           DateInvalid = type.DateInvalid?.ToString("o"),
                           Links = this.BuildLinks(type).ToArray()
                       };
        }

        public string GetLocation(PartFailErrorType errorType)
        {
            return $"/production/quality/part-fail-error-types/{errorType.ErrorType}";
        }

        object IResourceBuilder<PartFailErrorType>.Build(PartFailErrorType errorType) => this.Build(errorType);

        private IEnumerable<LinkResource> BuildLinks(PartFailErrorType errorType)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(errorType) };
        }
    }
}