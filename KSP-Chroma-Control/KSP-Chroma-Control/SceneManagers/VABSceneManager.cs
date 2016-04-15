using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSP_Chroma_Control.ColorSchemes;
using UnityEngine;

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
            currentColorScheme.SetKeysToColor(new string[] { "1", "2", "3", "4" }, Color.white);

            ConstructionMode state = EditorLogic.fetch.EditorConstructionMode;

            /*if (state.StartsWith("st_offset"))
                placementStatus = "offset";
            else if (state.StartsWith("st_rotate"))
                placementStatus = "rotate";
            else if (state.StartsWith("st_root"))
                placementStatus = "root";
            else if (state.StartsWith("st_idle") || state.StartsWith("st_place"))
                placementStatus = "place";
            else
                Debug.LogWarning("Unknown state: " + state);*/

            switch (state)
            {
                case ConstructionMode.Place:
                    currentColorScheme.SetKeyToColor("1", Color.blue);
                    break;
                case ConstructionMode.Move:
                    currentColorScheme.SetKeyToColor("2", Color.blue);
                    break;
                case ConstructionMode.Rotate:
                    currentColorScheme.SetKeyToColor("3", Color.blue);
                    break;
                case ConstructionMode.Root:
                    currentColorScheme.SetKeyToColor("4", Color.blue);
                    break;
            }
        }

        /// <summary>
        /// Lights up all toggleable keys in a color signifying the button's state.
        /// </summary>
        private void updateToggleables()
        {
            currentColorScheme.SetKeysToColor(new string[] { "x", "c" }, Color.red);
            
            if(EditorLogic.fetch.symmetryMode > 0)
            {
                currentColorScheme.SetKeyToColor("x", Color.green);
            }

            if (EditorLogic.fetch.symmetryMethod == SymmetryMethod.Mirror)
                currentColorScheme.SetKeyToColor("r", Color.blue);
            else if (EditorLogic.fetch.symmetryMethod == SymmetryMethod.Radial)
                currentColorScheme.SetKeyToColor("r", Color.green);

            if (GameSettings.VAB_USE_ANGLE_SNAP)
                currentColorScheme.SetKeyToColor("c", Color.green);
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
