using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSP_Chroma_Control.ColorSchemes;
using UnityEngine;
using Corale.Colore.Razer.Keyboard;

namespace KSP_Chroma_Control.SceneManagers
{
    /// <summary>
    /// Manages the keyboard colors for VAB and SPH scenes.
    /// </summary>
    class VABSceneManager : SceneManager
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
            currentColorScheme.SetKeysToColor(new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, }, Color.white);

            ConstructionMode state = EditorLogic.fetch.EditorConstructionMode;

            switch (state)
            {
                case ConstructionMode.Place:
                    currentColorScheme.SetKeyToColor(KeyCode.Alpha1, Color.blue);
                    break;
                case ConstructionMode.Move:
                    currentColorScheme.SetKeyToColor(KeyCode.Alpha2, Color.blue);
                    break;
                case ConstructionMode.Rotate:
                    currentColorScheme.SetKeyToColor(KeyCode.Alpha3, Color.blue);
                    break;
                case ConstructionMode.Root:
                    currentColorScheme.SetKeyToColor(KeyCode.Alpha4, Color.blue);
                    break;
            }
        }

        /// <summary>
        /// Lights up all toggleable keys in a color signifying the button's state.
        /// </summary>
        private void updateToggleables()
        {
            currentColorScheme.SetKeysToColor(new KeyCode[] { KeyCode.X, KeyCode.C }, Color.red);
            
            if(EditorLogic.fetch.symmetryMode > 0)
            {
                currentColorScheme.SetKeyToColor(KeyCode.X , Color.green);
            }

            if (EditorLogic.fetch.symmetryMethod == SymmetryMethod.Mirror)
                currentColorScheme.SetKeyToColor(KeyCode.R, Color.blue);
            else if (EditorLogic.fetch.symmetryMethod == SymmetryMethod.Radial)
                currentColorScheme.SetKeyToColor(KeyCode.R, Color.green);

            if (GameSettings.VAB_USE_ANGLE_SNAP)
                currentColorScheme.SetKeyToColor(KeyCode.C, Color.green);
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
