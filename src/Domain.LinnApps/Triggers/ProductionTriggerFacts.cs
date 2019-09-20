﻿namespace Linn.Production.Domain.LinnApps.Triggers
{
    using System.Collections.Generic;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    public class ProductionTriggerFacts
    {
        public ProductionTriggerFacts(ProductionTrigger trigger)
        {
            this.Trigger = trigger;
        }

        public ProductionTrigger Trigger { get; set; }

        public IEnumerable<WorksOrder> OutstandingWorksOrders { get; set; }
    }
}