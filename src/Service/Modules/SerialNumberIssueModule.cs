namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Domain.LinnApps.SerialNumberIssue;
    using Resources;
    using Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class SerialNumberIssueModule : NancyModule
    {
        private readonly IFacadeService<SerialNumberIssue, int, SerialNumberIssueResource, SerialNumberIssueResource>
            serialNumberIssueService;

        public SerialNumberIssueModule(IFacadeService<SerialNumberIssue, int, SerialNumberIssueResource, SerialNumberIssueResource> serialNumberIssueService)
        {
            this.serialNumberIssueService = serialNumberIssueService;
            this.Post("/production/maintenance/serial-number-issue", _ => this.AddSerialNumberIssue());
        }

        private object AddSerialNumberIssue()
        {
            var resource = this.Bind<SerialNumberIssueResource>();

            var result = this.serialNumberIssueService.Add(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
