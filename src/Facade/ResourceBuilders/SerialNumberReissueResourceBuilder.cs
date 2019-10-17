namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.SerialNumberReissue;
    using Linn.Production.Resources;

    public class SerialNumberReissueResourceBuilder : IResourceBuilder<SerialNumberReissue>
    {
        public object Build(SerialNumberReissue serialNumberReissue)
        {
            return new SerialNumberReissueResource
            {
                SernosGroup = serialNumberReissue.SernosGroup,
                Id = serialNumberReissue.Id,
                NewSerialNumber = serialNumberReissue.NewSerialNumber,
                SerialNumber = serialNumberReissue.SerialNumber,
                NewArticleNumber = serialNumberReissue.NewArticleNumber,
                ArticleNumber = serialNumberReissue.ArticleNumber,
                CreatedBy = serialNumberReissue.CreatedBy,
                Comments = serialNumberReissue.Comments,
                Links = this.BuildLinks(serialNumberReissue).ToArray()
            };
        }

        public string GetLocation(SerialNumberReissue serialNumberReissue)
        {
            return $"/production/maintenance/serial-number-reissue/{serialNumberReissue.Id}";
        }

        object IResourceBuilder<SerialNumberReissue>.Build(SerialNumberReissue serialNumberReissue) =>
            this.Build(serialNumberReissue);

        private IEnumerable<LinkResource> BuildLinks(SerialNumberReissue serialNumberReissue)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(serialNumberReissue) };
        }
    }
}
