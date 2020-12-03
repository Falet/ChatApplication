using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public interface IGetData
    {
        public List<UserProperties> GetBeginData();
    }
}
