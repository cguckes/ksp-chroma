using System;
using System.Collections.Generic;
using System.Linq;
using KspChromaControl.ColorSchemes;
using UnityEngine;
using Corale.Colore.Razer.Keyboard;

namespace KspChromaControl.SceneManagers
{
    /// <summary>
    /// Manages the keyboard colors during all flight scenes.
    /// </summary>
    internal class FlightSceneManager : SceneManager
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
        /// Maximum relative temperature, meaning the maximum of the percentage of all parts heat / heat resistance
        /// </summary>
        private double maxTemperature = 0.0;

        /// <summary>
        /// Contains all ActionGroups and their current usage state. False means
        /// this ActionGroup has no impact on any part of the vessel.
        /// </summary>
        private Dictionary<KSPActionGroup, Boolean> actionGroups = new Dictionary<KSPActionGroup, Boolean>();

        private static KeyCode[] rotation = new KeyCode[]
        {
                GameSettings.ROLL_LEFT.primary.code,
                GameSettings.ROLL_RIGHT.primary.code,
                GameSettings.PITCH_DOWN.primary.code,
                GameSettings.PITCH_UP.primary.code,
                GameSettings.YAW_LEFT.primary.code,
                GameSettings.YAW_RIGHT.primary.code
        };

        private static KeyCode[] timewarp = new KeyCode[]
        {
            GameSettings.TIME_WARP_INCREASE.primary.code,
            GameSettings.TIME_WARP_DECREASE.primary.code
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
            else if (currentVessel != null)
            {

                if (currentVessel.isEVA)
                {
                    this.currentColorScheme = new EVAScheme();
                    showGauge("EVAFuel", currentVessel.evaController.Fuel, currentVessel.evaController.FuelCapacity);
                }
                else if (!currentVessel.IsControllable)
                {
                    AnimationManager.Instance.setAnimation(new PowerLostAnimation());
                }
                else
                {
                    this.currentColorScheme = new FlightScheme();
                    recalculateResources();
                    updateToggleables();
                }
                this.displayVesselHeight();
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
            Dictionary<String, KeyValuePair<double, double>> resources = new Dictionary<string, KeyValuePair<double, double>>();
            foreach (Part part in FlightGlobals.ActiveVessel.parts)
            {
                foreach (PartResource resource in part.Resources)
                {
                    if(!resources.ContainsKey(resource.info.name))
                    {
                        resources.Add(resource.info.name, new KeyValuePair<double, double>(0.0f, 0.0f));
                    }
                    resources[resource.info.name] = new KeyValuePair<double, double>(
                        resources[resource.info.name].Key + resource.maxAmount,
                        resources[resource.info.name].Value + resource.amount
                    );
                }
            }

            resources.Keys.ToList().ForEach(res =>
            {
                if (!currentColorScheme.otherValues.ContainsKey(res))
                    currentColorScheme.otherValues.Add(res, (resources[res].Value / resources[res].Key));
                else
                    currentColorScheme.otherValues[res] = resources[res].Value / resources[res].Key;

                showGauge(res, resources[res].Value, resources[res].Key);
            });

            if (!currentColorScheme.otherValues.ContainsKey("Heat"))
                currentColorScheme.otherValues.Add("Heat", maxTemperature);
            else
                currentColorScheme.otherValues["Heat"] = maxTemperature;
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
                    KeyCode[] electric = { KeyCode.Print, KeyCode.ScrollLock, KeyCode.Pause };
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
            /// Updates all toggleable action group keys
            foreach (KeyValuePair<KSPActionGroup, Boolean> agroup in actionGroups)
            {
                if (agroup.Key != KSPActionGroup.None && Config.Instance.actionGroupConf.ContainsKey(agroup.Key))
                {
                    if (!agroup.Value)
                        currentColorScheme.SetKeyToColor(Config.Instance.actionGroupConf[agroup.Key].Key.primary.code, Color.black);
                    else if (currentVessel.ActionGroups[agroup.Key])
                        currentColorScheme.SetKeyToColor(Config.Instance.actionGroupConf[agroup.Key].Key.primary.code,
                            Config.Instance.actionGroupConf[agroup.Key].Value.Value);
                    else
                        currentColorScheme.SetKeyToColor(Config.Instance.actionGroupConf[agroup.Key].Key.primary.code,
                            Config.Instance.actionGroupConf[agroup.Key].Value.Key);
                }
            }

            /// Colors the map view key
            currentColorScheme.SetKeyToColor(
                GameSettings.MAP_VIEW_TOGGLE.primary.code,
                (MapView.MapIsEnabled ? Config.Instance.redGreenToggle.Value : Config.Instance.redGreenToggle.Key)
            );

            /// Lights steering buttons differently if precision mode is on
            if (FlightInputHandler.fetch.precisionMode)
            {
                currentColorScheme.SetKeysToColor(rotation, Color.yellow);
                currentColorScheme.SetKeyToColor(GameSettings.PRECISION_CTRL.primary.code, Color.green);
            }
            else
            {
                currentColorScheme.SetKeysToColor(rotation, Color.white);
                currentColorScheme.SetKeyToColor(GameSettings.PRECISION_CTRL.primary.code, Color.red);
            }

            /// Lights the quicksave button green, if it is enabled, red otherwise
            if (currentVessel.IsClearToSave() == ClearToSaveStatus.CLEAR ||
                currentVessel.IsClearToSave() == ClearToSaveStatus.NOT_IN_ATMOSPHERE ||
                currentVessel.IsClearToSave() == ClearToSaveStatus.NOT_UNDER_ACCELERATION)
                currentColorScheme.SetKeyToColor(GameSettings.QUICKSAVE.primary.code, Color.green);
            else
                currentColorScheme.SetKeyToColor(GameSettings.QUICKSAVE.primary.code, Color.red);

            /// Lights up the quickload button
            currentColorScheme.SetKeyToColor(GameSettings.QUICKLOAD.primary.code, Color.green);

            /// Colors the timewarp buttons red and green for physics and on-rails warp
            if (TimeWarp.WarpMode == TimeWarp.Modes.HIGH)
                currentColorScheme.SetKeysToColor(timewarp, Color.green);
            else
                currentColorScheme.SetKeysToColor(timewarp, Color.red);

            /// Different colors for the camera mode switch
            switch (FlightCamera.fetch.mode)
            {
                case FlightCamera.Modes.AUTO:
                    currentColorScheme.SetKeyToColor(GameSettings.CAMERA_NEXT.primary.code, Color.green);
                    break;
                case FlightCamera.Modes.CHASE:
                    currentColorScheme.SetKeyToColor(GameSettings.CAMERA_NEXT.primary.code, Color.blue);
                    break;
                case FlightCamera.Modes.FREE:
                    currentColorScheme.SetKeyToColor(GameSettings.CAMERA_NEXT.primary.code, Color.yellow);
                    break;
                case FlightCamera.Modes.LOCKED:
                    currentColorScheme.SetKeyToColor(GameSettings.CAMERA_NEXT.primary.code, Color.cyan);
                    break;
                default:
                    currentColorScheme.SetKeyToColor(GameSettings.CAMERA_NEXT.primary.code, Color.white);
                    break;
            }
        }

        /// <summary>
        /// Height off ground display on F keys from F1 to F4.
        /// </summary>
        private void displayVesselHeight()
        {
            double[] heightLimits = new double[]
            {
                10.0, 20.0, 100.0, 1000.0
            };

            KeyCode[] heightScaleKeys = new KeyCode[]
            {
                KeyCode.F1, KeyCode.F2, KeyCode.F3, KeyCode.F4
            };

            for(int i = 0; i < heightScaleKeys.Length; i++)
            {
                double floor = (i > 0) ? heightLimits[i - 1] : 0;
                double ceiling = heightLimits[i];
                double vesselHeight = calculateDistanceFromGroundAndTemperaturePercentage();
                Color newColor = new Color32(0, 100, 100, 255);

                if (vesselHeight > ceiling)
                    currentColorScheme.SetKeyToColor(heightScaleKeys[i], newColor);
                else if(vesselHeight > floor)
                {
                    float factor = (float)((vesselHeight - floor) / (ceiling - floor));
                    newColor.r *= factor;
                    newColor.g *= factor;
                    newColor.b *= factor;
                    currentColorScheme.SetKeyToColor(heightScaleKeys[i], newColor);
                }
            }
        }

        /// <summary>
        /// Calculates the ground distance for the vessel. Also calculates the maximum temperature percentage
        /// because why iterate over all parts twice.
        /// </summary>
        /// <returns></returns>
        private double calculateDistanceFromGroundAndTemperaturePercentage()
        {
            maxTemperature = 0.0;
            Vector3 CoM = currentVessel.CurrentCoM;  //Gets CoM
            Vector3 up = FlightGlobals.getUpAxis(CoM); //Gets up axis (needed for the raycast)
            float ASL = FlightGlobals.getAltitudeAtPos(CoM);
            RaycastHit craft;
            float trueAlt;
            float surfaceAlt;
            float bottomAlt;

            if (Physics.Raycast(CoM, -up, out craft, ASL + 10000f, 1 << 15))
            {
                trueAlt = Mathf.Min(ASL, craft.distance); //Smallest value between ASL and distance from ground
            }

            else { trueAlt = ASL; }

            surfaceAlt = ASL - trueAlt;
            bottomAlt = trueAlt; //Initiation to be sure the loop doesn't return a false value
            foreach (Part p in currentVessel.parts)
            {
                if (p.collider != null) //Makes sure the part actually has a collider to touch ground
                {
                    Vector3 bottom = p.collider.ClosestPointOnBounds(currentVessel.mainBody.position); //Gets the bottom point
                    float partAlt = FlightGlobals.getAltitudeAtPos(bottom) - surfaceAlt;  //Gets the looped part alt
                    bottomAlt = Mathf.Max(0, Mathf.Min(bottomAlt, partAlt));  //Stores the smallest value in all the parts
                }

                maxTemperature = Math.Max(p.skinTemperature / p.skinMaxTemp, maxTemperature);
            }

            return bottomAlt;
        }
    }
}
