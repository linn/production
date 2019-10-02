namespace Linn.Production.Resources
{
    public class PartFailResource
    {
        public int Id { get; set; }

        public string DateCreated { get; set; }

        public int EnteredBy { get; set; }

        public string EnteredByName { get; set; }

        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public int? Quantity { get; set; }

        public string Batch { get; set; }

        public string FaultCode { get; set; }

        public string FaultDescription { get; set; }

        public string ErrorType { get; set; }

        public string Story { get; set; }

        public string WorksOrderNumber { get; set; }

        public string PurchaseOrderNumber { get; set; }

        public string StoragePlace { get; set; }

        public int MinutesWasted { get; set; }
    }
}