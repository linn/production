namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Resources;

    public class EmployeeResourceBuilder : IResourceBuilder<Employee>
    {
        public EmployeeResource Build(Employee employee)
        {
            return new EmployeeResource
                       {
                           Id = employee.Id,
                           FullName = employee.FullName
                       };
        }

        public string GetLocation(Employee employee)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<Employee>.Build(Employee employee) => this.Build(employee);

        private IEnumerable<LinkResource> BuildLinks(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}