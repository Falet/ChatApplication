namespace TestServer.DbEF
{
    using System.ComponentModel.DataAnnotations;
    public class Characters
    {
        [Key]
        public int id { get; set; }
        public string CharacterID { get; set; }
        public int RoomID { get; set; }
    }
}
