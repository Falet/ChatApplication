namespace TestServer.Network
{
	using System.IO;
	using System.Text.Json;
	using Newtonsoft.Json;
	public static class  ConfigurationServer
	{
		#region Methods
		public static ConfigServer ReadConfigFromFile(string pathToFile)
		{
			using (StreamReader ReadingFile = new StreamReader(pathToFile))
			{
				string allConfigFromFile = ReadingFile.ReadToEnd();
				return JsonConvert.DeserializeObject<ConfigServer>(allConfigFromFile);
			}
		}

		private static void ReadConfigFromConsole()
		{

		}
		#endregion Methods
	}
}
