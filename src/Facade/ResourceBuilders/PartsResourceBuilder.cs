namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class PartsResourceBuilder : IResourceBuilder<ResponseModel<IEnumerable<Part>>>
    {
        private readonly PartResourceBuilder partResourceBuilder;

        public PartsResourceBuilder(IAuthorisationService authorisationService)
        {
            this.partResourceBuilder = new PartResourceBuilder(authorisationService);
        }

        public IEnumerable<PartResource> Build(ResponseModel<IEnumerable<Part>> model)
        {
            var parts = model.ResponseData;

            return parts
                .OrderBy(b => b.PartNumber)
                .Select(a => this.partResourceBuilder.Build(new ResponseModel<Part>(a, model.Privileges)));
        }

        object IResourceBuilder<ResponseModel<IEnumerable<Part>>>.Build(ResponseModel<IEnumerable<Part>> parts) => this.Build(parts);

        public string GetLocation(ResponseModel<IEnumerable<Part>> model)
        {
            throw new NotImplementedException();
        }
    }
}