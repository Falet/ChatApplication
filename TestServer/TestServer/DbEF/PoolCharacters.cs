namespace TestServer.DbEF
{
    using System.ComponentModel.DataAnnotations;
    public class PoolCharacters
    {
        [Key]
        public string CharacterID { get; set; }
    }
}
