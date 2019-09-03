namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public interface IWorksOrderService
    {
        IResult<WorksOrder> GetWorksOrder(int orderNumber);

        IResult<WorksOrder> AddWorksOrder(WorksOrderResource resource);

        IResult<WorksOrder> UpdateWorksOrder(WorksOrderResource resource);

        IResult<IEnumerable<WorksOrder>> SearchWorksOrders(string searchTerm);
    }
}
