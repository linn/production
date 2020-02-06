namespace Linn.Production.Resources
{
    public class BuildSernosRequestResource
    {
        public string PartNumber { get; set; }

        public int OrderNumber { get; set; }

        public int FromSerial { get; set; }

        public int ToSerial { get; set; }
    }
}