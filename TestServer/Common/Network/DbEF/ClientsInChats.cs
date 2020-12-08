namespace Common.DbEF
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ClientsInChats
    {
        #region Properties

        [Key]
        [Column(Order = 1)]
        public string ClientID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int ChatID { get; set; }

        #endregion Properties

    }
}
