namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class LabelTypeResource : HypermediaResource
    {
        public string LabelTypeCode { get; set; }

        public string Description { get; set; }

        public string BarcodePrefix { get; set; }

        public string NSBarcodePrefix { get; set; }

        public string Filename { get; set; }

        public string DefaultPrinter { get; set; }

        public string CommandFilename { get; set; }

        public string TestFilename { get; set; }

        public string TestPrinter { get; set; }

        public string TestCommandFilename { get; set; }
    }
}
