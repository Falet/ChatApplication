namespace TestServer.Network
{
    using System.Text.Json.Serialization;
    using Newtonsoft.Json;
    class ConfigServer
    {
        #region Properties
        [JsonProperty]
        public TransportType Protocol { private set;  get; }
        [JsonProperty]
        public int Port { private set; get; }

        #endregion Properties
    }
}
