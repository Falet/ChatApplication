namespace Server.DataBase
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    public class PoolClients
    {
        #region Properties

        [Key]
        public string ClientID { get; set; }
        public List<ClientsInChats> Clients { get; set; }

        #endregion Properties
    }
}
