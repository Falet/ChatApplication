using System;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;

namespace TestServer.Network
{
    class ConfigurationSetver
    {
        #region Fields

        #endregion Fields

        #region Properties

        public TransportType Protocol { get; set; }
        public int Port { get; set; }

        #endregion Properties

        #region Constructors
        public ConfigurationSetver(TypeGettingConfig type)
        {

        }
        public ConfigurationSetver(TypeGettingConfig type, string TextForNavigation)
        {
            switch (type)
            {
                case TypeGettingConfig.File:
                    ReadConfigFromFile(TextForNavigation);
                    break;
                
                case TypeGettingConfig.Db:
                    break;
            }
        }
        #endregion Constructors

        #region Methods
        private void ReadConfigFromFile(string pathToFile)
        {
            try
            {
                using (StreamReader ReadingFile = new StreamReader(TextForNavigation))
                {
                    while (!ReadingFile.EndOfStream)
                    {
                        string lineOfConfig = ReadingFile.ReadLine();
                        string[] optionsAndValue = lineOfConfig.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        switch (optionsAndValue[0])
                        {
                            case "Protocol":
                                {
                                    int valueProtocol = 0;
                                    if (Int32.TryParse(optionsAndValue[1], out valueProtocol))
                                    {
                                        Protocol = (TransportType)valueProtocol;
                                    }
                                    break;
                                }

                            case "Port":
                                {
                                    int valuePort = 0;
                                    if (Int32.TryParse(optionsAndValue[1], out valuePort))
                                    {
                                        Port = valuePort;
                                    }
                                    break;
                                }
                            default:
                                Console.WriteLine("Отсутствует совпадение");//(потом)Вызов исключения
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion Methods
    }
}
