﻿namespace Server.DataBase
{
    using System.ComponentModel.DataAnnotations;
    public class EventLog
    {
        #region Properties

        [Key]
        public int Id { get; set; }
        public string Identificator { get; set; }
        public string Message { get; set;}

        #endregion Properties

    }
}
