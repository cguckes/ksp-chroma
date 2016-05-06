using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KspChromaControl.ColorSchemes
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
            KeyCode[] yellowkeys = {
                GameSettings.TRANSLATE_BACK.primary,
                GameSettings.TRANSLATE_FWD.primary,
                GameSettings.TRANSLATE_LEFT.primary,
                GameSettings.TRANSLATE_RIGHT.primary,
                GameSettings.TRANSLATE_UP.primary,
                GameSettings.TRANSLATE_DOWN.primary
            };
            SetKeysToColor(yellowkeys, Color.yellow);

            KeyCode[] redkeys = { 
                GameSettings.THROTTLE_FULL.primary, GameSettings.THROTTLE_CUTOFF.primary,
                GameSettings.THROTTLE_UP.primary, GameSettings.THROTTLE_DOWN.primary
            };
            SetKeysToColor(redkeys, Color.red);

            KeyCode[] bluekeys = { GameSettings.FOCUS_NEXT_VESSEL.primary, GameSettings.FOCUS_PREV_VESSEL.primary };
            SetKeysToColor(bluekeys, Color.blue);
        }
    }
}
