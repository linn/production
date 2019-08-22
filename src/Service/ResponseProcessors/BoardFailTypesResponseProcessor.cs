namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class BoardFailTypesResponseProcessor : JsonResponseProcessor<IEnumerable<BoardFailType>>
    {
        public BoardFailTypesResponseProcessor(IResourceBuilder<IEnumerable<BoardFailType>> resourceBuilder)
            : base(resourceBuilder, "board-fail-types", 1)
        {
        }

    }
}