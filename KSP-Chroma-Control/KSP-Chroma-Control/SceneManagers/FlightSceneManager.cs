using System;
using System.Collections.Generic;
using System.Linq;
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

        private static KeyCode[] rotation = new KeyCode[]
        {
                GameSettings.ROLL_LEFT.primary,
                GameSettings.ROLL_RIGHT.primary,
                GameSettings.PITCH_DOWN.primary,
                GameSettings.PITCH_UP.primary,
                GameSettings.YAW_LEFT.primary,
                GameSettings.YAW_RIGHT.primary
        };

        private static KeyCode[] translation = new KeyCode[]
        {
            GameSettings.TRANSLATE_BACK.primary,
            GameSettings.TRANSLATE_FWD.primary,
            GameSettings.TRANSLATE_LEFT.primary,
            GameSettings.TRANSLATE_RIGHT.primary,
            GameSettings.TRANSLATE_UP.primary,
            GameSettings.TRANSLATE_DOWN.primary
        };

        private static KeyCode[] timewarp = new KeyCode[]
        {
            GameSettings.TIME_WARP_INCREASE.primary,
            GameSettings.TIME_WARP_DECREASE.primary
        };

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
            {
                if (!this.actionGroups.ContainsKey(group))
                {
                    this.actionGroups.Add(group, false);
                }
            }
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
                showGauge("EVAFuel", currentVessel.evaController.Fuel, currentVessel.evaController.FuelCapacity);
            }
            else
            {
                this.currentColorScheme = new FlightScheme();
                recalculateResources();
                updateToggleables();
            }
        }

        /// <summary>
        /// Scans the ship's parts for actions in any action group. Every action group
        /// that has any active parts gets a toggleing button lit up.
        /// </summary>
        private void findUsableActionGroups()
        {
            List<BaseAction> allActionsList = new List<BaseAction>();

            foreach (Part p in currentVessel.parts)
            {
                allActionsList.AddRange(p.Actions);
                foreach (PartModule pm in p.Modules)
                    allActionsList.AddRange(pm.Actions);
            }

            foreach (BaseAction action in allActionsList)
                foreach (KSPActionGroup group in Enum.GetValues(typeof(KSPActionGroup)).Cast<KSPActionGroup>())
                    actionGroups[group] = actionGroups[group] || ((action.actionGroup & group) == group);

            ///KSP ignores RCS and SAS action groups so we enable them manually
            actionGroups[KSPActionGroup.RCS] = true;
            actionGroups[KSPActionGroup.SAS] = true;
        }

        /// <summary>
        /// Displays the fuel status as lights on the keyboard.
        /// </summary>
        private void recalculateResources()
        {
            List<Vessel.ActiveResource> resources = currentVessel.GetActiveResources();

            resources.ForEach(res =>
            {
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
            Func<Color, int, Color> partialColor = (original, third) =>
            {
                Color newColor = new Color(original.r, original.g, original.b, original.a);
                double ceiling = maxAmount / 3 * (third + 1);
                double floor = maxAmount / 3 * third;

                if (amount <= ceiling) { 
                    float factor = (float)((amount - floor) / (ceiling - floor));
                    newColor.r *= factor;
                    newColor.g *= factor;
                    newColor.b *= factor;
                }
                if ((amount - floor) < 0.001)
                    newColor = Color.black;
                return newColor;
            };

            Action<KeyCode[], Color> displayFuel = (keys, color) => {
                for(int i = 0; i < 3; i++) { 
                    currentColorScheme.SetKeyToColor(keys[i], partialColor(color, i));
                }
            };

            switch (resource)
            {
                case "ElectricCharge":
                    KeyCode[] electric = { KeyCode.Print, KeyCode.ScrollLock, KeyCode.Break };
                    displayFuel(electric, Color.blue);
                    break;
                case "LiquidFuel":
                    KeyCode[] liquid = { KeyCode.Numlock, KeyCode.KeypadDivide, KeyCode.KeypadMultiply };
                    displayFuel(liquid, Color.green);
                    break;
                case "Oxidizer":
                    KeyCode[] oxidizer = { KeyCode.Keypad7, KeyCode.Keypad8, KeyCode.Keypad9 };
                    displayFuel(oxidizer, Color.cyan);
                    break;
                case "MonoPropellant":
                case "EVAFuel":
                    KeyCode[] monoprop = { KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6 };
                    displayFuel(monoprop, Color.yellow);
                    break;
                case "SolidFuel":
                    KeyCode[] solid = { KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3 };
                    displayFuel(solid, Color.magenta);
                    break;
                case "Ablator":
                    KeyCode[] ablator = { KeyCode.Delete, KeyCode.End, KeyCode.PageDown };
                    displayFuel(ablator, new Color(244, 259, 0, 255));
                    break;
                case "XenonGas":
                    KeyCode[] xenon = { KeyCode.Insert, KeyCode.Home, KeyCode.PageUp };
                    displayFuel(xenon, Color.gray);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Updates all toggleable buttons on the keyboard.
        /// </summary>
        private void updateToggleables()
        {
            foreach (KeyValuePair<KSPActionGroup, Boolean> agroup in actionGroups)
            {
                if (agroup.Key != KSPActionGroup.None)
                {
                    if (!agroup.Value)
                        currentColorScheme.SetKeyToColor(Config.Instance.actionGroupConf[agroup.Key].Key.primary, Color.black);
                    else if (currentVessel.ActionGroups[agroup.Key])
                        currentColorScheme.SetKeyToColor(Config.Instance.actionGroupConf[agroup.Key].Key.primary,
                            Config.Instance.actionGroupConf[agroup.Key].Value.Value);
                    else
                        currentColorScheme.SetKeyToColor(Config.Instance.actionGroupConf[agroup.Key].Key.primary,
                            Config.Instance.actionGroupConf[agroup.Key].Value.Key);
                }
            }

            currentColorScheme.SetKeyToColor(
                GameSettings.MAP_VIEW_TOGGLE.primary,
                (MapView.MapIsEnabled ? Config.Instance.redGreenToggle.Value : Config.Instance.redGreenToggle.Key)
            );

            if (FlightInputHandler.fetch.precisionMode)
            {
                currentColorScheme.SetKeysToColor(rotation, Color.yellow);
                currentColorScheme.SetKeyToColor(GameSettings.PRECISION_CTRL.primary, Color.green);
            }
            else
            {
                currentColorScheme.SetKeysToColor(rotation, Color.white);
                currentColorScheme.SetKeyToColor(GameSettings.PRECISION_CTRL.primary, Color.red);
            }

            if (currentVessel.IsClearToSave() == ClearToSaveStatus.CLEAR ||
                currentVessel.IsClearToSave() == ClearToSaveStatus.NOT_IN_ATMOSPHERE ||
                currentVessel.IsClearToSave() == ClearToSaveStatus.NOT_UNDER_ACCELERATION)
                currentColorScheme.SetKeyToColor(GameSettings.QUICKSAVE.primary, Color.green);
            currentColorScheme.SetKeyToColor(GameSettings.QUICKLOAD.primary, Color.green);

            if (TimeWarp.WarpMode == TimeWarp.Modes.HIGH)
                currentColorScheme.SetKeysToColor(timewarp, Color.green);
            else
                currentColorScheme.SetKeysToColor(timewarp, Color.red);

            switch (FlightCamera.fetch.mode)
            {
                case FlightCamera.Modes.AUTO:
                    currentColorScheme.SetKeyToColor(GameSettings.CAMERA_NEXT.primary, Color.green);
                    break;
                case FlightCamera.Modes.CHASE:
                    currentColorScheme.SetKeyToColor(GameSettings.CAMERA_NEXT.primary, Color.blue);
                    break;
                case FlightCamera.Modes.FREE:
                    currentColorScheme.SetKeyToColor(GameSettings.CAMERA_NEXT.primary, Color.yellow);
                    break;
                case FlightCamera.Modes.LOCKED:
                    currentColorScheme.SetKeyToColor(GameSettings.CAMERA_NEXT.primary, Color.cyan);
                    break;
                default:
                    currentColorScheme.SetKeyToColor(GameSettings.CAMERA_NEXT.primary, Color.white);
                    break;
            }
        }
    }
}
