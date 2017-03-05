using System;
using KspChromaControl.ColorSchemes;
using System.Collections.Generic;
using Corale.Colore.Razer.Keyboard;
using UnityEngine;

namespace KspChromaControl
{
    /// <summary>
    /// Data drain that colors razer devices.
    /// </summary>
    internal class ColoreDrain : DataDrain
    {
        /// <summary>
        /// Three colors we use to display craft hotness.
        /// </summary>
        private static Color cold = Color.blue;
        private static Color warm = Color.red;
        private static Color hot = Color.yellow;

        /// <summary>
        /// Unity Keybinding <=> UK Layout translation dictionary
        /// </summary>
        private static readonly Dictionary<KeyCode, Key> keyMapping = new Dictionary<KeyCode, Key>()
        {
            { KeyCode.A, Key.A },
            { KeyCode.Alpha0, Key.D0 },
            { KeyCode.Alpha1, Key.D1 },
            { KeyCode.Alpha2, Key.D2 },
            { KeyCode.Alpha3, Key.D3 },
            { KeyCode.Alpha4, Key.D4 },
            { KeyCode.Alpha5, Key.D5 },
            { KeyCode.Alpha6, Key.D6 },
            { KeyCode.Alpha7, Key.D7 },
            { KeyCode.Alpha8, Key.D8 },
            { KeyCode.Alpha9, Key.D9 },
            { KeyCode.AltGr, Key.Function }, // abused to use the fn key
            //{ KeyCode.Ampersand, Key.D7 },
            //{ KeyCode.Asterisk, Key.D8 },
            // { KeyCode.At, Key.OemApostrophe }, blinking

            { KeyCode.B, Key.B },
            { KeyCode.BackQuote, Key.OemTilde },
            { KeyCode.Backslash, Key.EurBackslash },
            { KeyCode.Backspace, Key.Backspace },
            //{ KeyCode.Break, Key.Pause },

            { KeyCode.C, Key.C },
            { KeyCode.CapsLock, Key.CapsLock },
            //{ KeyCode.Caret, Key.D6 },
            //{ KeyCode.Colon, Key.OemPeriod },
            { KeyCode.Comma, Key.OemComma },

            { KeyCode.D, Key.D },
            { KeyCode.Delete, Key.Delete },
            //{ KeyCode.Dollar, Key.D4 },
            //{ KeyCode.DoubleQuote, Key.D2 },
            { KeyCode.DownArrow, Key.Down },

            { KeyCode.E, Key.E },
            { KeyCode.End, Key.End },
            { KeyCode.Equals, Key.OemEquals },
            { KeyCode.Escape, Key.Escape },
            //{ KeyCode.Exclaim, Key.D1 },

            { KeyCode.F, Key.F },
            { KeyCode.F1, Key.F1 },
            { KeyCode.F2, Key.F2 },
            { KeyCode.F3, Key.F3 },
            { KeyCode.F4, Key.F4 },
            { KeyCode.F5, Key.F5 },
            { KeyCode.F6, Key.F6 },
            { KeyCode.F7, Key.F7 },
            { KeyCode.F8, Key.F8 },
            { KeyCode.F9, Key.F9 },
            { KeyCode.F10, Key.F10 },
            { KeyCode.F11, Key.F11 },
            { KeyCode.F12, Key.F12 },

            { KeyCode.G, Key.G },
            //{ KeyCode.Greater, Key.OemPeriod },

            { KeyCode.H, Key.H },
            { KeyCode.Hash, Key.EurPound },
            { KeyCode.Home, Key.Home },

            { KeyCode.I, Key.I },
            { KeyCode.Insert, Key.Insert },

            { KeyCode.J, Key.J },

            { KeyCode.K, Key.K },
            { KeyCode.Keypad0, Key.Num0 },
            { KeyCode.Keypad1, Key.Num1 },
            { KeyCode.Keypad2, Key.Num2 },
            { KeyCode.Keypad3, Key.Num3 },
            { KeyCode.Keypad4, Key.Num4 },
            { KeyCode.Keypad5, Key.Num5 },
            { KeyCode.Keypad6, Key.Num6 },
            { KeyCode.Keypad7, Key.Num7 },
            { KeyCode.Keypad8, Key.Num8 },
            { KeyCode.Keypad9, Key.Num9 },
            { KeyCode.KeypadDivide, Key.NumDivide },
            { KeyCode.KeypadEnter, Key.NumEnter },
            { KeyCode.KeypadMinus, Key.NumSubtract },
            { KeyCode.KeypadMultiply, Key.NumMultiply },
            { KeyCode.KeypadPeriod, Key.NumDecimal },
            { KeyCode.KeypadPlus, Key.NumAdd },

            { KeyCode.L, Key.L },
            { KeyCode.LeftAlt, Key.LeftAlt },
            //{ KeyCode.LeftApple, Key.LeftAlt }, removed due to blinking issues
            { KeyCode.LeftArrow, Key.Left },
            { KeyCode.LeftBracket, Key.OemLeftBracket },
            //{ KeyCode.LeftCommand, Key.LeftAlt }, !!!! Duplicate of RightApple
            { KeyCode.LeftControl, Key.LeftControl },
            //{ KeyCode.LeftParen, Key.D9 },
            { KeyCode.LeftShift, Key.LeftShift },
            { KeyCode.LeftWindows, Key.LeftWindows },
            //{ KeyCode.Less, Key.OemComma },

            { KeyCode.M, Key.M },
            { KeyCode.Menu, Key.RightMenu },
            { KeyCode.Minus, Key.OemMinus },

            { KeyCode.N, Key.N },
            { KeyCode.Numlock, Key.NumLock },

            { KeyCode.O, Key.O },

            { KeyCode.P, Key.P },
            { KeyCode.PageDown, Key.PageDown },
            { KeyCode.PageUp, Key.PageUp },
            { KeyCode.Pause, Key.Pause },
            { KeyCode.Period, Key.OemPeriod },
            //{ KeyCode.Plus, Key.OemEquals },
            { KeyCode.Print, Key.PrintScreen },

            { KeyCode.Q, Key.Q },
            //{ KeyCode.Question, Key.OemSlash },
            { KeyCode.Quote, Key.OemApostrophe },

            { KeyCode.R, Key.R },
            { KeyCode.Return, Key.Enter },
            { KeyCode.RightAlt, Key.RightAlt },
            //{ KeyCode.RightApple, Key.RightAlt }, removed due to blinking issues
            { KeyCode.RightArrow, Key.Right },
            { KeyCode.RightBracket, Key.OemRightBracket },
            //{ KeyCode.RightCommand, Key.RightAlt }, !!!! Duplicate of RightApple
            { KeyCode.RightControl, Key.RightControl },
            //{ KeyCode.RightParen, Key.D0 },
            { KeyCode.RightShift, Key.RightShift },

            { KeyCode.S, Key.S },
            { KeyCode.ScrollLock, Key.Scroll },
            { KeyCode.Semicolon, Key.OemSemicolon },
            { KeyCode.Slash, Key.OemSlash },
            { KeyCode.Space, Key.Space },
            //{ KeyCode.SysReq, Key.PrintScreen },

            { KeyCode.T, Key.T },
            { KeyCode.Tab, Key.Tab },

            { KeyCode.U, Key.U },
            //{ KeyCode.Underscore, Key.OemMinus },
            { KeyCode.UpArrow, Key.Up },

            { KeyCode.V, Key.V },

            { KeyCode.W, Key.W },

            { KeyCode.X, Key.X },

            { KeyCode.Y, Key.Y },

            { KeyCode.Z, Key.Z }
        };

        private Corale.Colore.Razer.Mouse.Effects.CustomGrid mouseGrid = new Corale.Colore.Razer.Mouse.Effects.CustomGrid(Color.black);
        private Corale.Colore.Razer.Mousepad.Effects.Custom mousePadGrid = new Corale.Colore.Razer.Mousepad.Effects.Custom(Color.black);
        private Corale.Colore.Razer.Headset.Effects.Static headSetGrid = new Corale.Colore.Razer.Headset.Effects.Static();

        /// <summary>
        /// Applies the current color scheme to all connected razer devices.
        /// </summary>
        /// <param name="scheme"></param>
        public void send(ColorScheme scheme)
        {
            applyToKeyboard(scheme);
            displayHeat(scheme);
            displayElectricity(scheme);
            displayHeadset(scheme);

            applyGrids();
        }

        private void displayHeadset(ColorScheme scheme)
        {
            Color saveStateColor = scheme[KeyCode.F5];
            if (saveStateColor.r == 1f && saveStateColor.g == 0f && saveStateColor.b == 0f)
                headSetGrid = new Corale.Colore.Razer.Headset.Effects.Static(Color.red);
            else
                headSetGrid = new Corale.Colore.Razer.Headset.Effects.Static(Color.green);

        }

        /// <summary>
        /// Applies the color scheme to the keyboard.
        /// </summary>
        /// <param name="colorScheme">The color scheme to apply.</param>
        private void applyToKeyboard(ColorScheme colorScheme)
        {
            if (colorScheme != null)
            {
                foreach (KeyValuePair<KeyCode, Key> key in keyMapping)
                {
                    Corale.Colore.Core.Keyboard.Instance.SetKey(key.Value, colorScheme[key.Key]);
                }
            }
        }

        /// <summary>
        /// Paints heat displays onto all connected devices except keyboards and keypads.
        /// </summary>
        /// <param name="colorScheme"></param>
        private void displayHeat(ColorScheme colorScheme)
        {
            double heat = 0.0;

            if (colorScheme.otherValues.ContainsKey("Heat"))
                heat = colorScheme.otherValues["Heat"];

            // Display heat on all LEDs we have
            Color heatColor = new Color();

            if (heat >= 0.5)
            {
                heat = 2 * heat - 1.0;
                heatColor.r = warm.r + (hot.r - warm.r) * (float)heat;
                heatColor.g = warm.g + (hot.g - warm.g) * (float)heat;
                heatColor.b = warm.b + (hot.b - warm.b) * (float)heat;
                heatColor.a = 1f;
            }
            else
            {
                heat = 2 * heat;
                heatColor.r = cold.r + (warm.r - cold.r) * (float)heat;
                heatColor.g = cold.g + (warm.g - cold.g) * (float)heat;
                heatColor.b = cold.b + (warm.b - cold.b) * (float)heat;
                heatColor.a = 1f;
            }

            mouseGrid.Set(heatColor);
            mousePadGrid.Set(heatColor);
        }

        private void displayElectricity(ColorScheme colorScheme)
        {
            double electricity = 0.0;

            if(colorScheme.otherValues.ContainsKey("ElectricCharge"))
                electricity = colorScheme.otherValues["ElectricCharge"];

            // Color the outside led rows with an electricity gauge, overwriting heat displays on a mouse
            double mouseElectricity = electricity * (Corale.Colore.Razer.Mouse.Constants.MaxRows - 2);
            for(int i = 1; i < Corale.Colore.Razer.Mouse.Constants.MaxRows - 1; i++)
            {
                Color ledColor = Color.cyan;

                float colorStrength = (float)Math.Min(mouseElectricity - (i - 1), 1.0);

                ledColor.r *= colorStrength;
                ledColor.g *= colorStrength;
                ledColor.b *= colorStrength;

                mouseGrid[Corale.Colore.Razer.Mouse.Constants.MaxRows - 1 - i, 0] = ledColor;
                mouseGrid[Corale.Colore.Razer.Mouse.Constants.MaxRows - 1 - i, Corale.Colore.Razer.Mouse.Constants.MaxColumns - 1] = ledColor;
            }

            // Color the outside led rows with an electricity gauge, overwriting heat displays on a mousepad
            double padElectricity = electricity * 5.0;
            for (int i = 0; i < 5; i++)
            {
                Color ledColor = Color.cyan;

                float colorStrength = (float)Math.Min(padElectricity - i, 1.0);

                ledColor.r *= colorStrength;
                ledColor.g *= colorStrength;
                ledColor.b *= colorStrength;

                mousePadGrid[4 - i] = ledColor;
                mousePadGrid[10 + i] = ledColor;
            }
        }

        private void applyGrids()
        {
            Corale.Colore.Core.Mouse.Instance.SetGrid(mouseGrid);
            Corale.Colore.Core.Mousepad.Instance.SetCustom(mousePadGrid);
            Corale.Colore.Core.Headset.Instance.SetStatic(headSetGrid);
        }
    }
}