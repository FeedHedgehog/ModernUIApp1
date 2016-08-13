using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace ModernUI.Common.Utility.TransferBlock
{
    public class UdpNetClient
    {
        private UdpClient udp = null;

        private IPEndPoint m_IPEndPoint = null;

        private IUdpNet m_IUdpNet = null;

        public UdpNetClient(IPEndPoint ep, IUdpNet iUdpNet)
        {
            m_IPEndPoint = ep;
            m_IUdpNet = iUdpNet;
        }

        private bool started;

        public bool Started
        {
            get { return started; }
        }

        private readonly object LockObj = new object();

        public bool Start()
        {
            lock (LockObj)
            {
                if (!started)
                {
                    try
                    {
                        udp = new UdpClient(m_IPEndPoint);
                        started = true;
                        udp.BeginReceive(new AsyncCallback(ReceiveEnd), udp);
                    }
                    catch (Exception ex)
                    {
                        started = false;
                        if (m_IUdpNet != null)
                        {
                            m_IUdpNet.OnException(ex);
                        }
                    }
                }
                return started;
            }
        }

        public void Stop()
        {
            lock (LockObj)
            {
                if (started)
                {
                    try
                    {
                        udp.Close();
                    }
                    catch (Exception ex)
                    {
                        if (m_IUdpNet != null)
                        {
                            m_IUdpNet.OnException(ex);
                        }
                    }
                    started = false;
                }
            }
        }

        private void ReceiveEnd(IAsyncResult ar)
        {
            UdpClient udpClient = ar.AsyncState as UdpClient;
            IPEndPoint ep = new IPEndPoint(System.Net.IPAddress.Any, m_IPEndPoint.Port);
            try
            {
                byte[] buffer = udpClient.EndReceive(ar, ref ep);
                if (buffer != null && buffer.Length > 0)
                {
                    if (m_IUdpNet != null)
                    {
                        m_IUdpNet.OnReceive(ep, buffer);
                    }
                    udpClient.BeginReceive(new AsyncCallback(this.ReceiveEnd), udpClient);
                }
            }
            catch //(Exception ex)
            {
                //if (m_IUdpNet != null)
                //{
                //    m_IUdpNet.OnException(ex);
                //}
            }
        }

        /// <summary>
        /// 同步发送数据
        /// </summary>
        /// <param name="buffer">数据</param>
        /// <param name="remoteEp">远端地址</param>
        public void Send(byte[] buffer, IPEndPoint remoteEp)
        {
            if (udp != null && buffer != null && buffer.Length > 0 && started)
            {
                //UdpSendData udpSendData = new UdpSendData();
                //udpSendData.Data = buffer;
                //udpSendData.RemoteEp = remoteEp;
                //udpSendData.Udper = udp;
                try
                {
                    int nSend = udp.Send(buffer, buffer.Length, remoteEp);
                    if (nSend > 0)
                    {
                        if (m_IUdpNet != null)
                        {
                            m_IUdpNet.OnSent(remoteEp, buffer);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (m_IUdpNet != null)
                    {
                        m_IUdpNet.OnException(ex);
                    }
                }
                //udp.BeginSend(buffer, buffer.Length, remoteEp, new AsyncCallback(SendEnd), udpSendData);
            }
        }

        //private void SendEnd(IAsyncResult ar)
        //{
        //    if (ar != null)
        //    {
        //        UdpSendData udpSendData = ar.AsyncState as UdpSendData;
        //        try
        //        {
        //            int nSend = udpSendData.Udper.EndSend(ar);
        //            if (nSend > 0)
        //            {
        //                if (m_IUdpNet != null)
        //                {
        //                    m_IUdpNet.OnSent(udpSendData.RemoteEp, udpSendData.Data);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            if (m_IUdpNet != null)
        //            {
        //                m_IUdpNet.OnException(ex);
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 224.0.0.0 到 239.255.255.255
        /// </summary>
        /// <param name="ipAddress">分组地址</param>
        public void JoinGroup(IPAddress ipAddress)
        {
            lock (LockObj)
            {
                if (started && udp != null)
                {
                    try
                    {
                        udp.JoinMulticastGroup(ipAddress, m_IPEndPoint.Address);
                    }
                    catch (Exception ex)
                    {
                        if (m_IUdpNet != null)
                        {
                            m_IUdpNet.OnException(ex);
                        }
                    }
                }
            }
        }

        public void DropGroup(IPAddress ipAddress)
        {
            lock (LockObj)
            {
                if (started && udp != null)
                {
                    try
                    {
                        udp.DropMulticastGroup(ipAddress);
                    }
                    catch (Exception ex)
                    {
                        if (m_IUdpNet != null)
                        {
                            m_IUdpNet.OnException(ex);
                        }
                    }
                }
            }
        }
    }

    //public class UdpSendData
    //{
    //    public UdpClient Udper { get; set; }

    //    public IPEndPoint RemoteEp { get; set; }

    //    public byte[] Data { get; set; }
    //}
}
