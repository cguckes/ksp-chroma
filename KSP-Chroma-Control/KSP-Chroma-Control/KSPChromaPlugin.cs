using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;
using KSP_Chroma_Control.SceneManagers;

/// <summary>
/// Contains the chroma control plugin allowing Kerbal Space Program to communicate a keyboard
/// layout via UDP to a chroma udp server.
/// </summary>
namespace KSP_Chroma_Control
{
    /// <summary>
    /// The main class, managing the keyboard appearance for every kind of scene KSP
    /// uses.
    /// </summary>
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class KSPChromaPlugin : MonoBehaviour
    {
        /// <summary>
        /// The UDP network socket to send keyboard appearance orders to the server.
        /// </summary>
        private SceneManager flightSceneManager = new FlightSceneManager();
        private SceneManager vabSceneManager = new VABSceneManager();
        private DataDrain dataDrain;

        /// <summary>
        /// Called by unity during the launch of this addon.
        /// </summary>
        void Awake()
        {
            ///this.dataDrain = new UdpDrain(IPAddress.Loopback, 8888);
            this.dataDrain = new ColoreDrain();
        }

        /// <summary>
        /// Called by unity on every physics frame.
        /// </summary>
        void FixedUpdate()
        {
            ColorSchemes.ColorScheme scheme;

            switch (HighLogic.LoadedScene)
            {
                case GameScenes.FLIGHT:
                    scheme = this.flightSceneManager.getScheme();
                    break;
                case GameScenes.EDITOR:
                    scheme = this.vabSceneManager.getScheme();
                    break;
                default:
                    scheme = new ColorSchemes.LogoScheme();
                    break;
            }

            this.dataDrain.send(scheme);
        }
    }
}
