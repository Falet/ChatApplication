using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network
{
    public class InfoAboutAllClientsEventArgs
    {
        #region Properties

        public string NameClient { get; }

        #endregion Properties

        #region Constructors

        public InfoAboutAllClientsEventArgs(string nameClient)
        {
            NameClient = nameClient;
        }

        #endregion Constructors
    }
}
