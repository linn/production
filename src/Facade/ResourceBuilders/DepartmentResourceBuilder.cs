using Linn.Production.Domain.LinnApps;

namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Resources;

    public class DepartmentResourceBuilder : IResourceBuilder<Department>
    {
        public DepartmentResource Build(Department department)
        {
            return new DepartmentResource
            {
                DepartmentCode = department.DepartmentCode,
                Description = department.Description
            };
        }

        public string GetLocation(Department ateFaultCode)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<Department>.Build(Department ateFaultCode) => this.Build(ateFaultCode);

        private IEnumerable<LinkResource> BuildLinks(Department ateFaultCode)
        {
            throw new NotImplementedException();
        }
    }
}