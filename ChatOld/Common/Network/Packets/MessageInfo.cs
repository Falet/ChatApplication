namespace Common.Network
{
    using System;
    public class MessageInfo
    {
        #region Properties

        public string FromMessage { get; }
        public string Text { get; }
        public DateTime Time { get; }

        #endregion Properties

        public MessageInfo(string fromMessage, string text, DateTime time)
        {
            FromMessage = fromMessage;
            Text = text;
            Time = time;
        }
    }
}
