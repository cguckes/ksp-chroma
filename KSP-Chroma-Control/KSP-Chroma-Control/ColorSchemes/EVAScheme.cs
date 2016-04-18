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
            KeyCode[] whitekeys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Q, KeyCode.E, KeyCode.Space  };
            SetKeysToColor(whitekeys, Color.green);

            KeyCode[] yellowkeys = { KeyCode.F };
            SetKeysToColor(yellowkeys, Color.yellow);

            KeyCode[] redkeys = { KeyCode.LeftShift, KeyCode.LeftControl };
            SetKeysToColor(redkeys, Color.red);

            KeyCode[] cyankeys = { KeyCode.L, KeyCode.R, KeyCode.B, KeyCode.LeftAlt };
            SetKeysToColor(cyankeys, Color.cyan);

            KeyCode[] bluekeys = { KeyCode.LeftParen, KeyCode.RightParen };
            SetKeysToColor(cyankeys, Color.blue);
        }
    }
}
