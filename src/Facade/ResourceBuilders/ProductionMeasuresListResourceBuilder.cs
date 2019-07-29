using System;
using System.Collections.Generic;
using System.Linq;
using Linn.Common.Facade;
using Linn.Production.Domain;
using Linn.Production.Resources;

namespace Linn.Production.Facade.ResourceBuilders
{
    public class ProductionMeasuresListResourceBuilder : IResourceBuilder<IEnumerable<ProductionMeasures>>
    {
        private readonly ProductionMeasuresResourceBuilder productionMeasuresResourceBuilder = new ProductionMeasuresResourceBuilder();

        public IEnumerable<ProductionMeasuresResource> Build(IEnumerable<ProductionMeasures> productionMeasureslist)
        {
            return productionMeasureslist
                .OrderBy(b => b.Cit.SortOrder)
                .Select(a => this.productionMeasuresResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<ProductionMeasures>>.Build(IEnumerable<ProductionMeasures> productionMeasures) => this.Build(productionMeasures);

        public string GetLocation(IEnumerable<ProductionMeasures> ateFaultCodes)
        {
            throw new NotImplementedException();
        }
    }
}