using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSP_Chroma_Control.ColorSchemes;
using UnityEngine;

namespace KSP_Chroma_Control.SceneManagers
{
    class VABSceneManager : SceneManager
    {
        private ColorScheme currentColorScheme;

        /// <summary>
        /// We are using integers because we dont need any accuracy for the three keys
        /// we can light up.
        /// </summary>

        public ColorScheme getScheme()
        {
            if(currentColorScheme == null)
            {
                reset();
            }

            update();            
            return this.currentColorScheme;
        }

        private void update()
        {
            updatePlacementState();
            updateToggleables();
        }

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

        private void reset()
        {
            this.currentColorScheme = new VabScheme();
        }
    }
}
