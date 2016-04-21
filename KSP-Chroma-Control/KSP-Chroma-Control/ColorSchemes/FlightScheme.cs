using UnityEngine;

namespace KSP_Chroma_Control.ColorSchemes
{
    /// <summary>
    /// The base color scheme for all flight related game scenes.
    /// </summary>
    class FlightScheme : ColorScheme
    {
        /// <summary>
        /// Overlays the defined keys over a black base layout.
        /// </summary>
        public FlightScheme()
        {
            KeyCode[] whitekeys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Q, KeyCode.E, KeyCode.Space };
            SetKeysToColor(whitekeys, Color.green);

            KeyCode[] yellowkeys = { KeyCode.H, KeyCode.N, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.I };
            SetKeysToColor(yellowkeys, Color.yellow);

            KeyCode[] redkeys = { KeyCode.LeftShift, KeyCode.LeftControl, KeyCode.X, KeyCode.Z };
            SetKeysToColor(redkeys, Color.red);

            KeyCode[] cyankeys = { KeyCode.L, KeyCode.R, KeyCode.B, KeyCode.LeftAlt };
            SetKeysToColor(cyankeys, Color.cyan);

            KeyCode[] greenkeys = { KeyCode.R, KeyCode.T, KeyCode.F, KeyCode.B };
            SetKeysToColor(greenkeys, Color.green);

            KeyCode[] bluekeys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4,
                KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9,
                KeyCode.Alpha0 };
            SetKeysToColor(bluekeys, Color.blue);
        }
    }
}
