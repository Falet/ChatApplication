namespace TestServer.DbEF
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Characters
    {
        [Key]
        [Column(Order = 1)]
        public string CharacterID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int RoomID { get; set; }
    }
}
