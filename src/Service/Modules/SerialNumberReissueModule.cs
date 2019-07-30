using Linn.Production.Domain.LinnApps.SerialNumberReissue;

namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Resources;
    using Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class SerialNumberReissueModule : NancyModule
    {
        private readonly IFacadeService<SerialNumberReissue, int, SerialNumberReissueResource, SerialNumberReissueResource>
            serialNumberReissueService;

        public SerialNumberReissueModule(IFacadeService<SerialNumberReissue, int, SerialNumberReissueResource, SerialNumberReissueResource> serialNumberReissueService)
        {
            this.serialNumberReissueService = serialNumberReissueService;
            this.Post("/production/maintenance/serial-number-reissue", _ => this.AddSerialNumberReissue());
        }

        private object AddSerialNumberReissue()
        {
            var resource = this.Bind<SerialNumberReissueResource>();

            var result = this.serialNumberReissueService.Add(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
