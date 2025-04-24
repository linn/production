namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ManufacturingSkillsModule : NancyModule
    {
        private readonly IFacadeFilterService<ManufacturingSkill, string, ManufacturingSkillResource, ManufacturingSkillResource, ManufacturingSkillResource> manufacturingSkillFacadeService;

        public ManufacturingSkillsModule(IFacadeFilterService<ManufacturingSkill, string, ManufacturingSkillResource, ManufacturingSkillResource, ManufacturingSkillResource> manufacturingSkillFacadeService)
        {
            this.manufacturingSkillFacadeService = manufacturingSkillFacadeService;

            this.Get("/production/resources/manufacturing-skills", _ => this.GetManufacturingSkills());
            this.Get("/production/resources/manufacturing-skills/{skillCode*}", parameters => this.GetById(parameters.skillCode));
            this.Put("/production/resources/manufacturing-skills/{skillCode*}", parameters => this.UpdateManufacturingSkill(parameters.skillCode));
            this.Post("/production/resources/manufacturing-skills", parameters => this.AddManufacturingSkill());
        }

        private object GetManufacturingSkills()
        {
            var resource = this.Bind<ManufacturingSkillResource>();
            var result = this.manufacturingSkillFacadeService.FilterBy(resource);
            
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetById(string skillCode)
        {
            var result = this.manufacturingSkillFacadeService.GetById(skillCode);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateManufacturingSkill(string skillCode)
        {
            var resource = this.Bind<ManufacturingSkillResource>();

            var result = this.manufacturingSkillFacadeService.Update(skillCode, resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object AddManufacturingSkill()
        {
            var resource = this.Bind<ManufacturingSkillResource>();

            var result = this.manufacturingSkillFacadeService.Add(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
