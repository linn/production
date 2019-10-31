namespace Linn.Production.Domain.LinnApps.BoardTests
{
    using System;

    public class BoardTest
    {
        public string BoardName { get; set; }

        public string BoardSerialNumber { get; set; }

        public int Seq { get; set; }

        public DateTime DateTested { get; set; }

        public string TimeTested { get; set; }

        public string Status { get; set; }

        public BoardFailType FailType { get; set; }

        public string TestMachine { get; set; }
    }
}
