namespace Linn.Production.Domain.LinnApps
{
    public class LabelPrintResponse
    {
        public LabelPrintResponse(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }
}
