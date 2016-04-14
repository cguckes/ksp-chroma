using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSP_Chroma_Control.ColorSchemes;
using UnityEngine;

namespace KSP_Chroma_Control.SceneManagers
{
    class FlightSceneManager : SceneManager
    {
        private Vessel currentVessel;
        private ColorScheme currentColorScheme;
        private Dictionary<KSPActionGroup, Boolean> actionGroups = new Dictionary<KSPActionGroup, Boolean>();

        /// <summary>
        /// We are using integers because we dont need any accuracy for the three keys
        /// we can light up.
        /// </summary>

        public ColorScheme getScheme()
        {
            update();            
            return this.currentColorScheme;
        }

        private void update()
        {
            if (this.currentVessel != FlightGlobals.ActiveVessel)
            {
                this.currentVessel = FlightGlobals.fetch.activeVessel;
                findUsableActionGroups();
            }

            if (currentVessel.isEVA)
            {
                this.currentColorScheme = new EVAScheme();

            }
            else
            {
                this.currentColorScheme = new FlightScheme();
                recalculateResources();
                updateToggleables();
            }
        }

        private void calculateEvaResources()
        {

        }

        private void findUsableActionGroups()
        {
            List<BaseAction> allActionsList = new List<BaseAction>();

            foreach (KSPActionGroup group in Enum.GetValues(typeof(KSPActionGroup)).Cast<KSPActionGroup>())
                this.actionGroups.Add(group, false);

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
            else if (resource.Equals("MonoPropellant"))
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

        private void updateToggleables()
        {
            currentColorScheme.SetKeysToColor(new string[] { "f5", "del", "t", "r", "m" }, Color.red);

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

            for (int i = 1; i <= 10; i++)
            {
                KSPActionGroup action = (KSPActionGroup)System.Enum.Parse(typeof(KSPActionGroup), "Custom" + i.ToString("D2"));
                string key = ((i == 10) ? "0" : i.ToString());
                if(!actionGroups[action])
                    currentColorScheme.SetKeyToColor(key, Color.black);
                else if (currentVessel.ActionGroups[action])
                    currentColorScheme.SetKeyToColor(key, Color.blue);
                else
                    currentColorScheme.SetKeyToColor(key, (Color) new Color32(50, 50, 255, 255));
            }
        }
    }
}
