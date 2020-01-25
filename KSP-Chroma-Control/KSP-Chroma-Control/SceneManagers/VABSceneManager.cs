namespace KspChromaControl.SceneManagers
{
    using KspChromaControl.ColorSchemes;
    using UnityEngine;

    /// <summary>
    ///     Manages the keyboard colors for VAB and SPH scenes.
    /// </summary>
    internal class VabSceneManager : ISceneManager
    {
        /// <summary>
        ///     The base color scheme, used by all editor scenes
        /// </summary>
        private ColorScheme currentColorScheme;

        /// <summary>
        ///     Returns the rendered color scheme for the current game state.
        /// </summary>
        /// <returns>The finalized color scheme</returns>
        public ColorScheme GetScheme()
        {
            if (this.currentColorScheme == null)
            {
                this.Reset();
            }

            this.Update();
            return this.currentColorScheme;
        }

        /// <summary>
        ///     Called during every physics frame of the game. Recalculates the colors
        ///     according to the editor's state.
        /// </summary>
        private void Update()
        {
            this.UpdatePlacementState();
            this.UpdateToggleables();
        }

        /// <summary>
        ///     Lights up the corresponding key to the current editor construction mode.
        /// </summary>
        private void UpdatePlacementState()
        {
            this.currentColorScheme.SetKeysToColor(
                new[]
                {
                    GameSettings.Editor_modePlace.primary.code, GameSettings.Editor_modeOffset.primary.code,
                    GameSettings.Editor_modeRotate.primary.code, GameSettings.Editor_modeRoot.primary.code
                },
                Color.white
            );

            if (EditorLogic.fetch != null)
            {
                var state = EditorLogic.fetch.EditorConstructionMode;

                switch (state)
                {
                    case ConstructionMode.Place:
                        this.currentColorScheme.SetKeyToColor(GameSettings.Editor_modePlace.primary.code, Color.blue);
                        break;
                    case ConstructionMode.Move:
                        this.currentColorScheme.SetKeyToColor(GameSettings.Editor_modeOffset.primary.code, Color.blue);
                        break;
                    case ConstructionMode.Rotate:
                        this.currentColorScheme.SetKeyToColor(GameSettings.Editor_modeRotate.primary.code, Color.blue);
                        break;
                    case ConstructionMode.Root:
                        this.currentColorScheme.SetKeyToColor(GameSettings.Editor_modeRoot.primary.code, Color.blue);
                        break;
                }
            }
        }

        /// <summary>
        ///     Lights up all toggleable keys in a color signifying the button's state.
        /// </summary>
        private void UpdateToggleables()
        {
            this.currentColorScheme.SetKeysToColor(
                new[]
                {
                    GameSettings.Editor_toggleSymMode.primary.code,
                    GameSettings.Editor_toggleAngleSnap.primary.code
                },
                Color.red
            );

            if (EditorLogic.fetch.symmetryMode > 0)
            {
                this.currentColorScheme.SetKeyToColor(GameSettings.Editor_toggleSymMode.primary.code, Color.green);
            }

            if (EditorLogic.fetch.symmetryMethod == SymmetryMethod.Mirror)
            {
                this.currentColorScheme.SetKeyToColor(GameSettings.Editor_toggleSymMethod.primary.code, Color.blue);
            }
            else if (EditorLogic.fetch.symmetryMethod == SymmetryMethod.Radial)
            {
                this.currentColorScheme.SetKeyToColor(GameSettings.Editor_toggleSymMethod.primary.code, Color.green);
            }

            if (GameSettings.VAB_USE_ANGLE_SNAP)
            {
                this.currentColorScheme.SetKeyToColor(GameSettings.Editor_toggleAngleSnap.primary.code, Color.green);
            }
        }

        /// <summary>
        ///     Resets the color scheme to the original one.
        /// </summary>
        private void Reset() => this.currentColorScheme = new VabScheme();
    }
}