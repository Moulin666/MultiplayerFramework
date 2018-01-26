using MGF.DataEntities;

namespace MGF
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MGFContext entities = new MGFContext())
            {
                Character character = new Character() { Name = "King Arthur" };

                entities.Characters.Add(character);
                entities.SaveChanges();
            }
        }
    }
}
