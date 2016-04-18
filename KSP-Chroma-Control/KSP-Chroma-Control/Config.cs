using Corale.Colore.Razer.Keyboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSP_Chroma_Control
{
    public class Config
    {
        private static Config instance;
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

        public readonly KeyValuePair<Color, Color> redOrangeToggle = new KeyValuePair<Color, Color>(Color.red, new Color32(255, 100, 0, 255));
        public readonly KeyValuePair<Color, Color> redGreenToggle = new KeyValuePair<Color, Color>(Color.red, Color.green);
        public readonly KeyValuePair<Color, Color> cyanBlueToggle = new KeyValuePair<Color, Color>(Color.cyan, Color.blue);

        /// <summary>
        /// Configures the key binding and colors for every action group
        /// </summary>
        public readonly Dictionary<KSPActionGroup, KeyValuePair<KeyBinding, KeyValuePair<Color, Color>>> actionGroupConf;

        /// <summary>
        /// Unity Keybinding <=> UK Layout translation dictionary
        /// </summary>
        public readonly Dictionary<KeyCode, Key> keyMapping;

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

            keyMapping = new Dictionary<KeyCode, Key>();
            keyMapping.Add(KeyCode.A, Key.A);
            keyMapping.Add(KeyCode.Alpha0, Key.D0);
            keyMapping.Add(KeyCode.Alpha1, Key.D1);
            keyMapping.Add(KeyCode.Alpha2, Key.D2);
            keyMapping.Add(KeyCode.Alpha3, Key.D3);
            keyMapping.Add(KeyCode.Alpha4, Key.D4);
            keyMapping.Add(KeyCode.Alpha5, Key.D5);
            keyMapping.Add(KeyCode.Alpha6, Key.D6);
            keyMapping.Add(KeyCode.Alpha7, Key.D7);
            keyMapping.Add(KeyCode.Alpha8, Key.D8);
            keyMapping.Add(KeyCode.Alpha9, Key.D9);
            keyMapping.Add(KeyCode.AltGr, Key.RightAlt);
            keyMapping.Add(KeyCode.Ampersand, Key.D7);
            keyMapping.Add(KeyCode.Asterisk, Key.D8);
            keyMapping.Add(KeyCode.At, Key.OemApostrophe);

            keyMapping.Add(KeyCode.B, Key.B);
            keyMapping.Add(KeyCode.BackQuote, Key.OemTilde);
            keyMapping.Add(KeyCode.Backslash, Key.EurBackslash);
            keyMapping.Add(KeyCode.Backspace, Key.Backspace);
            keyMapping.Add(KeyCode.Break, Key.Pause);

            keyMapping.Add(KeyCode.C, Key.C);
            keyMapping.Add(KeyCode.CapsLock, Key.CapsLock);
            keyMapping.Add(KeyCode.Caret, Key.D6);
            keyMapping.Add(KeyCode.Colon, Key.OemPeriod);
            keyMapping.Add(KeyCode.Comma, Key.OemComma);

            keyMapping.Add(KeyCode.D, Key.D);
            keyMapping.Add(KeyCode.Delete, Key.Delete);
            keyMapping.Add(KeyCode.Dollar, Key.D4);
            keyMapping.Add(KeyCode.DoubleQuote, Key.D2);
            keyMapping.Add(KeyCode.DownArrow, Key.Down);

            keyMapping.Add(KeyCode.E, Key.E);
            keyMapping.Add(KeyCode.End, Key.End);
            keyMapping.Add(KeyCode.Equals, Key.OemEquals);
            keyMapping.Add(KeyCode.Escape, Key.Escape);
            keyMapping.Add(KeyCode.Exclaim, Key.D1);

            keyMapping.Add(KeyCode.F, Key.F);
            keyMapping.Add(KeyCode.F1, Key.F1);
            keyMapping.Add(KeyCode.F2, Key.F2);
            keyMapping.Add(KeyCode.F3, Key.F3);
            keyMapping.Add(KeyCode.F4, Key.F4);
            keyMapping.Add(KeyCode.F5, Key.F5);
            keyMapping.Add(KeyCode.F6, Key.F6);
            keyMapping.Add(KeyCode.F7, Key.F7);
            keyMapping.Add(KeyCode.F8, Key.F8);
            keyMapping.Add(KeyCode.F9, Key.F9);
            keyMapping.Add(KeyCode.F10, Key.F10);
            keyMapping.Add(KeyCode.F11, Key.F11);
            keyMapping.Add(KeyCode.F12, Key.F12);

            keyMapping.Add(KeyCode.G, Key.G);
            keyMapping.Add(KeyCode.Greater, Key.OemPeriod);

            keyMapping.Add(KeyCode.H, Key.H);
            keyMapping.Add(KeyCode.Hash, Key.EurPound);
            keyMapping.Add(KeyCode.Home, Key.Home);

            keyMapping.Add(KeyCode.I, Key.I);
            keyMapping.Add(KeyCode.Insert, Key.Insert);

            keyMapping.Add(KeyCode.J, Key.J);

            keyMapping.Add(KeyCode.K, Key.K);
            keyMapping.Add(KeyCode.Keypad0, Key.Num0);
            keyMapping.Add(KeyCode.Keypad1, Key.Num1);
            keyMapping.Add(KeyCode.Keypad2, Key.Num2);
            keyMapping.Add(KeyCode.Keypad3, Key.Num3);
            keyMapping.Add(KeyCode.Keypad4, Key.Num4);
            keyMapping.Add(KeyCode.Keypad5, Key.Num5);
            keyMapping.Add(KeyCode.Keypad6, Key.Num6);
            keyMapping.Add(KeyCode.Keypad7, Key.Num7);
            keyMapping.Add(KeyCode.Keypad8, Key.Num8);
            keyMapping.Add(KeyCode.Keypad9, Key.Num9);
            keyMapping.Add(KeyCode.KeypadDivide, Key.NumDivide);
            keyMapping.Add(KeyCode.KeypadEnter, Key.NumEnter);
            keyMapping.Add(KeyCode.KeypadMinus, Key.NumSubtract);
            keyMapping.Add(KeyCode.KeypadMultiply, Key.NumMultiply);
            keyMapping.Add(KeyCode.KeypadPeriod, Key.NumDecimal);
            keyMapping.Add(KeyCode.KeypadPlus, Key.NumAdd);

            keyMapping.Add(KeyCode.L, Key.L);
            keyMapping.Add(KeyCode.LeftAlt, Key.LeftAlt);
            keyMapping.Add(KeyCode.LeftApple, Key.LeftAlt);
            keyMapping.Add(KeyCode.LeftArrow, Key.Left);
            keyMapping.Add(KeyCode.LeftBracket, Key.OemLeftBracket);
            //keyMapping.Add(KeyCode.LeftCommand, Key.LeftAlt); !!!! Duplicate of RightApple
            keyMapping.Add(KeyCode.LeftControl, Key.LeftControl);
            keyMapping.Add(KeyCode.LeftParen, Key.D9);
            keyMapping.Add(KeyCode.LeftShift, Key.LeftShift);
            keyMapping.Add(KeyCode.LeftWindows, Key.LeftWindows);
            keyMapping.Add(KeyCode.Less, Key.OemComma);

            keyMapping.Add(KeyCode.M, Key.M);
            keyMapping.Add(KeyCode.Menu, Key.RightMenu);
            keyMapping.Add(KeyCode.Minus, Key.OemMinus);

            keyMapping.Add(KeyCode.N, Key.N);
            keyMapping.Add(KeyCode.Numlock, Key.NumLock);

            keyMapping.Add(KeyCode.O, Key.O);

            keyMapping.Add(KeyCode.P, Key.P);
            keyMapping.Add(KeyCode.PageDown, Key.PageDown);
            keyMapping.Add(KeyCode.PageUp, Key.PageUp);
            keyMapping.Add(KeyCode.Pause, Key.Pause);
            keyMapping.Add(KeyCode.Period, Key.OemPeriod);
            keyMapping.Add(KeyCode.Plus, Key.OemEquals);
            keyMapping.Add(KeyCode.Print, Key.PrintScreen);

            keyMapping.Add(KeyCode.Q, Key.Q);
            keyMapping.Add(KeyCode.Question, Key.OemSlash);
            keyMapping.Add(KeyCode.Quote, Key.OemApostrophe);

            keyMapping.Add(KeyCode.R, Key.R);
            keyMapping.Add(KeyCode.Return, Key.Enter);
            keyMapping.Add(KeyCode.RightAlt, Key.RightAlt);
            keyMapping.Add(KeyCode.RightApple, Key.RightAlt);
            keyMapping.Add(KeyCode.RightArrow, Key.Right);
            keyMapping.Add(KeyCode.RightBracket, Key.OemRightBracket);
            //keyMapping.Add(KeyCode.RightCommand, Key.RightAlt); !!!! Duplicate of RightApple
            keyMapping.Add(KeyCode.RightControl, Key.Function);
            keyMapping.Add(KeyCode.RightParen, Key.D0);
            keyMapping.Add(KeyCode.RightShift, Key.RightShift);

            keyMapping.Add(KeyCode.S, Key.S);
            keyMapping.Add(KeyCode.ScrollLock, Key.Scroll);
            keyMapping.Add(KeyCode.Semicolon, Key.OemSemicolon);
            keyMapping.Add(KeyCode.Slash, Key.OemSlash);
            keyMapping.Add(KeyCode.Space, Key.Space);
            keyMapping.Add(KeyCode.SysReq, Key.PrintScreen);

            keyMapping.Add(KeyCode.T, Key.T);
            keyMapping.Add(KeyCode.Tab, Key.Tab);

            keyMapping.Add(KeyCode.U, Key.U);
            keyMapping.Add(KeyCode.Underscore, Key.OemMinus);
            keyMapping.Add(KeyCode.UpArrow, Key.Up);

            keyMapping.Add(KeyCode.V, Key.V);

            keyMapping.Add(KeyCode.W, Key.W);

            keyMapping.Add(KeyCode.X, Key.X);

            keyMapping.Add(KeyCode.Y, Key.Y);

            keyMapping.Add(KeyCode.Z, Key.Z);
        }
    }
}
