namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Domain.LinnApps.SerialNumberIssue;

    public class SerialNumberIssueResponseProcessor : JsonResponseProcessor<SerialNumberIssue>
    {
        public SerialNumberIssueResponseProcessor(IResourceBuilder<SerialNumberIssue> resourceBuilder) 
            : base(resourceBuilder, "serial-number-issue", 1)
        {
        }
    }
}
