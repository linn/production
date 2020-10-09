namespace Linn.Production.Resources.RequestResources
{
    using System;

    public class AssemblyFailsSearchRequestResource : SearchRequestResource
    {
        public string PartNumber { get; set; }

        public int? ProductId { get; set; }

        public string Date { get; set; }

        public string BoardPart { get; set; }

        public string CircuitPart { get; set; }
    }
}