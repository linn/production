﻿namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class PartFailFaultCodeResource : HypermediaResource
    {
        public string FaultCode { get; set; }

        public string FaultDescription { get; set; }
    }
}