namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class PartFailsModule : NancyModule
    {
        private readonly IFacadeService<PartFail, int, PartFailResource, PartFailResource> partFailService;

        public PartFailsModule(
            IFacadeService<PartFail, int, PartFailResource, PartFailResource> partFailService)
        {
            this.partFailService = partFailService;
            this.Get("/production/quality/part-fails/{id*}", parameters => this.GetById(parameters.id));
            this.Post("/production/quality/part-fails", _ => this.Add());
            this.Get("/production/quality/part-fails", _ => this.Search());
            this.Put("/production/quality/part-fails/{id*}", parameters => this.Update(parameters.id));
        }

        private object GetById(int id)
        {
            var result = this.partFailService.GetById(id);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object Add()
        {
            var resource = this.Bind<PartFailResource>();
            var result = this.partFailService.Add(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object Update(int type)
        {
            var resource = this.Bind<PartFailResource>();

            var result = this.partFailService.Update(type, resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object Search()
        {
            var resource = this.Bind<SearchRequestResource>();

            var result = this.partFailService.Search(resource.SearchTerm);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}