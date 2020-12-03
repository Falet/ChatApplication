namespace TestServer.DbEF
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Rooms
    {
        [Key]
        public int RoomID { get; set; }
        public string Type { get; set; }
        public string OwnerRoom { get; set; }
        public virtual List<Characters> ListCharacter { get; set; }
        public virtual List<Messages> PoolMessage { get; set; }
    }
}
