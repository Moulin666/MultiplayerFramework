using System.Collections.Generic;

namespace MGF.DataEntities
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Stat> Stats { get; set; }
    }
}
