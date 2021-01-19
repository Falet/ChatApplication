﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Network.Packets
{
    public class DisconnectNotice
    {
        #region Properties

        public string NameClient { get; }


        #endregion Properties

        #region Constructors

        public DisconnectNotice(string nameClient)
        {
            NameClient = nameClient;
        }

        #endregion Constructors
    }
}
