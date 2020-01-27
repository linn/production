namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Resources.Extensions;
    using Linn.Common.Reporting.Resources.ReportResultResources;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Resources;

    public class ShortageResultResourceBuilder : IResourceBuilder<ShortageResult>
    {
        public ShortageResultResource BuildResource(ShortageResult result)
        {
            return new ShortageResultResource
            {
                PartNumber = result.PartNumber,
                Priority = result.Priority,
                CanBuild = result.CanBuild,
                BackOrderQty = result.BackOrderQty,
                Build = result.Build,
                Kanban = result.Kanban,
                Results = new ReportReturnResource
                {
                    ReportResults =
                        new List<ReportResultResource>
                        {
                            result.Results.ConvertFinalModelToResource()
                        }
                }
            };
        }

        public object Build(ShortageResult model) => this.BuildResource(model);

        public string GetLocation(ShortageResult model)
        {
            throw new NotImplementedException();
        }
    }
}
