using System;
using KSP_Chroma_Control.ColorSchemes;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace KSP_Chroma_Control
{
    internal class PowerLostAnimation : KeyboardAnimation
    {
        private static ColorScheme red = new ColorScheme(Color.red);
        private static ColorScheme lightningBolts = new ColorScheme(Color.blue);

        static PowerLostAnimation()
        {
            KeyCode[] lightningKeys = new KeyCode[]
            {
                /// Left lightning
                KeyCode.F2, KeyCode.Alpha3, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.D, KeyCode.X, KeyCode.LeftAlt,

                /// Right lightning
                KeyCode.F9, KeyCode.Alpha0, KeyCode.O, KeyCode.P, KeyCode.LeftBracket, KeyCode.Semicolon, KeyCode.Period,
                KeyCode.RightAlt
            };

            lightningBolts.SetKeysToColor(lightningKeys, Color.white);
            red.SetKeysToColor(lightningKeys, Color.blue);
        }

        public ColorScheme getFrame()
        {
            return (((int)Time.realtimeSinceStartup) % 2 == 0) ? red : lightningBolts;
        }

        public bool isFinished()
        {
            /// Exit if the scene changes.
            if (HighLogic.LoadedScene != GameScenes.FLIGHT)
                return true;

            /// Check if the vessel needs power, and if empty, continue blinking
            IEnumerable<Vessel.ActiveResource> resource = FlightGlobals.ActiveVessel.GetActiveResources()
                .Where(res => res.info.name.Equals("ElectricCharge"));
            if (resource.Count() > 0)
                return resource.First().amount > 0.0001;

            /// No energy stored on the ship, end the animation.
            return true;
        }
    }
}