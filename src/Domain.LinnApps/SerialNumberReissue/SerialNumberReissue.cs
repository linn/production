namespace Linn.Production.Domain.LinnApps.SerialNumberReissue
{
    public class SerialNumberReissue
    {
        public SerialNumberReissue(string sernosGroup, string articleNumber)
        {
            this.SernosGroup = sernosGroup;
            this.ArticleNumber = articleNumber;
        }

        public int Id { get; set; }

        public string SernosGroup { get; set; }

        public int SerialNumber { get; set; }

        public int NewSerialNumber { get; set; }

        public string Comments { get; set; }

        public int CreatedBy { get; set; }

        public string ArticleNumber { get; set; }

        public string NewArticleNumber { get; set; }
    }
}
