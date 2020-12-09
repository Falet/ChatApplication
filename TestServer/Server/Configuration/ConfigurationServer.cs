namespace Server.Configuration
{
	using System.IO;
	using Newtonsoft.Json;
	public static class  ConfigurationServer
	{
		#region Methods

		public static ConfigServer ReadConfigFromFile(string pathToFile)
		{
			using (StreamReader readingFile = new StreamReader(pathToFile))
			{
				string allConfigFromFile = readingFile.ReadToEnd();
				return JsonConvert.DeserializeObject<ConfigServer>(allConfigFromFile);
			}
		}

		#endregion Methods
	}
}
