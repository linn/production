namespace Linn.Production.Service.Modules
{
    using Facade.Services;
    using Resources;
    using Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class SerialNumberReissueModule : NancyModule
    {
        private readonly ISerialNumberReissueService serialNumberReissueService;

        public SerialNumberReissueModule(ISerialNumberReissueService serialNumberReissueService)
        {
            this.serialNumberReissueService = serialNumberReissueService;
            this.Post("/production/maintenance/serial-number-reissue", _ => this.AddSerialNumberReissue());
        }

        private object AddSerialNumberReissue()
        {
            // TODO get user id from here
            var resource = this.Bind<SerialNumberReissueResource>();

            var result = this.serialNumberReissueService.ReissueSerialNumber(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
