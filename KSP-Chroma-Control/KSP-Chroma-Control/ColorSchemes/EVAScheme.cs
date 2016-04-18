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
        private readonly KeyCode[] movementKeys = {
            GameSettings.EVA_back.primary,
            GameSettings.EVA_forward.primary,
            GameSettings.EVA_left.primary,
            GameSettings.EVA_right.primary,
            GameSettings.EVA_Jump.primary,
            GameSettings.EVA_Run.primary
        };

        private readonly KeyCode[] useKeys = {
            GameSettings.EVA_Use.primary,
            GameSettings.EVA_Board.primary,
            GameSettings.EVA_ToggleMovementMode.primary,
        };

        private readonly KeyCode[] packKeys = {
            GameSettings.EVA_back.primary,
            GameSettings.EVA_forward.primary,
            GameSettings.EVA_left.primary,
            GameSettings.EVA_right.primary,
            GameSettings.EVA_Jump.primary,
            GameSettings.EVA_yaw_left.primary,
            GameSettings.EVA_yaw_right.primary,
            GameSettings.EVA_Pack_up.primary,
            GameSettings.EVA_Pack_down.primary,
            GameSettings.EVA_Run.primary
        };


        private readonly KeyCode[] switchKeys = {
            GameSettings.FOCUS_NEXT_VESSEL.primary,
            GameSettings.FOCUS_PREV_VESSEL.primary
        };
        /// <summary>
        /// Overlays the defined keys over a black base layout.
        /// </summary>
        public EVAScheme()
        {
            if (FlightGlobals.ActiveVessel.evaController.JetpackDeployed)
            {
                SetKeysToColor(packKeys, Color.yellow);
                SetKeyToColor(GameSettings.EVA_TogglePack.primary, Color.green);
            }
            else
            {
                SetKeysToColor(movementKeys, Color.white);
                SetKeyToColor(GameSettings.EVA_TogglePack.primary, Color.red);
            }

            if (FlightGlobals.ActiveVessel.evaController.lampOn)
                SetKeyToColor(GameSettings.EVA_Lights.primary, Color.green);
            else
                SetKeyToColor(GameSettings.EVA_Lights.primary, Color.red);

            SetKeysToColor(useKeys, Color.cyan);

            SetKeysToColor(switchKeys, Color.blue);
        }
    }
}
