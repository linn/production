namespace Linn.Production.Resources
{
    public class BoardTestResource
    {
        public string BoardName { get; set; }

        public string BoardSerialNumber { get; set; }

        public int Seq { get; set; }

        public string DateTested { get; set; }

        public string TimeTested { get; set; }

        public string Status { get; set; }

        public string FailType { get; set; }

        public string TestMachine { get; set; }
    }
}