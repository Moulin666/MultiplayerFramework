using MGF.DataEntities;
using System.Data.Entity;

namespace MGF
{
#if MYSQL
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class MGFContext : DbContext
    {
        public MGFContext() : base("name=MySqlDbConnectionString") { }

#else
    public class MGFContext : DbContext
    {
        public MGFContext() : base("name=MsSqlDbConnectionString") { }

#endif

        // Define entities here
        public DbSet<Character> Characters { get; set; }
        public DbSet<Stat> Stats { get; set; }
    }
}
