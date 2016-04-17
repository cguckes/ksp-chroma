using System;
using System.Net;
using KSP_Chroma_Control.ColorSchemes;
using System.Net.Sockets;
using System.Text;

namespace KSP_Chroma_Control
{
    internal class UdpDrain : DataDrain
    {
        private IPAddress address;
        private int port;
        private UdpClient client;

        public UdpDrain(IPAddress address, int port)
        {
            this.address = address;
            this.port = port;

            this.client = new UdpClient();
            IPEndPoint ep = new IPEndPoint(address, port);
            this.client.Connect(ep);
        }

        ~UdpDrain()
        {
            this.client.Close();
        }

        public void send(ColorScheme scheme)
        {
            byte[] ToSend = Encoding.UTF8.GetBytes(scheme.ToString());
            this.client.Send(ToSend, ToSend.Length);
        }
    }
}