﻿using System.Collections.Generic;
using Linn.Common.Facade;
using Linn.Common.Nancy.Facade;
using Linn.Production.Domain;

namespace Linn.Production.Service.ResponseProcessors
{
    public class ProductionMeasuresListResponseProcessor : JsonResponseProcessor<IEnumerable<ProductionMeasures>>
    {
        public ProductionMeasuresListResponseProcessor(IResourceBuilder<IEnumerable<ProductionMeasures>> resourceBuilder)
            : base(resourceBuilder, "production-measures-list", 1)
        {
        }
    }
}