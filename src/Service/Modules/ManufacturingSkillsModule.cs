using Linn.Production.Domain.LinnApps;
using Linn.Production.Facade.Services;

namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ManufacturingSkillsModule : NancyModule
    {
        private readonly IFacadeService<ManufacturingSkill, string, ManufacturingSkillResource, ManufacturingSkillResource> manufacturingSkillService;

        public ManufacturingSkillsModule(IFacadeService<ManufacturingSkill, string, ManufacturingSkillResource, ManufacturingSkillResource> manufacturingSkillService)
        {
            this.manufacturingSkillService = manufacturingSkillService;
            this.Get("/production/manufacturing-skills", _ => this.GetAll());
            this.Get("production/manufacturing-skills/{skillCode*}", parameters => this.GetById(parameters.skillCode));
            this.Put("production/manufacturing-skills/{skillCode*}", parameters => this.UpdateManufacturingSkill(parameters.skillCode));
            this.Post("production/manufacturing-skills", parameters => this.AddManufacturingSkill());
        }

        private object GetAll()
        {
            var result = this.manufacturingSkillService.GetAll();
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetById(string skillCode)
        {
            var result = this.manufacturingSkillService.GetById(skillCode);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateManufacturingSkill(string skillCode)
        {
            var resource = this.Bind<ManufacturingSkillResource>();

            var result = this.manufacturingSkillService.Update(skillCode, resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
        private object AddManufacturingSkill()
        {
            var resource = this.Bind<ManufacturingSkillResource>();

            var result = this.manufacturingSkillService.Add(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}