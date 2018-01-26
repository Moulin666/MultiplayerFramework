namespace MGF.DataEntities
{
    public class Stat
    {
        public int StatId { get; set; }
        public string Name { get; set; }

        public int CharacterId { get; set; }
        public Character Character { get; set; }

        public int Value { get; set; }
    }
}
