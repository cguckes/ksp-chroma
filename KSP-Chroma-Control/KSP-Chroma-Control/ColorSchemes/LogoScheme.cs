using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSP_Chroma_Control.ColorSchemes
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
            string[] redkeys = {
                ///Rocket
                "alt", "backslash", "z", "s", "x", "c", "f3", "d", "e", "4",
                
                ///Stripes
                "leftshift", "x", "c", "v", "b", "n", "m", ",", ".", "slash", "rightshift"
            };
            SetKeysToColor(redkeys, Color.red);

            string[] whitekeys =
            {
                "leftctrl", "windows", "space", "altgr", "fn", "contextmenu", "rightctrl",
                "a", "w", "3", "f2", "f4", "5", "r", "f",
                "capslock", "g", "h", "j", "k", "l", ";", "'", "hash"
            };
            SetKeysToColor(whitekeys, Color.white);
        }
    }
}
