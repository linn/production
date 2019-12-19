namespace Linn.Production.Domain.LinnApps
{
    using System.Collections.Generic;

    public class IdAndName
    {
        public IdAndName(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; }

        public string Name { get; }
    }
}
