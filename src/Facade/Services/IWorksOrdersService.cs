﻿namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public interface IWorksOrdersService
    {
        IResult<WorksOrder> GetWorksOrder(int orderNumber);

        IResult<WorksOrder> AddWorksOrder(WorksOrderResource resource);

        IResult<WorksOrder> UpdateWorksOrder(WorksOrderResource resource);

        IResult<WorksOrder> CancelWorksOrder(WorksOrderResource resource);

        IResult<IEnumerable<WorksOrder>> SearchWorksOrders(string searchTerm);

        IResult<string> GetAuditDisclaimer(string partNumber);
    }
}