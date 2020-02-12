namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public interface IWorksOrderTimingsService : IFacadeService<WorksOrderTiming, int, WorksOrderTimingResource, WorksOrderTimingResource>
    {
        IResult<IEnumerable<WorksOrderTiming>> SearchByDates(
            DateTime startDate,
            DateTime endDate);
    }
}
