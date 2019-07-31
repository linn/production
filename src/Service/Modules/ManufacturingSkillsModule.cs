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

        }

        private object GetAll()
        {
            var result = this.manufacturingSkillService.GetAll();
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}