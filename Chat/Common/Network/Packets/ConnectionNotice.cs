﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Network.Packets
{
    public class ConnectionNotice
    {
        #region Properties

        public string NameClient { get; }

        #endregion Properties

        #region Constructors

        public ConnectionNotice(string nameClient)
        {
            NameClient = nameClient;
        }

        #endregion Constructors
    }
}
