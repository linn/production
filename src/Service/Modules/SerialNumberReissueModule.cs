namespace Linn.Production.Service.Modules
{
    using System.Linq;

    using Linn.Common.Resources;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Extensions;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Security;

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
            this.RequiresAuthentication();

            // TODO create privilege to be used here
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            var resource = this.Bind<SerialNumberReissueResource>();

            resource.Links = new[] { new LinkResource("created-by", this.Context?.CurrentUser?.GetEmployeeUri()) };

            var result = this.serialNumberReissueService.ReissueSerialNumber(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
