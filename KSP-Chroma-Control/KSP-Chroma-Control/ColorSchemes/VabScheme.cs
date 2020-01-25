namespace KspChromaControl.ColorSchemes
{
    using UnityEngine;

    /// <summary>
    ///     Contains the base color scheme for all VAB and SPH scenes.
    /// </summary>
    internal class VabScheme : ColorScheme
    {
        /// <summary>
        ///     Overlays the defined key colors over the base color scheme.
        /// </summary>
        public VabScheme()
        {
            this.SetKeysToColor(
                new[]
                {
                    GameSettings.Editor_pitchUp.primary.code, GameSettings.Editor_pitchDown.primary.code,
                    GameSettings.Editor_rollLeft.primary.code, GameSettings.Editor_rollRight.primary.code,
                    GameSettings.Editor_yawLeft.primary.code, GameSettings.Editor_yawRight.primary.code
                },
                new Color(1f, 1f, 0f)
            );
            this.SetKeysToColor(
                new[]
                {
                    GameSettings.Editor_fineTweak.primary.code,
                    GameSettings.Editor_resetRotation.primary.code,
                    GameSettings.Editor_coordSystem.primary.code
                },
                Color.magenta
            );
        }
    }
}