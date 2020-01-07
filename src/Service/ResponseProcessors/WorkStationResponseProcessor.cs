namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class WorkStationResponseProcessor : JsonResponseProcessor<WorkStation>
    {
        public WorkStationResponseProcessor(IResourceBuilder<WorkStation> resourceBuilder)
            : base(resourceBuilder, "work-stations", 1)
        {
        }
    }
}
