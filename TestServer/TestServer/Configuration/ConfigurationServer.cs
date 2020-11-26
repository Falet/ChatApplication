namespace TestServer.Network
{
	using System.IO;
	using System.Text.Json;
	using Newtonsoft.Json;
	class ConfigurationServer
	{
		#region Fields

		#endregion Fields


		#region Constructors
		public ConfigurationServer()
		{
		
		}
		#endregion Constructors

		#region Methods
		public ConfigServer ReadConfigFromFile(string pathToFile)
		{
			using (StreamReader ReadingFile = new StreamReader(pathToFile))
			{
				string allConfigFromFile = ReadingFile.ReadToEnd();
				return JsonConvert.DeserializeObject<ConfigServer>(allConfigFromFile);
			}
		}

		private void ReadConfigFromConsole()
		{

		}
		#endregion Methods
	}
}
