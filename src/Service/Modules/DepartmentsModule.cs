namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;

    public sealed class DepartmentsModule : NancyModule
    {
        private readonly IFacadeService<Department, string, DepartmentResource, DepartmentResource> departmentService;

        public DepartmentsModule(IFacadeService<Department, string, DepartmentResource, DepartmentResource> departmentService)
        {
            this.departmentService = departmentService;
            this.Get("/production/resources/departments", _ => this.GetPersonnelDepartments());
        }

        private object GetPersonnelDepartments()
        {
            var result = this.departmentService.Search("Y");
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}