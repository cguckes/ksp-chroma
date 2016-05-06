using Corale.Colore.Razer.Keyboard;
using KspChromaControl.ColorSchemes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KspChromaControl
{
    /// <summary>
    /// Stores configuration settings for the whole mod. Implemented as singleton.
    /// </summary>
    internal class Config
    {
        /// <summary>
        /// Singleton instance
        /// </summary>
        private static Config instance;

        /// <summary>
        /// Singleton getter / initializer
        /// </summary>
        public static Config Instance
        {
            get
            {
                if (Config.instance == null)
                {
                    Config.instance = new Config();
                }
                return Config.instance;
            }
        }

        /// <summary>
        /// Colors for red and orange toggle keys.
        /// </summary>
        public readonly KeyValuePair<Color, Color> redOrangeToggle = new KeyValuePair<Color, Color>(Color.red, new Color32(255, 100, 0, 255));

        /// <summary>
        /// Colors for red and green toggle keys.
        /// </summary>
        public readonly KeyValuePair<Color, Color> redGreenToggle = new KeyValuePair<Color, Color>(Color.red, Color.green);

        /// <summary>
        /// Colors for cyan and blue toggle keys.
        /// </summary>
        public readonly KeyValuePair<Color, Color> cyanBlueToggle = new KeyValuePair<Color, Color>(Color.cyan, Color.blue);

        /// <summary>
        /// Allows getting a key via it's coordinates, rather than it's keycodes. Necessary for animations to work as expected.
        /// Default values are calibrated for razer devices. (Most animations should be ok on other devices too).
        /// </summary>
        public KeyCode[,] KeyByPosition { get; set; } = new KeyCode[6,22]
        {
            {
                KeyCode.None, KeyCode.Escape, KeyCode.None, KeyCode.F1, KeyCode.F2,
                KeyCode.F3, KeyCode.F4, KeyCode.F5, KeyCode.F6, KeyCode.F7,
                KeyCode.F8, KeyCode.F9, KeyCode.F10, KeyCode.F11, KeyCode.F12, KeyCode.Print,
                KeyCode.ScrollLock, KeyCode.Pause, KeyCode.None, KeyCode.None,
                KeyCode.None, KeyCode.None
            },
            {
                KeyCode.None, KeyCode.BackQuote, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3,
                KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8,
                KeyCode.Alpha9, KeyCode.Alpha0, KeyCode.Minus, KeyCode.Equals, KeyCode.Backspace,
                KeyCode.Insert, KeyCode.Home, KeyCode.PageUp, KeyCode.Numlock, KeyCode.KeypadDivide,
                KeyCode.KeypadMultiply, KeyCode.KeypadMinus },
            {
                KeyCode.None, KeyCode.Tab, KeyCode.Q, KeyCode.W, KeyCode.E,
                KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I,
                KeyCode.O, KeyCode.P, KeyCode.LeftBracket, KeyCode.RightBracket, KeyCode.Backslash,
                KeyCode.Delete, KeyCode.End, KeyCode.PageDown, KeyCode.Keypad7, KeyCode.Keypad8,
                KeyCode.Keypad9,KeyCode.KeypadPlus
            },
            {
                KeyCode.None, KeyCode.CapsLock, KeyCode.A, KeyCode.S, KeyCode.D,
                KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K,
                KeyCode.L, KeyCode.Semicolon, KeyCode.Colon, KeyCode.Hash, KeyCode.Return,
                KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.Keypad4, KeyCode.Keypad5,
                KeyCode.Keypad6, KeyCode.None
            },
            {
                KeyCode.None, KeyCode.LeftShift, KeyCode.None, KeyCode.Z, KeyCode.X,
                KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M,
                KeyCode.Comma, KeyCode.Period, KeyCode.Slash, KeyCode.None, KeyCode.RightShift,
                KeyCode.None, KeyCode.UpArrow, KeyCode.None, KeyCode.Keypad1, KeyCode.Keypad2,
                KeyCode.Keypad3, KeyCode.KeypadEnter
            },
            {
                KeyCode.None, KeyCode.LeftControl, KeyCode.LeftWindows, KeyCode.LeftAlt, KeyCode.None,
                KeyCode.None, KeyCode.None, KeyCode.Space, KeyCode.None, KeyCode.None,
                KeyCode.None, KeyCode.RightAlt, KeyCode.AltGr, KeyCode.Menu, KeyCode.RightControl,
                KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.None, KeyCode.Keypad0,
                KeyCode.KeypadPeriod, KeyCode.None
            },
        };

        /// <summary>
        /// Configures the key binding and colors for every action group
        /// </summary>
        public readonly Dictionary<KSPActionGroup, KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>> actionGroupConf;

        /// <summary>
        /// Private constructor to avoid instantiation outside of our singleton logic.
        /// </summary>
        private Config()
        {
            actionGroupConf = new Dictionary<KSPActionGroup, KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>>()
            {
                { KSPActionGroup.Abort, new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.AbortActionGroup, redOrangeToggle) },
                { KSPActionGroup.Brakes, new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.BRAKES, redGreenToggle) },
                { KSPActionGroup.Custom01, new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.CustomActionGroup1, cyanBlueToggle) },
                { KSPActionGroup.Custom02, new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.CustomActionGroup2, cyanBlueToggle) },
                { KSPActionGroup.Custom03, new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.CustomActionGroup3, cyanBlueToggle) },
                { KSPActionGroup.Custom04, new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.CustomActionGroup4, cyanBlueToggle) },
                { KSPActionGroup.Custom05, new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.CustomActionGroup5, cyanBlueToggle) },
                { KSPActionGroup.Custom06, new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.CustomActionGroup6, cyanBlueToggle) },
                { KSPActionGroup.Custom07, new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.CustomActionGroup7, cyanBlueToggle) },
                { KSPActionGroup.Custom08, new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.CustomActionGroup8, cyanBlueToggle) },
                { KSPActionGroup.Custom09, new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.CustomActionGroup9, cyanBlueToggle) },
                { KSPActionGroup.Custom10, new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.CustomActionGroup10, cyanBlueToggle) },
                { KSPActionGroup.Gear, new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.LANDING_GEAR, redGreenToggle) },
                { KSPActionGroup.Light, new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.HEADLIGHT_TOGGLE, redGreenToggle) },
                { KSPActionGroup.RCS, new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.RCS_TOGGLE, redGreenToggle) },
                { KSPActionGroup.SAS, new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.SAS_TOGGLE, redGreenToggle) },
                { KSPActionGroup.Stage, new KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>(GameSettings.UIMODE_STAGING, redGreenToggle) }
            };
        }
    }
}
