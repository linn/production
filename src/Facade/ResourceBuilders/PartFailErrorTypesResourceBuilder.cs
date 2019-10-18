namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public class PartFailErrorTypesResourceBuilder : IResourceBuilder<IEnumerable<PartFailErrorType>>
    {
        private readonly PartFailErrorTypeResourceBuilder typeResourceBuilder = new PartFailErrorTypeResourceBuilder();

        public IEnumerable<PartFailErrorTypeResource> Build(IEnumerable<PartFailErrorType> types)
        {
            return types
                .OrderBy(b => b.ErrorType)
                .Select(a => this.typeResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<PartFailErrorType>>.Build(IEnumerable<PartFailErrorType> types) => this.Build(types);

        public string GetLocation(IEnumerable<PartFailErrorType> types)
        {
            throw new NotImplementedException();
        }
    }
}