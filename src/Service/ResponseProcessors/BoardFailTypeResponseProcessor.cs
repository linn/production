namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.BoardTests;

    public class BoardFailTypeResponseProcessor : JsonResponseProcessor<BoardFailType>
    {
        public BoardFailTypeResponseProcessor(IResourceBuilder<BoardFailType> resourceBuilder)
            : base(resourceBuilder, "board-fail-type", 1)
        {
        }
    }
}