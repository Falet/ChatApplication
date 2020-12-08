namespace Common.Network
{
    using System;
    public class MessageInfoForDb
    {
        #region Properties

        public int NumberChat { get; set; }
        public string FromMessage { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }

        #endregion Properties

    }
}
