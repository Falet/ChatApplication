namespace TestServer.DbEF
{
    using System.ComponentModel.DataAnnotations;
    public class Messages
    {
        [Key]
        public string MessageID { get; set; }
        public int RoomID { get; set; }
        public string From { get; set; }
        public string Text { get; set; }
        public string Time { get; set; }
    }
}
