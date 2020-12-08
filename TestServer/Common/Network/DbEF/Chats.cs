namespace Common.DbEF
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Chats
    {
        #region Properties

        [Key]
        public int ChatID { get; set; }
        public string Type { get; set; }
        public string OwnerChat { get; set; }
        public virtual List<ClientsInChats> ClientInChats { get; set; }
        public virtual List<Messages> PoolMessage { get; set; }

        #endregion Properties

    }
}
