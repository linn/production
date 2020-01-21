namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    public class AteTestsResourceBuilder : IResourceBuilder<IEnumerable<AteTest>>
    {
        private readonly AteTestResourceBuilder resourceBuilder = new AteTestResourceBuilder();

        public IEnumerable<AteTestResource> Build(IEnumerable<AteTest> tests)
        {
            return tests.Select(c => this.resourceBuilder.Build(c));
        }

        object IResourceBuilder<IEnumerable<AteTest>>.Build(IEnumerable<AteTest> tests) => this.Build(tests);

        public string GetLocation(IEnumerable<AteTest> s)
        {
            throw new NotImplementedException();
        }
    }
}