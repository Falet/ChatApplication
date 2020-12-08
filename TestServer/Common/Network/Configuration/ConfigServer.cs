namespace Common.Network
{
    using Newtonsoft.Json;
    public class ConfigServer
    {
        #region Properties

        [JsonProperty]
        public TransportType Protocol { private set;  get; }
        [JsonProperty]
        public int Port { private set; get; }

        #endregion Properties
    }
}
