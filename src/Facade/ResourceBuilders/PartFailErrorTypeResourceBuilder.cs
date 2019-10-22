namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

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
                           DateInvalid = type.DateInvalid != null ? ((DateTime)type.DateInvalid).ToString("o") : null
                       };
        }

        public string GetLocation(PartFailErrorType type)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<PartFailErrorType>.Build(PartFailErrorType type) => this.Build(type);

        private IEnumerable<LinkResource> BuildLinks(PartFailErrorType type)
        {
            throw new NotImplementedException();
        }
    }
}