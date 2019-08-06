﻿namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
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

        public string GetLocation(Department department)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<Department>.Build(Department department) => this.Build(department);

        private IEnumerable<LinkResource> BuildLinks(Department department)
        {
            throw new NotImplementedException();
        }
    }
}