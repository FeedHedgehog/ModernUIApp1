using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ModernUI.Common.Utility.TransferBlock
{
    public interface IUdpNet
    {
        void OnReceive(IPEndPoint ep, byte[] buffer);

        void OnSent(IPEndPoint ep, byte[] buffer);

        void OnException(Exception ex);
    }
}
