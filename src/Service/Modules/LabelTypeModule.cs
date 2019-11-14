namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;

    public sealed class LabelTypeModule : NancyModule
    {
        private readonly IFacadeService<LabelType, string, LabelTypeResource, LabelTypeResource> labelTypeService;

        public LabelTypeModule(IFacadeService<LabelType, string, LabelTypeResource, LabelTypeResource> labelTypeService)
        {
            this.labelTypeService = labelTypeService;
            this.Get("/production/resources/label-types", _ => this.GetAll());
            this.Get("/production/resources/label-types/{labelTypeCode*}", parameters => this.GetById(parameters.labelTypeCode));
            this.Put("/production/resources/label-types/{labelTypeCode*}", parameters => this.UpdateLabelType(parameters.labelTypeCode));
            this.Post("/production/resources/label-types", parameters => this.AddLabelType());
        }

        private object GetAll()
        {
            var result = this.labelTypeService.GetAll();
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetById(string labelTypeCode)
        {
            var result = this.labelTypeService.GetById(labelTypeCode);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateLabelType(string labelTypeCode)
        {
            var resource = this.Bind<LabelTypeResource>();

            var result = this.labelTypeService.Update(labelTypeCode, resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object AddLabelType()
        {
            var resource = this.Bind<LabelTypeResource>();

            var result = this.labelTypeService.Add(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
