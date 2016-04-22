using System;
using KSP_Chroma_Control.ColorSchemes;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace KSP_Chroma_Control
{
    /// <summary>
    /// Displays a warning on the keyboard, indicating that the vessel is currently out of power and cannot
    /// be controlled. Consists of two frames alternating at 1fps.
    /// </summary>
    internal class PowerLostAnimation : KeyboardAnimation
    {
        /// <summary>
        /// The red frame
        /// </summary>
        private static ColorScheme red = new ColorScheme(Color.red);

        /// <summary>
        /// The blue frame
        /// </summary>
        private static ColorScheme blue = new ColorScheme(Color.blue);

        /// <summary>
        /// Static constructor adds lightning bolts in different colors to both frames
        /// </summary>
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

            blue.SetKeysToColor(lightningKeys, Color.white);
            red.SetKeysToColor(lightningKeys, Color.blue);
        }

        /// <summary>
        /// <see cref="KeyboardAnimation.getFrame"/>
        /// </summary>
        /// <returns>the current animation frame.</returns>
        public ColorScheme getFrame()
        {
            return (((int)Time.realtimeSinceStartup) % 2 == 0) ? red : blue;
        }

        /// <summary>
        /// <see cref="KeyboardAnimation.isFinished"/>
        /// </summary>
        /// <returns>true, if the animation is finished, false if not.</returns>
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