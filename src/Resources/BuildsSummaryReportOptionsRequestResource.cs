﻿namespace Linn.Production.Resources
{
    public class BuildsSummaryReportOptionsRequestResource
    {
        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public bool Monthly { get; set; }

        public string PartNumbers { get; set; }
    }
}