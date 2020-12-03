namespace TestServer.DbEF
{
    using System.ComponentModel.DataAnnotations;
    public class EventLog
    {
        [Key]
        public int Id { get; set; }
        public string Actor { get; set; }
        public string Message { get; set;}
    }
}
