namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;

    public class BoardFailTypesResourceBuilder : IResourceBuilder<IEnumerable<BoardFailType>>
    {
        private readonly BoardFailTypeResourceBuilder boardFailTypeResourceBuilder = new BoardFailTypeResourceBuilder();

        public object Build(IEnumerable<BoardFailType> boardFailTypes)
        {
            return boardFailTypes.OrderBy(t => t.Type).Select(f => this.boardFailTypeResourceBuilder.Build(f));
        }

        object IResourceBuilder<IEnumerable<BoardFailType>>.Build(IEnumerable<BoardFailType> failTypes) => this.Build(failTypes);

        public string GetLocation(IEnumerable<BoardFailType> model)
        {
            throw new System.NotImplementedException();
        }
    }
}