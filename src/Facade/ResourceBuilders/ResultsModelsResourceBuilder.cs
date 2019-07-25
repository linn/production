namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.ReportResultResources;

    public class ResultsModelsResourceBuilder : IResourceBuilder<IEnumerable<ResultsModel>>
    {
        private readonly ResultsModelResourceBuilder resultModelResourceBuilder = new ResultsModelResourceBuilder();

        public IEnumerable<ReportReturnResource> Build(IEnumerable<ResultsModel> resultsModels)
        {
            return resultsModels.Select(m => this.resultModelResourceBuilder.Build(m));
        }

        public string GetLocation(IEnumerable<ResultsModel> model)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<IEnumerable<ResultsModel>>.Build(IEnumerable<ResultsModel> model)
        {
            return this.Build(model);
        }
    }
}