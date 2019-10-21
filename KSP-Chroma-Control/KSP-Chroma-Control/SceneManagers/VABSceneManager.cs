using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KspChromaControl.ColorSchemes;
using UnityEngine;
using Corale.Colore.Razer.Keyboard;

namespace KspChromaControl.SceneManagers
{
    /// <summary>
    /// Manages the keyboard colors for VAB and SPH scenes.
    /// </summary>
    internal class VABSceneManager : SceneManager
    {
        /// <summary>
        /// The base color scheme, used by all editor scenes
        /// </summary>
        private ColorScheme currentColorScheme;

        /// <summary>
        /// Returns the rendered color scheme for the current game state.
        /// </summary>
        /// <returns>The finalized color scheme</returns>
        public ColorScheme getScheme()
        {
            if(currentColorScheme == null)
            {
                reset();
            }

            update();            
            return this.currentColorScheme;
        }

        /// <summary>
        /// Called during every physics frame of the game. Recalculates the colors
        /// according to the editor's state.
        /// </summary>
        private void update()
        {
            updatePlacementState();
            updateToggleables();
        }

        /// <summary>
        /// Lights up the corresponding key to the current editor construction mode.
        /// </summary>
        private void updatePlacementState()
        {
            currentColorScheme.SetKeysToColor(new KeyCode[] { GameSettings.Editor_modePlace.primary.code, GameSettings.Editor_modeOffset.primary.code,
                GameSettings.Editor_modeRotate.primary.code, GameSettings.Editor_modeRoot.primary.code }, Color.white);

            if (EditorLogic.fetch != null)
            {
                ConstructionMode state = EditorLogic.fetch.EditorConstructionMode;

                switch (state)
                {
                    case ConstructionMode.Place:
                        currentColorScheme.SetKeyToColor(GameSettings.Editor_modePlace.primary.code, Color.blue);
                        break;
                    case ConstructionMode.Move:
                        currentColorScheme.SetKeyToColor(GameSettings.Editor_modeOffset.primary.code, Color.blue);
                        break;
                    case ConstructionMode.Rotate:
                        currentColorScheme.SetKeyToColor(GameSettings.Editor_modeRotate.primary.code, Color.blue);
                        break;
                    case ConstructionMode.Root:
                        currentColorScheme.SetKeyToColor(GameSettings.Editor_modeRoot.primary.code, Color.blue);
                        break;
                }
            }
        }

        /// <summary>
        /// Lights up all toggleable keys in a color signifying the button's state.
        /// </summary>
        private void updateToggleables()
        {
            currentColorScheme.SetKeysToColor(new KeyCode[] { GameSettings.Editor_toggleSymMode.primary.code, GameSettings.Editor_toggleAngleSnap.primary.code }, Color.red);
            
            if(EditorLogic.fetch.symmetryMode > 0)
            {
                currentColorScheme.SetKeyToColor(GameSettings.Editor_toggleSymMode.primary.code, Color.green);
            }

            if (EditorLogic.fetch.symmetryMethod == SymmetryMethod.Mirror)
                currentColorScheme.SetKeyToColor(GameSettings.Editor_toggleSymMethod.primary.code, Color.blue);
            else if (EditorLogic.fetch.symmetryMethod == SymmetryMethod.Radial)
                currentColorScheme.SetKeyToColor(GameSettings.Editor_toggleSymMethod.primary.code, Color.green);

            if (GameSettings.VAB_USE_ANGLE_SNAP)
                currentColorScheme.SetKeyToColor(GameSettings.Editor_toggleAngleSnap.primary.code, Color.green);
        }

        /// <summary>
        /// Resets the color scheme to the original one.
        /// </summary>
        private void reset()
        {
            this.currentColorScheme = new VabScheme();
        }
    }
}
