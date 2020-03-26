namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class PartCadInfosResourceBuilder : IResourceBuilder<ResponseModel<IEnumerable<PartCadInfo>>>
    {
        private readonly PartCadInfoResourceBuilder partCadInfoResourceBuilder;

        public PartCadInfosResourceBuilder(IAuthorisationService authorisationService)
        {
            this.partCadInfoResourceBuilder = new PartCadInfoResourceBuilder(authorisationService);
        }

        public IEnumerable<PartCadInfoResource> Build(ResponseModel<IEnumerable<PartCadInfo>> model)
        {
            var partCadInfos = model.ResponseData;

            return partCadInfos.Select(
                p => this.partCadInfoResourceBuilder.Build(new ResponseModel<PartCadInfo>(p, model.Privileges)));
        }

        public string GetLocation(ResponseModel<IEnumerable<PartCadInfo>> model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<ResponseModel<IEnumerable<PartCadInfo>>>.Build(ResponseModel<IEnumerable<PartCadInfo>> partCadInfos) =>
            this.Build(partCadInfos);
    }
}