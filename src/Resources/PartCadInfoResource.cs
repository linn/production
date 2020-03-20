﻿namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class PartCadInfoResource : HypermediaResource
    {
        public int MsId { get; set; }

        public string PartNumber { get; set; }

        public string FootprintRef { get; set; }

        public string LibraryRef { get; set; }

        public string Description { get; set; }

        public string LibraryName { get; set; }
    }
}