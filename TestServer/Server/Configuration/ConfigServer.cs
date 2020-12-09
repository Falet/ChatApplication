namespace Server.Configuration
{
    using Newtonsoft.Json;
    using Common.Network;
    public class ConfigServer
    {
        #region Properties

        [JsonProperty]
        public TypeTransport Protocol { private set;  get; }
        [JsonProperty]
        public int Port { private set; get; }

        #endregion Properties
    }
}
