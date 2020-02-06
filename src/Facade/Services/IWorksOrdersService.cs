namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;
    using System.Globalization;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public interface IWorksOrdersService : IFacadeService<WorksOrder, int, WorksOrderResource, WorksOrderResource>
    {
        IResult<WorksOrder> AddWorksOrder(WorksOrderResource resource);

        IResult<WorksOrder> UpdateWorksOrder(WorksOrderResource resource);

        IResult<IEnumerable<WorksOrder>> SearchByBoardNumber(string boardNumber, int limit, string orderByDesc);

        IResult<WorksOrderPartDetails> GetWorksOrderPartDetails(string partNumber);
    }
}
