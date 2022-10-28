namespace Linn.Production.Domain.LinnApps
{
    using System.Collections.Generic;

    public class Node
    {
        public string Name { get; set; }

        public List<Node> Children { get; set; }

        public Node Parent { get; set; }
    }
}
