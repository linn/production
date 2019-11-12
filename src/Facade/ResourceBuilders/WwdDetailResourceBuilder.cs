namespace Linn.Production.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class WwdDetailResourceBuilder : IResourceBuilder<WwdDetail>
    {
        public object Build(WwdDetail detail)
        {
            return new WwdDetailResource
            {
                PartNumber = detail.PartNumber,
                Description = detail.Description,
                Remarks = detail.Remarks,
                StoragePlace = detail.StoragePlace,
                QtyKitted = detail.QtyKitted,
                QtyReserved = detail.QtyReserved,
                QtyAtLocation = detail.QtyAtLocation
            };
        }

        public WwdDetailResource BuildDetail(WwdDetail detail)
        {
            return (WwdDetailResource) this.Build(detail);
        }

        public string GetLocation(WwdDetail model)
        {
            throw new System.NotImplementedException();
        }
    }
}