namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class EmployeesResponseProcessor : JsonResponseProcessor<IEnumerable<Employee>>
    {
        public EmployeesResponseProcessor(IResourceBuilder<IEnumerable<Employee>> resourceBuilder)
            : base(resourceBuilder, "employees", 1)
        {
        }
    }
}