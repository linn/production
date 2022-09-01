namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;
    using Linn.Production.Resources.RequestResources;

    public interface IWorksOrdersService : IFacadeFilterService<WorksOrder, int, WorksOrderResource, WorksOrderResource, WorksOrderRequestResource>
    {
        IResult<WorksOrder> AddWorksOrder(WorksOrderResource resource);

        IResult<WorksOrder> UpdateWorksOrder(WorksOrderResource resource);

        IResult<IEnumerable<WorksOrder>> SearchByBoardNumber(
            string boardNumber, 
            int? limit, 
            string orderByDesc);

        IResult<WorksOrderPartDetails> GetWorksOrderPartDetails(string partNumber);
    }
}
