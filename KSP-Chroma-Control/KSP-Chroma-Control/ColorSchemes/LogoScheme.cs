using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KspChromaControl.ColorSchemes
{
    /// <summary>
    /// Displays an image on the keyboard vaguely similar to the logo minus the 
    /// KSP text.
    /// </summary>
    class LogoScheme : ColorScheme
    {
        /// <summary>
        /// Overlays the defined keys on top of a blue base color scheme.
        /// </summary>
        public LogoScheme() : base(Color.blue)
        {
            KeyCode[] redkeys = {
                ///Rocket
                KeyCode.LeftAlt, KeyCode.Backslash, KeyCode.Z, KeyCode.S, KeyCode.X, KeyCode.C, KeyCode.F3,
                KeyCode.D, KeyCode.E, KeyCode.Alpha4,
                
                ///Stripes
                KeyCode.LeftShift, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M, KeyCode.Comma, KeyCode.Period,
                KeyCode.Slash, KeyCode.RightShift
            };
            SetKeysToColor(redkeys, Color.red);

            KeyCode[] whitekeys =
            {
                KeyCode.LeftControl, KeyCode.LeftWindows, KeyCode.Space, KeyCode.AltGr, KeyCode.RightAlt, KeyCode.RightControl,
                KeyCode.Menu, KeyCode.RightControl, KeyCode.A, KeyCode.W, KeyCode.Alpha3, KeyCode.F2,
                KeyCode.F4, KeyCode.Alpha5, KeyCode.R, KeyCode.F, KeyCode.CapsLock, KeyCode.G, KeyCode.H,
                KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.Semicolon, KeyCode.Quote, KeyCode.Hash
            };
            SetKeysToColor(whitekeys, Color.white);
        }
    }
}
