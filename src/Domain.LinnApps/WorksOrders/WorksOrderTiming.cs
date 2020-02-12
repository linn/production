namespace Linn.Production.Domain.LinnApps.WorksOrders
{
    using System;

    public class WorksOrderTiming
    {
                public int OrderNumber { get; set; }

                public int OperationNumber { get; set; }

                public string OperationType { get; set; }

                public int BuiltBy { get; set; }

                public string ResourceCode { get; set; }

                public string RouteCode { get; set; }

                public DateTime StartTime { get; set; }

                public DateTime EndTime { get; set; }

                public int TimeTaken { get; set; }
    }
}
