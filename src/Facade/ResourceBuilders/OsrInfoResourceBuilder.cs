namespace Linn.Production.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public class OsrInfoResourceBuilder : IResourceBuilder<OsrInfo>
    {
        public object Build(OsrInfo info)
        {
            return new OsrInfoResource
            {
                LastDaysToLookAhead = info.PtlMaster.LastDaysToLookAhead,
                LastPtlJobref = info.RunMaster.LastTriggerJobref,
                LastOSRRunDateTime = info.RunMaster.RunDateTime.ToString("o"),
                LastPtlRunDateTime = info.RunMaster.LastTriggerRunDateTime.ToString("o")
            };
        }

        public string GetLocation(OsrInfo model)
        {
            throw new System.NotImplementedException();
        }
    }
}