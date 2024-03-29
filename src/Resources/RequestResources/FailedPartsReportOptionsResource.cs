﻿namespace Linn.Production.Resources.RequestResources
{
    public class FailedPartsReportOptionsResource : CitCodeRequestResource
    {
        public string PartNumber { get; set; }

        public string OrderByDate { get; set; }

        public bool ExcludeLinnProduced { get; set; }

        public string VendorManager { get; set; }

        public string StockPoolCode { get; set; }
    }
}
