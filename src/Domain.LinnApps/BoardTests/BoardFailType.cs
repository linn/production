namespace Linn.Production.Domain.LinnApps.BoardTests
{
    using System.Collections.Generic;

    public class BoardFailType
    {
        public int Type { get; set; }

        public string Description { get; set; }

        // entity framework
        public IEnumerable<BoardTest> BoardTests { get; protected set; }
    }
}