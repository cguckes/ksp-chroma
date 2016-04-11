using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;

namespace KSP_Chroma_Control
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class KSPChromaPlugin : MonoBehaviour
    {
        private ColorSchemes.ColorScheme Scheme;
        private UdpClient Client { get; set; }

        void Awake()
        {
            Client = new UdpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
            this.Client.Connect(ep);
        }

        void Start()
        {
            Debug.Log("Sending Keyboard Layout");

            switch (HighLogic.LoadedScene) {
                case GameScenes.MAINMENU:
                    this.Scheme = new ColorSchemes.LogoScheme();
                    break;
                case GameScenes.FLIGHT:
                    this.Scheme = new ColorSchemes.FlightScheme();
                    break;
                default:
                    this.Scheme = new ColorSchemes.ColorScheme(Color.black);
                    break;
            }

            byte[] ToSend = Encoding.UTF8.GetBytes(this.Scheme.ToString());
            this.Client.Send(ToSend, ToSend.Length);
        }

        void Update()
        {

        }

        void FixedUpdate()
        {
           
        }

        void OnDestroy()
        {
            
        }
    }
}
