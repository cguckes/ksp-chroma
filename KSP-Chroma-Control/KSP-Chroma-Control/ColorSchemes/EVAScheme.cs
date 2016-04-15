using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSP_Chroma_Control.ColorSchemes
{
    /// <summary>
    /// Special color scheme for EVA scenes.
    /// </summary>
    class EVAScheme : ColorScheme
    {
        /// <summary>
        /// Overlays the defined keys over a black base layout.
        /// </summary>
        public EVAScheme()
        {
            string[] whitekeys = { "w", "a", "s", "d", "q", "e", "space" };
            SetKeysToColor(whitekeys, Color.green);

            string[] yellowkeys = { "f", "space",  };
            SetKeysToColor(yellowkeys, Color.yellow);

            string[] redkeys = { "leftshift", "leftctrl" };
            SetKeysToColor(redkeys, Color.red);

            string[] cyankeys = { "l", "r", "b", "alt" };
            SetKeysToColor(cyankeys, Color.cyan);

            string[] bluekeys = { "[", "]" };
            SetKeysToColor(cyankeys, Color.blue);
        }
    }
}
