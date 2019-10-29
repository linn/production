namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Resources.RequestResources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class PartFailsModule : NancyModule
    {
        private readonly IFacadeService<PartFail, int, PartFailResource, PartFailResource> partFailService;

        private readonly IFacadeService<PartFailFaultCode, string, PartFailFaultCodeResource, PartFailFaultCodeResource>
            faultCodeService;

        private readonly IFacadeService<PartFailErrorType, string, PartFailErrorTypeResource, PartFailErrorTypeResource>
            errorTypeService;

        private readonly IPartsReportFacadeService partsReportFacadeService;

        public PartFailsModule(
            IFacadeService<PartFail, int, PartFailResource, PartFailResource> partFailService, 
            IFacadeService<PartFailFaultCode, string, PartFailFaultCodeResource, PartFailFaultCodeResource> faultCodeService, 
            IFacadeService<PartFailErrorType, string, PartFailErrorTypeResource, PartFailErrorTypeResource> errorTypeService,
            IPartsReportFacadeService partsReportFacadeService)
        {
            this.partFailService = partFailService;
            this.errorTypeService = errorTypeService;
            this.partsReportFacadeService = partsReportFacadeService;
            this.faultCodeService = faultCodeService;

            this.Get("/production/quality/part-fails/{id*}", parameters => this.GetById(parameters.id));
            this.Post("/production/quality/part-fails", _ => this.Add());
            this.Get("/production/quality/part-fails", _ => this.Search());
            this.Put("/production/quality/part-fails/{id*}", parameters => this.Update(parameters.id));

            this.Get("/production/quality/part-fail-error-types", _ => this.GetErrorTypes());
            this.Get("/production/quality/part-fail-error-types/{type*}", parameters => this.GetErrorType(parameters.type));
            this.Put("/production/quality/part-fail-error-types/{type*}", parameters => this.UpdateErrorType(parameters.type));
            this.Post("/production/quality/part-fail-error-types", parameters => this.AddErrorType());

            this.Get("/production/quality/part-fail-fault-codes", _ => this.GetFaultCodes());
            this.Get("/production/quality/part-fail-fault-codes/{code*}", parameters => this.GetFaultCode(parameters.code));
            this.Put("/production/quality/part-fail-fault-codes/{code*}", parameters => this.UpdateFaultCode(parameters.code));
            this.Post("/production/quality/part-fail-fault-codes", parameters => this.AddFaultCode());

            this.Get("/production/quality/part-fails/detail-report", _ => this.GetPartFailsDetailReportOptions());
            this.Get("/production/quality/part-fails/detail-report/report", _ => this.GetPartFailsDetailReport());
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

        private object GetErrorTypes()
        {
            var result = this.errorTypeService.GetAll();
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetErrorType(string id)
        {
            var result = this.errorTypeService.GetById(id);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateErrorType(string type)
        {
            var resource = this.Bind<PartFailErrorTypeResource>();

            var result = this.errorTypeService.Update(type, resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object AddErrorType()
        {
            var resource = this.Bind<PartFailErrorTypeResource>();

            var result = this.errorTypeService.Add(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetFaultCodes()
        {
            var result = this.faultCodeService.GetAll();
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetFaultCode(string id)
        {
            var result = this.faultCodeService.GetById(id);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateFaultCode(string type)
        {
            var resource = this.Bind<PartFailFaultCodeResource>();

            var result = this.faultCodeService.Update(type, resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object AddFaultCode()
        {
            var resource = this.Bind<PartFailFaultCodeResource>();

            var result = this.faultCodeService.Add(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetPartFailsDetailReportOptions()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }

        private object GetPartFailsDetailReport()
        {
            var resource = this.Bind<PartFailDetailsReportRequestResource>();
            var results = this.partsReportFacadeService.GetPartFailDetailsReport(
                resource.SupplierId,
                resource.FromWeek,
                resource.ToWeek,
                resource.ErrorType,
                resource.FaultCode,
                resource.PartNumber,
                resource.Department);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}