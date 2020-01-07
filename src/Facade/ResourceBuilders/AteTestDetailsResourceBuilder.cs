namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    public class AteTestDetailsResourceBuilder : IResourceBuilder<IEnumerable<AteTestDetail>>
    {
        private readonly AteTestDetailResourceBuilder detailResourceBuilder = new AteTestDetailResourceBuilder();

        public IEnumerable<AteTestDetailResource> Build(IEnumerable<AteTestDetail> details)
        {
            return details.Select(c => this.detailResourceBuilder.Build(c));
        }

        object IResourceBuilder<IEnumerable<AteTestDetail>>.Build(IEnumerable<AteTestDetail> details) => this.Build(details);

        public string GetLocation(IEnumerable<AteTestDetail> details)
        {
            throw new NotImplementedException();
        }
    }
}