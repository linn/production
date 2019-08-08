using System;
using System.Collections.Generic;
using Linn.Common.Facade;
using Linn.Common.Resources;
using Linn.Production.Domain.LinnApps;
using Linn.Production.Resources;

namespace Linn.Production.Facade.ResourceBuilders
{
    public class BoardFailTypeResourceBuilder : IResourceBuilder<BoardFailType>
    {
        public object Build(BoardFailType failType)
        {
            return new BoardFailTypeResource
            {
                FailType = failType.Type,
                Description = failType.Description
            };
        }

        public string GetLocation(BoardFailType failType)
        {
            return $"/production/quality/board-fail-types/{failType.Type}";
        }

        object IResourceBuilder<BoardFailType>.Build(BoardFailType failType) => this.Build(failType);

        private IEnumerable<LinkResource> BuildLinks(BoardFailType failType)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(failType) };
        }
    }
}