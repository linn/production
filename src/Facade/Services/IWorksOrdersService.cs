namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public interface IWorksOrdersService : IFacadeService<WorksOrder, int, WorksOrderResource, WorksOrderResource>
    {
        IResult<WorksOrder> AddWorksOrder(WorksOrderResource resource);

        IResult<WorksOrder> UpdateWorksOrder(WorksOrderResource resource);

        IResult<WorksOrderDetails> GetWorksOrderDetails(string partNumber);
    }
}
