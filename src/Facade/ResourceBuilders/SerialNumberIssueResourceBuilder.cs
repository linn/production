namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Domain.LinnApps.SerialNumberIssue;
    using Resources;

    public class SerialNumberIssueResourceBuilder : IResourceBuilder<SerialNumberIssue>
    {
        public object Build(SerialNumberIssue serialNumberIssue)
        {
            return new SerialNumberIssueResource
            {
                SernosGroup = serialNumberIssue.SernosGroup,
                Id = serialNumberIssue.Id,
                NewSerialNumber = serialNumberIssue.NewSerialNumber,
                SerialNumber = serialNumberIssue.SerialNumber,
                NewArticleNumber = serialNumberIssue.NewArticleNumber,
                ArticleNumber = serialNumberIssue.ArticleNumber,
                CreatedBy = serialNumberIssue.CreatedBy,
                Comments = serialNumberIssue.Comments,
                Links = this.BuildLinks(serialNumberIssue).ToArray()
            };
        }

        private IEnumerable<LinkResource> BuildLinks(SerialNumberIssue serialNumberIssue)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(serialNumberIssue) };
        }

        object IResourceBuilder<SerialNumberIssue>.Build(SerialNumberIssue serialNumberIssue) =>
            this.Build(serialNumberIssue);

        public string GetLocation(SerialNumberIssue serialNumberIssue)
        {
            return $"/production/maintenance/serial-number-issue/{serialNumberIssue.Id}";
        }
    }
}
