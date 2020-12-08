namespace Common.Network
{
    using System.Data.Entity;
    using Common.DbEF;
    public class DBChat : DbContext
    {
        #region Properties

        public DbSet<Chats> PoolChat { get; set; }
        public DbSet<ClientsInChats> ClientsInChats { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<PoolClients> PoolClients { get; set; }

        public DbSet<EventLog> ServerLog { get; set; }

        #endregion Properties

    }
}
