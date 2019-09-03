namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Resources;

    public class EmployeesResourceBuilder : IResourceBuilder<IEnumerable<Employee>>
    {
        private readonly EmployeeResourceBuilder citResourceBuilder = new EmployeeResourceBuilder();

        public IEnumerable<EmployeeResource> Build(IEnumerable<Employee> cits)
        {
            return cits.Select(a => this.citResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<Employee>>.Build(IEnumerable<Employee> cits) => this.Build(cits);

        public string GetLocation(IEnumerable<Employee> cits)
        {
            throw new NotImplementedException();
        }
    }
}