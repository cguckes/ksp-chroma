using System.Net.Sockets;
using System.Net;
using System.Linq;
using UnityEngine;
using KSP_Chroma_Control.SceneManagers;
using System.Collections.Generic;

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
        private List<DataDrain> dataDrains = new List<DataDrain>();

        /// <summary>
        /// Called by unity during the launch of this addon.
        /// </summary>
        void Awake()
        {
            this.dataDrains.Add(new ColoreDrain());
            
            GameEvents.VesselSituation.onLand.Add(callbackLanded);
        }

        private void callbackLanded(Vessel vessel, CelestialBody body)
        {
            if(vessel.situation == Vessel.Situations.SPLASHED)
                AnimationManager.Instance.setAnimation(new SplashdownAnimation());
        }

        private void callbackCrashed(EventReport report)
        {
            if (report.eventType == FlightEvents.SPLASHDOWN_CRASH)
                AnimationManager.Instance.setAnimation(new SplashdownAnimation());
            /*else if (report.eventType == FlightEvents.CRASH)
                AnimationManager.Instance.setAnimation(new CrashAnimation());*/
        }

        /// <summary>
        /// Called by unity on every physics frame.
        /// </summary>
        void Update()
        {
            ColorSchemes.ColorScheme scheme;

            if (AnimationManager.Instance.animationRunning())
            {
                scheme = AnimationManager.Instance.getFrame();
            }
            else
            {
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
            }

            this.dataDrains.ForEach(drain => drain.send(scheme));
        }
    }
}
