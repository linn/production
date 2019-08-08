using Linn.Common.Facade;
using Linn.Production.Domain.LinnApps;
using Linn.Production.Resources;
using Linn.Production.Service.Models;
using Nancy;
using Nancy.ModelBinding;

namespace Linn.Production.Service.Modules
{
    public sealed class BoardFailTypesModule : NancyModule
    {
        private readonly IFacadeService<BoardFailType, int, BoardFailTypeResource, BoardFailTypeResource> facadeService;

        public BoardFailTypesModule(
            IFacadeService<BoardFailType, int, BoardFailTypeResource, BoardFailTypeResource> facadeService)
        {
            this.facadeService = facadeService;
            this.Get("/production/resources/board-fail-types", _ => this.GetAll());
            this.Get("/production/resources/board-fail-types/{type}", parameters => this.GetById(parameters.type));
            this.Put("/production/resources/board-fail-types/{type}", parameters => this.Update(parameters.type));
            this.Post("/production/resources/board-fail-types", parameters => this.Add());
        }

        private object GetAll()
        {
            var result = this.facadeService.GetAll();
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetById(int id)
        {
            var result = this.facadeService.GetById(id);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object Update(int type)
        {
            var resource = this.Bind<BoardFailTypeResource>();

            var result = this.facadeService.Update(type, resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
        private object Add()
        {
            var resource = this.Bind<BoardFailTypeResource>();

            var result = this.facadeService.Add(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}