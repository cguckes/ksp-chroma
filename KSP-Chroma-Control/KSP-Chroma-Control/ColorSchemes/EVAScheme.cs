using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KspChromaControl.ColorSchemes
{
    /// <summary>
    /// Special color scheme for EVA scenes.
    /// </summary>
    internal class EVAScheme : ColorScheme
    {
        private readonly KeyCode[] movementKeys = {
            GameSettings.EVA_back.primary.code,
            GameSettings.EVA_forward.primary.code,
            GameSettings.EVA_left.primary.code,
            GameSettings.EVA_right.primary.code,
            GameSettings.EVA_Jump.primary.code,
            GameSettings.EVA_Run.primary.code
        };

        private readonly KeyCode[] useKeys = {
            GameSettings.EVA_Use.primary.code,
            GameSettings.EVA_Board.primary.code,
            GameSettings.EVA_ToggleMovementMode.primary.code,
        };

        private readonly KeyCode[] packKeys = {
            GameSettings.EVA_back.primary.code,
            GameSettings.EVA_forward.primary.code,
            GameSettings.EVA_left.primary.code,
            GameSettings.EVA_right.primary.code,
            GameSettings.EVA_Jump.primary.code,
            GameSettings.EVA_yaw_left.primary.code,
            GameSettings.EVA_yaw_right.primary.code,
            GameSettings.EVA_Pack_up.primary.code,
            GameSettings.EVA_Pack_down.primary.code,
            GameSettings.EVA_Run.primary.code
        };


        private readonly KeyCode[] switchKeys = {
            GameSettings.FOCUS_NEXT_VESSEL.primary.code,
            GameSettings.FOCUS_PREV_VESSEL.primary.code
        };
        /// <summary>
        /// Overlays the defined keys over a black base layout.
        /// </summary>
        public EVAScheme()
        {
            if (FlightGlobals.ActiveVessel.evaController.JetpackDeployed)
            {
                SetKeysToColor(packKeys, Color.yellow);
                SetKeyToColor(GameSettings.EVA_TogglePack.primary.code, Color.green);
            }
            else
            {
                SetKeysToColor(movementKeys, Color.white);
                SetKeyToColor(GameSettings.EVA_TogglePack.primary.code, Color.red);
            }

            if (FlightGlobals.ActiveVessel.evaController.lampOn)
                SetKeyToColor(GameSettings.EVA_Lights.primary.code, Color.green);
            else
                SetKeyToColor(GameSettings.EVA_Lights.primary.code, Color.red);

            SetKeysToColor(useKeys, Color.cyan);

            SetKeysToColor(switchKeys, Color.blue);
        }
    }
}
