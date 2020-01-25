namespace KspChromaControl.ColorSchemes
{
    using UnityEngine;

    /// <summary>
    ///     The base color scheme for all flight related game scenes.
    /// </summary>
    internal class FlightScheme : ColorScheme
    {
        /// <summary>
        ///     Overlays the defined keys over a black base layout.
        /// </summary>
        public FlightScheme()
        {
            KeyCode[] yellowkeys =
            {
                GameSettings.TRANSLATE_BACK.primary.code,
                GameSettings.TRANSLATE_FWD.primary.code,
                GameSettings.TRANSLATE_LEFT.primary.code,
                GameSettings.TRANSLATE_RIGHT.primary.code,
                GameSettings.TRANSLATE_UP.primary.code,
                GameSettings.TRANSLATE_DOWN.primary.code
            };
            this.SetKeysToColor(yellowkeys, Color.yellow);

            KeyCode[] redkeys =
            {
                GameSettings.THROTTLE_FULL.primary.code, GameSettings.THROTTLE_CUTOFF.primary.code,
                GameSettings.THROTTLE_UP.primary.code, GameSettings.THROTTLE_DOWN.primary.code
            };
            this.SetKeysToColor(redkeys, Color.red);

            KeyCode[] bluekeys =
                {GameSettings.FOCUS_NEXT_VESSEL.primary.code, GameSettings.FOCUS_PREV_VESSEL.primary.code};
            this.SetKeysToColor(bluekeys, Color.blue);
        }
    }
}