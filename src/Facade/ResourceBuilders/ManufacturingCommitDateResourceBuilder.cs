namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Resources.Extensions;
    using Linn.Common.Reporting.Resources.ReportResultResources;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Resources;

    public class ManufacturingCommitDateResourceBuilder : IResourceBuilder<ManufacturingCommitDateResults>
    {
        public ManufacturingCommitDateResultsResource Build(ManufacturingCommitDateResults model)
        {
            var returnResource = new ManufacturingCommitDateResultsResource
                                     {
                                         IncompleteLinesAnalysis = new ReportReturnResource
                                                                       {
                                                                           ReportResults =
                                                                               new List<ReportResultResource>
                                                                                   {
                                                                                       model.IncompleteLinesAnalysis.ConvertFinalModelToResource()
                                                                                   }
                                                                       }
                                     };

            var results = new List<ManufacturingCommitDateResultResource>();

            foreach (var manufacturingCommitDateResult in model.Results)
            {
                results.Add(new ManufacturingCommitDateResultResource
                                {
                                    NumberOfLines = manufacturingCommitDateResult.NumberOfLines,
                                    ProductType = manufacturingCommitDateResult.ProductType,
                                    NumberSupplied = manufacturingCommitDateResult.NumberSupplied,
                                    PercentageSupplied = manufacturingCommitDateResult.PercentageSupplied,
                                    NumberAvailable = manufacturingCommitDateResult.NumberAvailable,
                                    PercentageAvailable = manufacturingCommitDateResult.PercentageAvailable,
                                    Results = new ReportReturnResource
                                                  {
                                                      ReportResults =
                                                          new List<ReportResultResource>
                                                              {
                                                                  manufacturingCommitDateResult.Results.ConvertFinalModelToResource()
                                                              }
                                                  }
                                });
            }

            returnResource.Results = results;

            return returnResource;
        }

        object IResourceBuilder<ManufacturingCommitDateResults>.Build(ManufacturingCommitDateResults model) => this.Build(model);

        public string GetLocation(ManufacturingCommitDateResults model)
        {
            throw new NotImplementedException();
        }
    }
}