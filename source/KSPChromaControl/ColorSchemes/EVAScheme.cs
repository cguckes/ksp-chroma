namespace KspChromaControl.ColorSchemes
{
    using UnityEngine;

    /// <summary>
    ///     Special color scheme for EVA scenes.
    /// </summary>
    internal class EvaScheme : ColorScheme
    {
        private readonly KeyCode[] movementKeys =
        {
            GameSettings.EVA_back.primary.code,
            GameSettings.EVA_forward.primary.code,
            GameSettings.EVA_left.primary.code,
            GameSettings.EVA_right.primary.code,
            GameSettings.EVA_Jump.primary.code,
            GameSettings.EVA_Run.primary.code
        };

        private readonly KeyCode[] packKeys =
        {
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


        private readonly KeyCode[] switchKeys =
        {
            GameSettings.FOCUS_NEXT_VESSEL.primary.code,
            GameSettings.FOCUS_PREV_VESSEL.primary.code
        };

        private readonly KeyCode[] useKeys =
        {
            GameSettings.EVA_Use.primary.code,
            GameSettings.EVA_Board.primary.code,
            GameSettings.EVA_ToggleMovementMode.primary.code
        };

        /// <summary>
        ///     Overlays the defined keys over a black base layout.
        /// </summary>
        public EvaScheme()
        {
            if (FlightGlobals.ActiveVessel.evaController.JetpackDeployed)
            {
                this.SetKeysToColor(this.packKeys, Color.yellow);
                this.SetKeyToColor(GameSettings.EVA_TogglePack.primary.code, Color.green);
            }
            else
            {
                this.SetKeysToColor(this.movementKeys, Color.white);
                this.SetKeyToColor(GameSettings.EVA_TogglePack.primary.code, Color.red);
            }

            if (FlightGlobals.ActiveVessel.evaController.lampOn)
            {
                this.SetKeyToColor(GameSettings.EVA_Lights.primary.code, Color.green);
            }
            else
            {
                this.SetKeyToColor(GameSettings.EVA_Lights.primary.code, Color.red);
            }

            this.SetKeysToColor(this.useKeys, Color.cyan);

            this.SetKeysToColor(this.switchKeys, Color.blue);
        }
    }
}