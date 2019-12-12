namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class AteQualityModule : NancyModule
    {
        private readonly IFacadeService<AteFaultCode, string, AteFaultCodeResource, AteFaultCodeResource> ateFaultCodeService;

        private readonly IFacadeService<AteTest, int, AteTestResource, AteTestResource> ateTestService;

        public AteQualityModule(
            IFacadeService<AteFaultCode, string, AteFaultCodeResource, AteFaultCodeResource> ateFaultCodeService,
            IFacadeService<AteTest, int, AteTestResource, AteTestResource> ateTestService)
        {
            this.ateFaultCodeService = ateFaultCodeService;
            this.ateTestService = ateTestService;
            this.Get("/production/quality/ate/fault-codes/{faultCode*}", parameters => this.GetFaultCodeById(parameters.faultCode));
            this.Get("/production/quality/ate/fault-codes/", _ => this.GetAllFaultCodes());
            this.Put("/production/quality/ate/fault-codes/{faultCode*}", parameters => this.UpdateFaultCode(parameters.faultCode));
            this.Post("/production/quality/ate/fault-codes", _ => this.AddFaultCode());
            this.Get("/production/quality/ate-test/{id}", parameters => this.GetTestById(parameters.id));
        }

        private object GetAllFaultCodes()
        {
            var result = this.ateFaultCodeService.GetAll();
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetFaultCodeById(string id)
        {
            var result = this.ateFaultCodeService.GetById(id);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object AddFaultCode()
        {
            var resource = this.Bind<AteFaultCodeResource>();

            var result = this.ateFaultCodeService.Add(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateFaultCode(string faultCode)
        {
            var resource = this.Bind<AteFaultCodeResource>();

            var result = this.ateFaultCodeService.Update(faultCode, resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetTestById(int id)
        {
            var result = this.ateTestService.GetById(id);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}