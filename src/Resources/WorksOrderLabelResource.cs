namespace Linn.Production.Resources
{
    using System.Dynamic;

    using Linn.Common.Resources;

    public class WorksOrderLabelResource : HypermediaResource
    {
        public string PartNumber { get; set; }

        public int Sequence { get; set; }

        public string LabelText { get; set; }
    }
}