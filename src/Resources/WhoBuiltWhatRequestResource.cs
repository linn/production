namespace Linn.Production.Resources
{
    using Linn.Production.Resources.RequestResources;

    public class WhoBuiltWhatRequestResource : FromToDateRequestResource
    {
        public string CitCode { get; set; }

        public int userNumber { get; set; }
    }
}