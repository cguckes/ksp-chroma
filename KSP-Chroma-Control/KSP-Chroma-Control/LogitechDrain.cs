using System;
using System.Collections.Generic;
using KSP_Chroma_Control.ColorSchemes;
using LedCSharp;
using UnityEngine;

namespace KSP_Chroma_Control
{
    class LogitechDrain : DataDrain
    {
        /// <summary>
        /// Unity Keybinding <=> Logitech DE Layout translation dictionary
        /// </summary>
        private static readonly Dictionary<KeyCode, keyboardNames> keyMapping = new Dictionary<KeyCode, keyboardNames>()
        {
            { KeyCode.A, keyboardNames.A },
            { KeyCode.Alpha0, keyboardNames.ZERO },
            { KeyCode.Alpha1, keyboardNames.ONE },
            { KeyCode.Alpha2, keyboardNames.TWO },
            { KeyCode.Alpha3, keyboardNames.THREE },
            { KeyCode.Alpha4, keyboardNames.FOUR },
            { KeyCode.Alpha5, keyboardNames.FIVE },
            { KeyCode.Alpha6, keyboardNames.SIX },
            { KeyCode.Alpha7, keyboardNames.SEVEN },
            { KeyCode.Alpha8, keyboardNames.EIGHT },
            { KeyCode.Alpha9, keyboardNames.NINE },
            //{ KeyCode.AltGr, keyboardNames.Function }, // abused to use the fn key
            //{ KeyCode.Ampersand, keyboardNames.D7 },
            //{ KeyCode.Asterisk, keyboardNames.D8 },
            // { KeyCode.At, keyboardNames.OemApostrophe }, blinking

            { KeyCode.B, keyboardNames.B },
            { KeyCode.BackQuote, keyboardNames.TILDE },
            { KeyCode.Backslash, keyboardNames.BACKSLASH },
            { KeyCode.Backspace, keyboardNames.BACKSPACE },
            //{ KeyCode.Break, keyboardNames.PAUSE_BREAK },

            { KeyCode.C, keyboardNames.C },
            { KeyCode.CapsLock, keyboardNames.CAPS_LOCK},
            //{ KeyCode.Caret, keyboardNames.D6 },
            //{ KeyCode.Colon, keyboardNames.OemPeriod },
            { KeyCode.Comma, keyboardNames.COMMA},

            { KeyCode.D, keyboardNames.D },
            { KeyCode.Delete, keyboardNames.KEYBOARD_DELETE },
            //{ KeyCode.Dollar, keyboardNames.D4 },
            //{ KeyCode.DoubleQuote, keyboardNames.D2 },
            { KeyCode.DownArrow, keyboardNames.ARROW_DOWN },

            { KeyCode.E, keyboardNames.E },
            { KeyCode.End, keyboardNames.END },
            { KeyCode.Equals, keyboardNames.EQUALS },
            { KeyCode.Escape, keyboardNames.ESC },
            //{ KeyCode.Exclaim, keyboardNames.D1 },

            { KeyCode.F, keyboardNames.F },
            { KeyCode.F1, keyboardNames.F1 },
            { KeyCode.F2, keyboardNames.F2 },
            { KeyCode.F3, keyboardNames.F3 },
            { KeyCode.F4, keyboardNames.F4 },
            { KeyCode.F5, keyboardNames.F5 },
            { KeyCode.F6, keyboardNames.F6 },
            { KeyCode.F7, keyboardNames.F7 },
            { KeyCode.F8, keyboardNames.F8 },
            { KeyCode.F9, keyboardNames.F9 },
            { KeyCode.F10, keyboardNames.F10 },
            { KeyCode.F11, keyboardNames.F11 },
            { KeyCode.F12, keyboardNames.F12 },

            { KeyCode.G, keyboardNames.G },
            //{ KeyCode.Greater, keyboardNames.OemPeriod },

            { KeyCode.H, keyboardNames.H },
            //{ KeyCode.Hash, keyboardNames.EurPound },
            { KeyCode.Home, keyboardNames.HOME },

            { KeyCode.I, keyboardNames.I },
            { KeyCode.Insert, keyboardNames.INSERT },

            { KeyCode.J, keyboardNames.J },

            { KeyCode.K, keyboardNames.K },
            { KeyCode.Keypad0, keyboardNames.NUM_ZERO },
            { KeyCode.Keypad1, keyboardNames.NUM_ONE },
            { KeyCode.Keypad2, keyboardNames.NUM_TWO },
            { KeyCode.Keypad3, keyboardNames.NUM_THREE },
            { KeyCode.Keypad4, keyboardNames.NUM_FOUR },
            { KeyCode.Keypad5, keyboardNames.NUM_FIVE },
            { KeyCode.Keypad6, keyboardNames.NUM_SIX },
            { KeyCode.Keypad7, keyboardNames.NUM_SEVEN },
            { KeyCode.Keypad8, keyboardNames.NUM_EIGHT },
            { KeyCode.Keypad9, keyboardNames.NUM_NINE },
            { KeyCode.KeypadDivide, keyboardNames.NUM_SLASH },
            { KeyCode.KeypadEnter, keyboardNames.NUM_ENTER },
            { KeyCode.KeypadMinus, keyboardNames.NUM_MINUS },
            { KeyCode.KeypadMultiply, keyboardNames.NUM_ASTERISK },
            { KeyCode.KeypadPeriod, keyboardNames.NUM_PERIOD },
            { KeyCode.KeypadPlus, keyboardNames.NUM_PLUS },

            { KeyCode.L, keyboardNames.L },
            { KeyCode.LeftAlt, keyboardNames.LEFT_ALT },
            //{ KeyCode.LeftApple, keyboardNames.LeftAlt }, removed due to blinking issues
            { KeyCode.LeftArrow, keyboardNames.ARROW_LEFT },
            { KeyCode.LeftBracket, keyboardNames.OPEN_BRACKET },
            //{ KeyCode.LeftCommand, keyboardNames.LeftAlt }, !!!! Duplicate of RightApple
            { KeyCode.LeftControl, keyboardNames.LEFT_CONTROL },
            //{ KeyCode.LeftParen, keyboardNames.D9 },
            { KeyCode.LeftShift, keyboardNames.LEFT_SHIFT },
            { KeyCode.LeftWindows, keyboardNames.LEFT_WINDOWS },
            //{ KeyCode.Less, keyboardNames.OemComma },

            { KeyCode.M, keyboardNames.M },
            { KeyCode.Menu, keyboardNames.APPLICATION_SELECT },
            { KeyCode.Minus, keyboardNames.MINUS },

            { KeyCode.N, keyboardNames.N },
            { KeyCode.Numlock, keyboardNames.NUM_LOCK },

            { KeyCode.O, keyboardNames.O },

            { KeyCode.P, keyboardNames.P },
            { KeyCode.PageDown, keyboardNames.PAGE_DOWN },
            { KeyCode.PageUp, keyboardNames.PAGE_UP },
            { KeyCode.Pause, keyboardNames.PAUSE_BREAK },
            { KeyCode.Period, keyboardNames.PERIOD },
            //{ KeyCode.Plus, keyboardNames.EQUALS },
            { KeyCode.Print, keyboardNames.PRINT_SCREEN },

            { KeyCode.Q, keyboardNames.Q },
            //{ KeyCode.Question, keyboardNames.OemSlash },
            { KeyCode.Quote, keyboardNames.APOSTROPHE },

            { KeyCode.R, keyboardNames.R },
            { KeyCode.Return, keyboardNames.ENTER },
            { KeyCode.RightAlt, keyboardNames.RIGHT_ALT },
            //{ KeyCode.RightApple, keyboardNames.RightAlt }, removed due to blinking issues
            { KeyCode.RightArrow, keyboardNames.ARROW_RIGHT },
            { KeyCode.RightBracket, keyboardNames.CLOSE_BRACKET },
            //{ KeyCode.RightCommand, keyboardNames.RightAlt }, !!!! Duplicate of RightApple
            { KeyCode.RightControl, keyboardNames.RIGHT_CONTROL },
            //{ KeyCode.RightParen, keyboardNames.D0 },
            { KeyCode.RightShift, keyboardNames.RIGHT_SHIFT },
            { KeyCode.RightWindows, keyboardNames.RIGHT_WINDOWS },

            { KeyCode.S, keyboardNames.S },
            { KeyCode.ScrollLock, keyboardNames.SCROLL_LOCK },
            { KeyCode.Semicolon, keyboardNames.SEMICOLON },
            { KeyCode.Slash, keyboardNames.FORWARD_SLASH },
            { KeyCode.Space, keyboardNames.SPACE },
            //{ KeyCode.SysReq, keyboardNames.PRINT_SCREEN },

            { KeyCode.T, keyboardNames.T },
            { KeyCode.Tab, keyboardNames.TAB },

            { KeyCode.U, keyboardNames.U },
            //{ KeyCode.Underscore, keyboardNames.OemMinus },
            { KeyCode.UpArrow, keyboardNames.ARROW_UP },

            { KeyCode.V, keyboardNames.V },

            { KeyCode.W, keyboardNames.W },

            { KeyCode.X, keyboardNames.X },

            { KeyCode.Y, keyboardNames.Y },

            { KeyCode.Z, keyboardNames.Z }
        };

        public LogitechDrain()
        {
            LogitechGSDK.LogiLedInit();
        }

        ~LogitechDrain()
        {
            // Not strictly necessary, frees up memory, but causes keys blinking on scene switches
            LogitechGSDK.LogiLedShutdown();
        }

        public void send(ColorScheme scheme)
        {
            applyToKeyboard(scheme);
        }

        /// <summary>
        ///     Applies the color scheme to the keyboard.
        /// </summary>
        /// <param name="colorScheme">The color scheme to apply.</param>
        private void applyToKeyboard(ColorScheme colorScheme)
        {
            foreach (KeyValuePair<KeyCode, Color> entry in colorScheme)
            {
                if (keyMapping.ContainsKey(entry.Key))
                {
                    LogitechGSDK.LogiLedSetLightingForKeyWithScanCode(
                        Convert.ToInt32(keyMapping[entry.Key]),
                        Convert.ToInt32(entry.Value.r*100),
                        Convert.ToInt32(entry.Value.g*100),
                        Convert.ToInt32(entry.Value.b*100)
                        );
                }
            }
        }
    }
}
