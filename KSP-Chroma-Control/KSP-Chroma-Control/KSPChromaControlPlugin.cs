using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;


namespace KSP_Chroma_Control
{
/*    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    class KSPChromaControlPlugin : MonoBehaviour
    {
        public static UdpClient Client;
        
        void Awake()
        {
            Debug.LogError("Starting network client");
            if(Client == null)
            {
                Client = new UdpClient();
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
                Client.Connect(ep);
            }
        }

        void Destroy()
        {
            if(Client != null)
            {
                Client.Close();
            }
        }
    }*/
}
