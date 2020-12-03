namespace TestServer.DbEF
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    public class PoolCharacters
    {
        [Key]
        public string CharacterID { get; set; }
        public List<Characters> Users { get; set; }
    }
}
