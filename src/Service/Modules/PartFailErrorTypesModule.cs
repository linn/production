namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class PartFailErrorTypesModule : NancyModule
    {
        private readonly IFacadeService<PartFailErrorType, string, PartFailErrorTypeResource, PartFailErrorTypeResource> facadeService;

        public PartFailErrorTypesModule(
            IFacadeService<PartFailErrorType, string, PartFailErrorTypeResource, PartFailErrorTypeResource> facadeService)
        {
            this.facadeService = facadeService;
            this.Get("/production/quality/part-fail-error-types", _ => this.GetAll());
            this.Get("/production/quality/part-fail-error-types/{type*}", parameters => this.GetById(parameters.type));
            this.Put("/production/quality/part-fail-error-types/{type*}", parameters => this.Update(parameters.type));
            this.Post("/production/quality/part-fail-error-types", parameters => this.Add());
        }

        private object GetAll()
        {
            var result = this.facadeService.GetAll();
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetById(string id)
        {
            var result = this.facadeService.GetById(id);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object Update(string type)
        {
            var resource = this.Bind<PartFailErrorTypeResource>();

            var result = this.facadeService.Update(type, resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object Add()
        {
            var resource = this.Bind<PartFailErrorTypeResource>();

            var result = this.facadeService.Add(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}