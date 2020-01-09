namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Resources.RequestResources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class AteQualityModule : NancyModule
    {
        private readonly IFacadeService<AteFaultCode, string, AteFaultCodeResource, AteFaultCodeResource> ateFaultCodeService;

        private readonly IFacadeService<AteTest, int, AteTestResource, AteTestResource> ateTestService;

        private readonly IFacadeService<AteTestDetail, AteTestDetailKey, AteTestDetailResource, AteTestDetailResource> ateTestDetailService;

        private readonly IAteReportsFacadeService ateReportsFacadeService;

        public AteQualityModule(
            IFacadeService<AteFaultCode, string, AteFaultCodeResource, AteFaultCodeResource> ateFaultCodeService,
            IFacadeService<AteTest, int, AteTestResource, AteTestResource> ateTestService,
            IFacadeService<AteTestDetail, AteTestDetailKey, AteTestDetailResource, AteTestDetailResource> ateTestDetailService,
            IAteReportsFacadeService ateReportsFacadeService)
        {
            this.ateFaultCodeService = ateFaultCodeService;
            this.ateTestService = ateTestService;
            this.ateTestDetailService = ateTestDetailService;
            this.ateReportsFacadeService = ateReportsFacadeService;
            this.Get("/production/quality/ate/fault-codes/{faultCode*}", parameters => this.GetFaultCodeById(parameters.faultCode));
            this.Get("/production/quality/ate/fault-codes/", _ => this.GetAllFaultCodes());
            this.Put("/production/quality/ate/fault-codes/{faultCode*}", parameters => this.UpdateFaultCode(parameters.faultCode));
            this.Post("/production/quality/ate/fault-codes", _ => this.AddFaultCode());
            this.Get("/production/quality/ate-tests/{id}", parameters => this.GetTestById(parameters.id));
            this.Get("/production/quality/ate-tests", _ => this.SearchAteTests());
            this.Put("/production/quality/ate-tests/{id}", parameters => this.UpdateAteTest(parameters.id));
            this.Post("/production/quality/ate-tests", _ => this.AddAteTest());
            this.Put("/production/quality/ate-test-details/{id}", parameters => this.UpdateAteTestDetail(parameters.id));

            this.Get("/production/reports/ate/status", _ => this.GetApp());
            this.Get("/production/reports/ate/status/report", _ => this.GetStatusReport());
            this.Get("/production/reports/ate/details", _ => this.GetApp());
            this.Get("/production/reports/ate/details/report", _ => this.GetDetailsReport());
        }

        private object GetDetailsReport()
        {
            var resource = this.Bind<AteDetailsReportRequestResource>();

            var result = this.ateReportsFacadeService.GetDetailsReport(
                resource.FromDate,
                resource.ToDate,
                resource.SmtOrPcb,
                resource.PlaceFound,
                resource.Board,
                resource.Component,
                resource.FaultCode);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetStatusReport()
        {
            var resource = this.Bind<AteStatusReportRequestResource>();

            var result = this.ateReportsFacadeService.GetStatusReport(
                resource.FromDate,
                resource.ToDate,
                resource.SmtOrPcb,
                resource.PlaceFound,
                resource.GroupBy);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
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

        private object SearchAteTests()
        {
            var resource = this.Bind<SearchRequestResource>();

            var result = this.ateTestService.Search(resource.SearchTerm);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateAteTest(int id)
        {
            var resource = this.Bind<AteTestResource>();
            var result = this.ateTestService.Update(id, resource);
            return this.Negotiate.WithModel(result).WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object AddAteTest()
        {
            var resource = this.Bind<AteTestResource>();
            var result = this.ateTestService.Add(resource);
            return this.Negotiate.WithModel(result).WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object UpdateAteTestDetail(int id)
        {
            var resource = this.Bind<AteTestDetailResource>();
            var result = this.ateTestDetailService.Update(new AteTestDetailKey(), resource);
            return this.Negotiate.WithModel(result).WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object GetApp()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}