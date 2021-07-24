namespace KspChromaControl.SceneManagers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using KspChromaControl.Animations;
    using KspChromaControl.ColorSchemes;
    using UnityEngine;

    /// <summary>
    ///     Manages the keyboard colors during all flight scenes.
    /// </summary>
    internal class FlightSceneManager : ISceneManager
    {
        private static readonly KeyCode[] rotation =
        {
            GameSettings.ROLL_LEFT.primary.code,
            GameSettings.ROLL_RIGHT.primary.code,
            GameSettings.PITCH_DOWN.primary.code,
            GameSettings.PITCH_UP.primary.code,
            GameSettings.YAW_LEFT.primary.code,
            GameSettings.YAW_RIGHT.primary.code
        };

        private static readonly KeyCode[] timewarp =
        {
            GameSettings.TIME_WARP_INCREASE.primary.code,
            GameSettings.TIME_WARP_DECREASE.primary.code
        };

        /// <summary>
        ///     Contains all ActionGroups and their current usage state. False means
        ///     this ActionGroup has no impact on any part of the vessel.
        /// </summary>
        private readonly Dictionary<KSPActionGroup, bool> actionGroups = new Dictionary<KSPActionGroup, bool>();

        /// <summary>
        ///     The current keyboard state color scheme
        /// </summary>
        private ColorScheme currentColorScheme;

        /// <summary>
        ///     The vessel we are piloting currently. Can be a normal vessel or a single
        ///     kerbal.
        /// </summary>
        private Vessel currentVessel;

        /// <summary>
        ///     Maximum relative temperature, meaning the maximum of the percentage of all parts heat / heat resistance
        /// </summary>
        private double maxTemperature;

        /// <summary>
        ///     Fills the action group list with all false values;
        /// </summary>
        public FlightSceneManager()
        {
            this.ResetActionGroups();
        }

        /// <summary>
        ///     Returns the calculated color scheme for the current game state.
        /// </summary>
        /// <returns>The final color scheme for this frame</returns>
        public ColorScheme GetScheme()
        {
            this.Update();
            return this.currentColorScheme;
        }

        /// <summary>
        ///     Recalculates every action group's usage.
        /// </summary>
        private void ResetActionGroups()
        {
            this.actionGroups.Clear();

            foreach (var group in Enum.GetValues(typeof(KSPActionGroup)).Cast<KSPActionGroup>())
            {
                if (!this.actionGroups.ContainsKey(group))
                {
                    this.actionGroups.Add(group, false);
                }
            }
        }

        /// <summary>
        ///     Called by the plugin on every physics frame.
        /// </summary>
        private void Update()
        {
            if (this.currentVessel != FlightGlobals.ActiveVessel)
            {
                this.currentVessel = FlightGlobals.fetch.activeVessel;
                this.ResetActionGroups();
                this.FindUsableActionGroups();
            }
            else if (this.currentVessel != null)
            {
                if (this.currentVessel.isEVA)
                {
                    this.currentColorScheme = new EvaScheme();
                    this.ShowGauge(
                        "EVAFuel",
                        this.currentVessel.evaController.Fuel,
                        this.currentVessel.evaController.FuelCapacity
                    );
                }
                else if (!this.currentVessel.IsControllable)
                {
                    AnimationManager.Instance.SetAnimation(new PowerLostAnimation());
                }
                else
                {
                    this.currentColorScheme = new FlightScheme();
                    this.RecalculateResources();
                    this.UpdateToggleables();
                }

                this.DisplayVesselHeight();
            }
        }

        /// <summary>
        ///     Scans the ship's parts for actions in any action group. Every action group
        ///     that has any active parts gets a toggleing button lit up.
        /// </summary>
        private void FindUsableActionGroups()
        {
            var allActionsList = new List<BaseAction>();

            foreach (var p in this.currentVessel.parts)
            {
                allActionsList.AddRange(p.Actions);

                foreach (var pm in p.Modules)
                {
                    allActionsList.AddRange(pm.Actions);
                }
            }

            foreach (var action in allActionsList)
            {
                foreach (var group in Enum.GetValues(typeof(KSPActionGroup)).Cast<KSPActionGroup>())
                {
                    this.actionGroups[group] = this.actionGroups[group] || (action.actionGroup & group) == group;
                }
            }

            // KSP ignores RCS and SAS action groups so we enable them manually
            this.actionGroups[KSPActionGroup.RCS] = true;
            this.actionGroups[KSPActionGroup.SAS] = true;
        }

        /// <summary>
        ///     Displays the fuel status as lights on the keyboard.
        /// </summary>
        private void RecalculateResources()
        {
            var resources = new Dictionary<string, KeyValuePair<double, double>>();

            foreach (var part in FlightGlobals.ActiveVessel.parts)
            {
                foreach (var resource in part.Resources)
                {
                    if (!resources.ContainsKey(resource.info.name))
                    {
                        resources.Add(resource.info.name, new KeyValuePair<double, double>(0.0f, 0.0f));
                    }

                    resources[resource.info.name] = new KeyValuePair<double, double>(
                        resources[resource.info.name].Key + resource.maxAmount,
                        resources[resource.info.name].Value + resource.amount
                    );
                }
            }

            resources.Keys.ToList().ForEach(
                res =>
                {
                    if (!this.currentColorScheme.OtherValues.ContainsKey(res))
                    {
                        this.currentColorScheme.OtherValues.Add(
                            res,
                            resources[res].Value / resources[res].Key
                        );
                    }
                    else
                    {
                        this.currentColorScheme.OtherValues[res] = resources[res].Value / resources[res].Key;
                    }

                    this.ShowGauge(res, resources[res].Value, resources[res].Key);
                }
            );

            if (!this.currentColorScheme.OtherValues.ContainsKey("Heat"))
            {
                this.currentColorScheme.OtherValues.Add("Heat", this.maxTemperature);
            }
            else
            {
                this.currentColorScheme.OtherValues["Heat"] = this.maxTemperature;
            }
        }

        /// <summary>
        ///     Displays the amount of resources left as a gauge on the keyboard
        /// </summary>
        /// <param name="resource">The name of the resource</param>
        /// <param name="amount">The actual amount of the resource in the current stage</param>
        /// <param name="maxAmount">The maximal amount of the resource in the current stage</param>
        private void ShowGauge(string resource, double amount, double maxAmount)
        {
            Func<Color, int, Color> partialColor = (original, third) =>
            {
                var newColor = new Color(
                    original.r,
                    original.g,
                    original.b,
                    original.a
                );
                var ceiling = maxAmount / 3 * (third + 1);
                var floor = maxAmount / 3 * third;

                if (amount <= ceiling)
                {
                    var factor = (float) ((amount - floor) / (ceiling - floor));
                    newColor.r *= factor;
                    newColor.g *= factor;
                    newColor.b *= factor;
                }

                if (amount - floor < 0.001)
                {
                    newColor = Color.black;
                }

                return newColor;
            };

            Action<KeyCode[], Color> displayFuel = (keys, color) =>
            {
                for (var i = 0; i < 3; i++)
                {
                    this.currentColorScheme.SetKeyToColor(keys[i], partialColor(color, i));
                }
            };

            switch (resource)
            {
                case "ElectricCharge":
                    KeyCode[] electric = {KeyCode.Print, KeyCode.ScrollLock, KeyCode.Pause};
                    displayFuel(electric, Color.blue);
                    break;
                case "LiquidFuel":
                    KeyCode[] liquid = {KeyCode.Numlock, KeyCode.KeypadDivide, KeyCode.KeypadMultiply};
                    displayFuel(liquid, Color.green);
                    break;
                case "Oxidizer":
                    KeyCode[] oxidizer = {KeyCode.Keypad7, KeyCode.Keypad8, KeyCode.Keypad9};
                    displayFuel(oxidizer, Color.cyan);
                    break;
                case "MonoPropellant":
                case "EVAFuel":
                    KeyCode[] monoprop = {KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6};
                    displayFuel(monoprop, Color.yellow);
                    break;
                case "SolidFuel":
                    KeyCode[] solid = {KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3};
                    displayFuel(solid, Color.magenta);
                    break;
                case "Ablator":
                    KeyCode[] ablator = {KeyCode.Delete, KeyCode.End, KeyCode.PageDown};
                    displayFuel(
                        ablator,
                        new Color(
                            244,
                            259,
                            0,
                            255
                        )
                    );
                    break;
                case "XenonGas":
                    KeyCode[] xenon = {KeyCode.Insert, KeyCode.Home, KeyCode.PageUp};
                    displayFuel(xenon, Color.gray);
                    break;
            }
        }

        /// <summary>
        ///     Updates all toggleable buttons on the keyboard.
        /// </summary>
        private void UpdateToggleables()
        {
            // Updates all toggleable action group keys
            foreach (var agroup in this.actionGroups)
            {
                if (agroup.Key != KSPActionGroup.None && Config.Instance.ActionGroupConf.ContainsKey(agroup.Key))
                {
                    if (!agroup.Value)
                    {
                        this.currentColorScheme.SetKeyToColor(
                            Config.Instance.ActionGroupConf[agroup.Key].Key.primary.code,
                            Color.black
                        );
                    }
                    else if (this.currentVessel.ActionGroups[agroup.Key])
                    {
                        this.currentColorScheme.SetKeyToColor(
                            Config.Instance.ActionGroupConf[agroup.Key].Key.primary.code,
                            Config.Instance.ActionGroupConf[agroup.Key].Value.Value
                        );
                    }
                    else
                    {
                        this.currentColorScheme.SetKeyToColor(
                            Config.Instance.ActionGroupConf[agroup.Key].Key.primary.code,
                            Config.Instance.ActionGroupConf[agroup.Key].Value.Key
                        );
                    }
                }
            }

            // Colors the map view key
            this.currentColorScheme.SetKeyToColor(
                GameSettings.MAP_VIEW_TOGGLE.primary.code,
                MapView.MapIsEnabled ? Config.Instance.RedGreenToggle.Value : Config.Instance.RedGreenToggle.Key
            );

            // Lights steering buttons differently if precision mode is on
            if (FlightInputHandler.fetch.precisionMode)
            {
                this.currentColorScheme.SetKeysToColor(rotation, Color.yellow);
                this.currentColorScheme.SetKeyToColor(GameSettings.PRECISION_CTRL.primary.code, Color.green);
            }
            else
            {
                this.currentColorScheme.SetKeysToColor(rotation, Color.white);
                this.currentColorScheme.SetKeyToColor(GameSettings.PRECISION_CTRL.primary.code, Color.red);
            }

            // Lights the quicksave button green, if it is enabled, red otherwise
            if (this.currentVessel.IsClearToSave() == ClearToSaveStatus.CLEAR ||
                this.currentVessel.IsClearToSave() == ClearToSaveStatus.NOT_IN_ATMOSPHERE ||
                this.currentVessel.IsClearToSave() == ClearToSaveStatus.NOT_UNDER_ACCELERATION)
            {
                this.currentColorScheme.SetKeyToColor(GameSettings.QUICKSAVE.primary.code, Color.green);
            }
            else
            {
                this.currentColorScheme.SetKeyToColor(GameSettings.QUICKSAVE.primary.code, Color.red);
            }

            // Lights up the quickload button
            this.currentColorScheme.SetKeyToColor(GameSettings.QUICKLOAD.primary.code, Color.green);

            // Colors the timewarp buttons red and green for physics and on-rails warp
            if (TimeWarp.WarpMode == TimeWarp.Modes.HIGH)
            {
                this.currentColorScheme.SetKeysToColor(timewarp, Color.green);
            }
            else
            {
                this.currentColorScheme.SetKeysToColor(timewarp, Color.red);
            }

            // Different colors for the camera mode switch
            switch (FlightCamera.fetch.mode)
            {
                case FlightCamera.Modes.AUTO:
                    this.currentColorScheme.SetKeyToColor(GameSettings.CAMERA_NEXT.primary.code, Color.green);
                    break;
                case FlightCamera.Modes.CHASE:
                    this.currentColorScheme.SetKeyToColor(GameSettings.CAMERA_NEXT.primary.code, Color.blue);
                    break;
                case FlightCamera.Modes.FREE:
                    this.currentColorScheme.SetKeyToColor(GameSettings.CAMERA_NEXT.primary.code, Color.yellow);
                    break;
                case FlightCamera.Modes.LOCKED:
                    this.currentColorScheme.SetKeyToColor(GameSettings.CAMERA_NEXT.primary.code, Color.cyan);
                    break;
                default:
                    this.currentColorScheme.SetKeyToColor(GameSettings.CAMERA_NEXT.primary.code, Color.white);
                    break;
            }
        }

        /// <summary>
        ///     Height off ground display on F keys from F1 to F4.
        /// </summary>
        private void DisplayVesselHeight()
        {
            double[] heightLimits =
            {
                10.0, 20.0, 100.0, 1000.0
            };

            KeyCode[] heightScaleKeys =
            {
                KeyCode.F1, KeyCode.F2, KeyCode.F3, KeyCode.F4
            };

            for (var i = 0; i < heightScaleKeys.Length; i++)
            {
                var floor = i > 0 ? heightLimits[i - 1] : 0;
                var ceiling = heightLimits[i];
                var vesselHeight = this.CalculateDistanceFromGroundAndTemperaturePercentage();
                Color newColor = new Color32(
                    0,
                    100,
                    100,
                    255
                );

                if (vesselHeight > ceiling)
                {
                    this.currentColorScheme.SetKeyToColor(heightScaleKeys[i], newColor);
                }
                else if (vesselHeight > floor)
                {
                    var factor = (float) ((vesselHeight - floor) / (ceiling - floor));
                    newColor.r *= factor;
                    newColor.g *= factor;
                    newColor.b *= factor;
                    this.currentColorScheme.SetKeyToColor(heightScaleKeys[i], newColor);
                }
            }
        }

        /// <summary>
        ///     Calculates the ground distance for the vessel. Also calculates the maximum temperature percentage
        ///     because why iterate over all parts twice.
        /// </summary>
        /// <returns></returns>
        private double CalculateDistanceFromGroundAndTemperaturePercentage()
        {
            this.maxTemperature = 0.0;
            var coM = this.currentVessel.CurrentCoM; //Gets CoM
            Vector3 up = FlightGlobals.getUpAxis(coM); //Gets up axis (needed for the raycast)
            var asl = FlightGlobals.getAltitudeAtPos(coM);
            RaycastHit craft;
            float trueAlt;
            float surfaceAlt;
            float bottomAlt;

            if (Physics.Raycast(
                coM,
                -up,
                out craft,
                asl + 10000f,
                1 << 15
            ))
            {
                trueAlt = Mathf.Min(asl, craft.distance); //Smallest value between ASL and distance from ground
            }

            else
            {
                trueAlt = asl;
            }

            surfaceAlt = asl - trueAlt;
            bottomAlt = trueAlt; //Initiation to be sure the loop doesn't return a false value

            foreach (var p in this.currentVessel.parts)
            {
                if (p.collider != null) //Makes sure the part actually has a collider to touch ground
                {
                    var bottom =
                        p.collider.ClosestPointOnBounds(this.currentVessel.mainBody.position); //Gets the bottom point
                    var partAlt = FlightGlobals.getAltitudeAtPos(bottom) - surfaceAlt; //Gets the looped part alt
                    bottomAlt = Mathf.Max(
                        0,
                        Mathf.Min(bottomAlt, partAlt)
                    ); //Stores the smallest value in all the parts
                }

                this.maxTemperature = Math.Max(p.skinTemperature / p.skinMaxTemp, this.maxTemperature);
            }

            return bottomAlt;
        }
    }
}