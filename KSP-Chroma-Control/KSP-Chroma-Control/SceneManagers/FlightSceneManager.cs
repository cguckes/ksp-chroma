using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSP_Chroma_Control.ColorSchemes;
using UnityEngine;

namespace KSP_Chroma_Control.SceneManagers
{
    /// <summary>
    /// Manages the keyboard colors during all flight scenes.
    /// </summary>
    class FlightSceneManager : SceneManager
    {
        /// <summary>
        /// The vessel we are piloting currently. Can be a normal vessel or a single
        /// kerbal.
        /// </summary>
        private Vessel currentVessel;

        /// <summary>
        /// The current keyboard state color scheme
        /// </summary>
        private ColorScheme currentColorScheme;

        /// <summary>
        /// Contains all ActionGroups and their current usage state. False means
        /// this ActionGroup has no impact on any part of the vessel.
        /// </summary>
        private Dictionary<KSPActionGroup, Boolean> actionGroups = new Dictionary<KSPActionGroup, Boolean>();

        /// <summary>
        /// Fills the action group list with all false values;
        /// </summary>
        public FlightSceneManager()
        {
            resetActionGroups();
        }

        /// <summary>
        /// Recalculates every action group's usage.
        /// </summary>
        private void resetActionGroups()
        {
            this.actionGroups.Clear();
            foreach (KSPActionGroup group in Enum.GetValues(typeof(KSPActionGroup)).Cast<KSPActionGroup>())
                this.actionGroups.Add(group, false);
        }

        /// <summary>
        /// Returns the calculated color scheme for the current game state.
        /// </summary>
        /// <returns>The final color scheme for this frame</returns>
        public ColorScheme getScheme()
        {
            update();            
            return this.currentColorScheme;
        }

        /// <summary>
        /// Called by the plugin on every physics frame.
        /// </summary>
        private void update()
        {
            if (this.currentVessel != FlightGlobals.ActiveVessel)
            {
                this.currentVessel = FlightGlobals.fetch.activeVessel;
                resetActionGroups();
                findUsableActionGroups();
            }

            if (currentVessel.isEVA)
            {
                this.currentColorScheme = new EVAScheme();
                updateEvaKeys();
            }
            else
            {
                this.currentColorScheme = new FlightScheme();
                recalculateResources();
                updateToggleables();
            }
        }

        /// <summary>
        /// Handles the EVA keyboard colors.
        /// </summary>
        private void updateEvaKeys()
        {
            KerbalEVA eva = currentVessel.evaController;

            showGauge("EVAFuel", eva.Fuel, eva.FuelCapacity);

            if (eva.JetpackDeployed)
                currentColorScheme.SetKeyToColor("r", Color.green);
            else
                currentColorScheme.SetKeyToColor("r", Color.red);

            if (eva.lampOn)
                currentColorScheme.SetKeyToColor("l", Color.green);
            else
                currentColorScheme.SetKeyToColor("l", Color.red);
        }

        /// <summary>
        /// Scans the ship's parts for actions in any action group. Every action group
        /// that has any active parts gets a toggleing button lit up.
        /// </summary>
        private void findUsableActionGroups()
        {
            List<BaseAction> allActionsList = new List<BaseAction>();

            foreach(Part p in currentVessel.parts)
            {
                allActionsList.AddRange(p.Actions);
                foreach(PartModule pm in p.Modules)
                    allActionsList.AddRange(pm.Actions);
            }

            foreach (BaseAction action in allActionsList)
                foreach (KSPActionGroup group in Enum.GetValues(typeof(KSPActionGroup)).Cast<KSPActionGroup>())
                    actionGroups[group] = actionGroups[group] || ((action.actionGroup & group) == group);
        }

        /// <summary>
        /// Displays the fuel status as lights on the keyboard.
        /// </summary>
        private void recalculateResources()
        {
            List<Vessel.ActiveResource> resources = currentVessel.GetActiveResources();

            resources.ForEach(res => {
                showGauge(res.info.name, res.amount, res.maxAmount);
            });
        }

        /// <summary>
        /// Displays the amount of resources left as a gauge on the keyboard
        /// </summary>
        /// <param name="resource">The name of the resource</param>
        /// <param name="amount">The actual amount of the resource in the current stage</param>
        /// <param name="maxAmount">The maximal amount of the resource in the current stage</param>
        private void showGauge(string resource, double amount, double maxAmount)
        {
            string[] keys = new string[3];
            if (resource.Equals("ElectricCharge"))
            {
                currentColorScheme.SetKeysToColor(new string[] { "prtsc", "scrlk", "break" }, Color.black);
                
                keys[0] = (amount > 0.01) ? "prtsc" : "";
                keys[1] = (amount > maxAmount * 0.33) ? "scrlk" : "";
                keys[2] = (amount > maxAmount * 0.66) ? "break" : "";
                currentColorScheme.SetKeysToColor(keys, Color.blue);
            }
            else if (resource.Equals("LiquidFuel"))
            {
                currentColorScheme.SetKeysToColor(new string[] { "numlk", "num/", "num*" }, Color.black);

                keys[0] = (amount > 0.01) ? "numlk" : "";
                keys[1] = (amount > maxAmount * 0.33) ? "num/" : "";
                keys[2] = (amount > maxAmount * 0.66) ? "num*" : "";
                currentColorScheme.SetKeysToColor(keys, Color.green);
            }
            else if (resource.Equals("Oxidizer"))
            {
                currentColorScheme.SetKeysToColor(new string[] { "num7", "num8", "num9" }, Color.black);

                keys[0] = (amount > 0.01) ? "num7" : "";
                keys[1] = (amount > maxAmount * 0.33) ? "num8" : "";
                keys[2] = (amount > maxAmount * 0.66) ? "num9" : "";
                currentColorScheme.SetKeysToColor(keys, Color.cyan);
            }
            else if (resource.Equals("MonoPropellant") || resource.Equals("EVAFuel"))
            {
                currentColorScheme.SetKeysToColor(new string[] { "num4", "num5", "num6" }, Color.black);

                keys[0] = (amount > 0.01) ? "num4" : "";
                keys[1] = (amount > maxAmount * 0.33) ? "num5" : "";
                keys[2] = (amount > maxAmount * 0.66) ? "num6" : "";
                currentColorScheme.SetKeysToColor(keys, Color.magenta);
            }
            else if (resource.Equals("SolidFuel"))
            {
                currentColorScheme.SetKeysToColor(new string[] { "num1", "num2", "num3" }, Color.black);

                keys[0] = (amount > 0.01) ? "num1" : "";
                keys[1] = (amount > maxAmount * 0.33) ? "num2" : "";
                keys[2] = (amount > maxAmount * 0.66) ? "num3" : "";
                currentColorScheme.SetKeysToColor(keys, new Color(1f, 1f, 0f));
            }
            else if (resource.Equals("Ablator"))
            {
                currentColorScheme.SetKeysToColor(new string[] { "del", "end", "pagedown" }, Color.black);

                keys[0] = (amount > 0.01) ? "del" : "";
                keys[1] = (amount > maxAmount * 0.33) ? "end" : "";
                keys[2] = (amount > maxAmount * 0.66) ? "pagedown" : "";
                currentColorScheme.SetKeysToColor(keys, new Color32(244, 159, 0, 255));
            }
            else if (resource.Equals("XenonGas"))
            {
                currentColorScheme.SetKeysToColor(new string[] { "ins", "home", "pageup" }, Color.black);

                keys[0] = (amount > 0.01) ? "ins" : "";
                keys[1] = (amount > maxAmount * 0.33) ? "home" : "";
                keys[2] = (amount > maxAmount * 0.66) ? "pageup" : "";
                currentColorScheme.SetKeysToColor(keys, new Color(.8f, .8f, .8f));
            }
            else
            {
                //Debug.LogWarning("Unhandled fuel resource: " + resource);
            }
        }

        /// <summary>
        /// Updates all toggleable buttons on the keyboard.
        /// </summary>
        private void updateToggleables()
        {
            currentColorScheme.SetKeysToColor(new string[] { "f5", "t", "r", "m" }, Color.red);

            if (currentVessel.Autopilot !=null && currentVessel.Autopilot.Enabled)
                currentColorScheme.SetKeyToColor("t", Color.green);

            if(!FlightInputHandler.RCSLock)
                currentColorScheme.SetKeyToColor("r", Color.green);

            if (MapView.MapIsEnabled)
                currentColorScheme.SetKeyToColor("m", Color.green);

            if (FlightInputHandler.fetch.precisionMode)
            {
                currentColorScheme.SetKeysToColor(new string[] { "q", "e", "w", "a", "s", "d" }, Color.cyan);
                currentColorScheme.SetKeyToColor("capslock", Color.green);
            }
            else
            {
                currentColorScheme.SetKeysToColor(new string[] { "q", "e", "w", "a", "s", "d" }, Color.white);
                currentColorScheme.SetKeyToColor("capslock", Color.red);
            }

            if (currentVessel.IsClearToSave() == ClearToSaveStatus.CLEAR ||
                currentVessel.IsClearToSave() == ClearToSaveStatus.NOT_IN_ATMOSPHERE ||
                currentVessel.IsClearToSave() == ClearToSaveStatus.NOT_UNDER_ACCELERATION)
                currentColorScheme.SetKeyToColor("f5", Color.green);

            if (TimeWarp.WarpMode == TimeWarp.Modes.HIGH)
                currentColorScheme.SetKeysToColor(new string[] { ",", "." }, Color.green);
            else
                currentColorScheme.SetKeysToColor(new string[] { ",", "." }, Color.red);

            ///TODO: Remove garbage code make nice and use all actiongroups for toggleable buttons
            for (int i = 1; i <= 14; i++)
            {
                string key = "";
                KSPActionGroup action;

                switch (i) {
                    case 11:
                        action = KSPActionGroup.Light;
                        key = "u";
                        break;
                    case 12:
                        action = KSPActionGroup.Brakes;
                        key = "b";
                        break;
                    case 13:
                        action = KSPActionGroup.Gear;
                        key = "g";
                        break;
                    case 14:
                        action = KSPActionGroup.Abort;
                        key = "backspace";
                        break;
                    default:
                        action = (KSPActionGroup)System.Enum.Parse(typeof(KSPActionGroup), "Custom" + i.ToString("D2"));
                        key = ((i == 10) ? "0" : i.ToString());
                        break;
                }

                if (!actionGroups[action])
                    currentColorScheme.SetKeyToColor(key, Color.black);
                else if (currentVessel.ActionGroups[action])
                    currentColorScheme.SetKeyToColor(key, Color.blue);
                else
                    currentColorScheme.SetKeyToColor(key, (Color) new Color32(50, 50, 255, 255));
            }

            switch(FlightCamera.fetch.mode)
            {
                case FlightCamera.Modes.AUTO:
                    currentColorScheme.SetKeyToColor("v", Color.green);
                    break;
                case FlightCamera.Modes.CHASE:
                    currentColorScheme.SetKeyToColor("v", Color.blue);
                    break;
                case FlightCamera.Modes.FREE:
                    currentColorScheme.SetKeyToColor("v", Color.yellow);
                    break;
                case FlightCamera.Modes.LOCKED:
                    currentColorScheme.SetKeyToColor("v", Color.cyan);
                    break;
                default:
                    currentColorScheme.SetKeyToColor("v", Color.white);
                    break;
            }
        }
    }
}
