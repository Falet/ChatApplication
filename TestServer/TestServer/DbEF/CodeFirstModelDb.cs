namespace TestServer.Network
{
    using System.Data.Entity;
    using TestServer.DbEF;
    public class DBChat : DbContext
    {
        public DbSet<Rooms> PoolRoom { get; set; }
        public DbSet<Characters> Characters { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<PoolCharacters> PoolUsers { get; set; }

        public DbSet<EventLog> ServerLog { get; set; }
    }
}
