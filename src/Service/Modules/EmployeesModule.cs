namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class EmployeesModule : NancyModule
    {
        private readonly IFacadeService<Employee, int, EmployeeResource, EmployeeResource> employeeService;

        public EmployeesModule(
            IFacadeService<Employee, int, EmployeeResource, EmployeeResource> productionTriggerLevelsService)
        {
            this.employeeService = productionTriggerLevelsService;

            this.Get("production/maintenance/employees", _ => this.GetEmployees());
        }

        private object GetEmployees()
        {
            var parts = this.employeeService.GetAll();


            return this.Negotiate.WithModel(parts).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}