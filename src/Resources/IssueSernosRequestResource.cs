namespace Linn.Production.Resources
{
    public class IssueSernosRequestResource
    {
        public int DocumentNumber { get; set; }

        public string DocumentType { get; set; }

        public int DocumentLine { get; set; }

        public string PartNumber { get; set; }

        public int CreatedBy { get; set; }

        public int Quantity { get; set; }

         public int? FirstSerialNumber { get; set; }
    }
}