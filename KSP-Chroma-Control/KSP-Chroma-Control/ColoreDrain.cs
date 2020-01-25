// ReSharper disable CompareOfFloatsByEqualityOperator

namespace KspChromaControl
{
    using System;
    using System.Collections.Generic;
    using Corale.Colore.Core;
    using Corale.Colore.Razer.Keyboard;
    using Corale.Colore.Razer.Mouse.Effects;
    using KspChromaControl.ColorSchemes;
    using UnityEngine;
    using Color = Corale.Colore.Core.Color;
    using Constants = Corale.Colore.Razer.Mouse.Constants;
    using Custom = Corale.Colore.Razer.Mousepad.Effects.Custom;
    using Static = Corale.Colore.Razer.Headset.Effects.Static;

    /// <summary>
    ///     Data drain that colors razer devices.
    /// </summary>
    internal class ColoreDrain : IDataDrain
    {
        /// <summary>
        ///     Three colors we use to display craft hotness.
        /// </summary>
        private static Color cold = Color.Blue;

        private static Color warm = Color.Red;
        private static Color hot = Color.Yellow;

        /// <summary>
        ///     Unity Keybinding - UK Layout translation dictionary
        /// </summary>
        private static readonly Dictionary<KeyCode, Key> keyMapping = new Dictionary<KeyCode, Key>
        {
            {KeyCode.A, Key.A},
            {KeyCode.Alpha0, Key.D0},
            {KeyCode.Alpha1, Key.D1},
            {KeyCode.Alpha2, Key.D2},
            {KeyCode.Alpha3, Key.D3},
            {KeyCode.Alpha4, Key.D4},
            {KeyCode.Alpha5, Key.D5},
            {KeyCode.Alpha6, Key.D6},
            {KeyCode.Alpha7, Key.D7},
            {KeyCode.Alpha8, Key.D8},
            {KeyCode.Alpha9, Key.D9},
            {KeyCode.AltGr, Key.Function}, // abused to use the fn key
            //{ KeyCode.Ampersand, Key.D7 },
            //{ KeyCode.Asterisk, Key.D8 },
            // { KeyCode.At, Key.OemApostrophe }, blinking

            {KeyCode.B, Key.B},
            {KeyCode.BackQuote, Key.OemTilde},
            {KeyCode.Backslash, Key.EurBackslash},
            {KeyCode.Backspace, Key.Backspace},
            //{ KeyCode.Break, Key.Pause },

            {KeyCode.C, Key.C},
            {KeyCode.CapsLock, Key.CapsLock},
            //{ KeyCode.Caret, Key.D6 },
            //{ KeyCode.Colon, Key.OemPeriod },
            {KeyCode.Comma, Key.OemComma},

            {KeyCode.D, Key.D},
            {KeyCode.Delete, Key.Delete},
            //{ KeyCode.Dollar, Key.D4 },
            //{ KeyCode.DoubleQuote, Key.D2 },
            {KeyCode.DownArrow, Key.Down},

            {KeyCode.E, Key.E},
            {KeyCode.End, Key.End},
            {KeyCode.Equals, Key.OemEquals},
            {KeyCode.Escape, Key.Escape},
            //{ KeyCode.Exclaim, Key.D1 },

            {KeyCode.F, Key.F},
            {KeyCode.F1, Key.F1},
            {KeyCode.F2, Key.F2},
            {KeyCode.F3, Key.F3},
            {KeyCode.F4, Key.F4},
            {KeyCode.F5, Key.F5},
            {KeyCode.F6, Key.F6},
            {KeyCode.F7, Key.F7},
            {KeyCode.F8, Key.F8},
            {KeyCode.F9, Key.F9},
            {KeyCode.F10, Key.F10},
            {KeyCode.F11, Key.F11},
            {KeyCode.F12, Key.F12},

            {KeyCode.G, Key.G},
            //{ KeyCode.Greater, Key.OemPeriod },

            {KeyCode.H, Key.H},
            {KeyCode.Hash, Key.EurPound},
            {KeyCode.Home, Key.Home},

            {KeyCode.I, Key.I},
            {KeyCode.Insert, Key.Insert},

            {KeyCode.J, Key.J},

            {KeyCode.K, Key.K},
            {KeyCode.Keypad0, Key.Num0},
            {KeyCode.Keypad1, Key.Num1},
            {KeyCode.Keypad2, Key.Num2},
            {KeyCode.Keypad3, Key.Num3},
            {KeyCode.Keypad4, Key.Num4},
            {KeyCode.Keypad5, Key.Num5},
            {KeyCode.Keypad6, Key.Num6},
            {KeyCode.Keypad7, Key.Num7},
            {KeyCode.Keypad8, Key.Num8},
            {KeyCode.Keypad9, Key.Num9},
            {KeyCode.KeypadDivide, Key.NumDivide},
            {KeyCode.KeypadEnter, Key.NumEnter},
            {KeyCode.KeypadMinus, Key.NumSubtract},
            {KeyCode.KeypadMultiply, Key.NumMultiply},
            {KeyCode.KeypadPeriod, Key.NumDecimal},
            {KeyCode.KeypadPlus, Key.NumAdd},

            {KeyCode.L, Key.L},
            {KeyCode.LeftAlt, Key.LeftAlt},
            //{ KeyCode.LeftApple, Key.LeftAlt }, removed due to blinking issues
            {KeyCode.LeftArrow, Key.Left},
            {KeyCode.LeftBracket, Key.OemLeftBracket},
            //{ KeyCode.LeftCommand, Key.LeftAlt }, !!!! Duplicate of RightApple
            {KeyCode.LeftControl, Key.LeftControl},
            //{ KeyCode.LeftParen, Key.D9 },
            {KeyCode.LeftShift, Key.LeftShift},
            {KeyCode.LeftWindows, Key.LeftWindows},
            //{ KeyCode.Less, Key.OemComma },

            {KeyCode.M, Key.M},
            {KeyCode.Menu, Key.RightMenu},
            {KeyCode.Minus, Key.OemMinus},

            {KeyCode.N, Key.N},
            {KeyCode.Numlock, Key.NumLock},

            {KeyCode.O, Key.O},

            {KeyCode.P, Key.P},
            {KeyCode.PageDown, Key.PageDown},
            {KeyCode.PageUp, Key.PageUp},
            {KeyCode.Pause, Key.Pause},
            {KeyCode.Period, Key.OemPeriod},
            //{ KeyCode.Plus, Key.OemEquals },
            {KeyCode.Print, Key.PrintScreen},

            {KeyCode.Q, Key.Q},
            //{ KeyCode.Question, Key.OemSlash },
            {KeyCode.Quote, Key.OemApostrophe},

            {KeyCode.R, Key.R},
            {KeyCode.Return, Key.Enter},
            {KeyCode.RightAlt, Key.RightAlt},
            //{ KeyCode.RightApple, Key.RightAlt }, removed due to blinking issues
            {KeyCode.RightArrow, Key.Right},
            {KeyCode.RightBracket, Key.OemRightBracket},
            //{ KeyCode.RightCommand, Key.RightAlt }, !!!! Duplicate of RightApple
            {KeyCode.RightControl, Key.RightControl},
            //{ KeyCode.RightParen, Key.D0 },
            {KeyCode.RightShift, Key.RightShift},

            {KeyCode.S, Key.S},
            {KeyCode.ScrollLock, Key.Scroll},
            {KeyCode.Semicolon, Key.OemSemicolon},
            {KeyCode.Slash, Key.OemSlash},
            {KeyCode.Space, Key.Space},
            //{ KeyCode.SysReq, Key.PrintScreen },

            {KeyCode.T, Key.T},
            {KeyCode.Tab, Key.Tab},

            {KeyCode.U, Key.U},
            //{ KeyCode.Underscore, Key.OemMinus },
            {KeyCode.UpArrow, Key.Up},

            {KeyCode.V, Key.V},

            {KeyCode.W, Key.W},

            {KeyCode.X, Key.X},

            {KeyCode.Y, Key.Y},

            {KeyCode.Z, Key.Z}
        };

        private Static headSetGrid;

        private CustomGrid mouseGrid = new CustomGrid(Color.Black);
        private Custom mousePadGrid = new Custom(Color.Black);

        /// <summary>
        ///     Applies the current color scheme to all connected razer devices.
        /// </summary>
        /// <param name="scheme"></param>
        public void Send(ColorScheme scheme)
        {
            ApplyToKeyboard(scheme);
            this.DisplayHeat(scheme);
            this.DisplayElectricity(scheme);
            this.DisplayHeadset(scheme);

            this.ApplyGrids();
        }

        private void DisplayHeadset(ColorScheme scheme)
        {
            var saveStateColor = new Color(scheme[KeyCode.F5].r, scheme[KeyCode.F5].g, scheme[KeyCode.F5].b);

            if (saveStateColor.R == 1f && saveStateColor.G == 0f && saveStateColor.B == 0f)
            {
                this.headSetGrid = new Static(Color.Red);
            }
            else
            {
                this.headSetGrid = new Static(Color.Green);
            }
        }

        /// <summary>
        ///     Applies the color scheme to the keyboard.
        /// </summary>
        /// <param name="colorScheme">The color scheme to apply.</param>
        private static void ApplyToKeyboard(ColorScheme colorScheme)
        {
            if (colorScheme != null)
            {
                foreach (var key in keyMapping)
                {
                    var keyColor = new Color(colorScheme[key.Key].r, colorScheme[key.Key].g, colorScheme[key.Key].b);
                    Keyboard.Instance.SetKey(key.Value, keyColor);
                }
            }
        }

        /// <summary>
        ///     Paints heat displays onto all connected devices except keyboards and keypads.
        /// </summary>
        /// <param name="colorScheme"></param>
        private void DisplayHeat(ColorScheme colorScheme)
        {
            var heat = 0.0;

            if (colorScheme.OtherValues.ContainsKey("Heat"))
            {
                heat = colorScheme.OtherValues["Heat"];
            }


            float red, green, blue;

            if (heat >= 0.5)
            {
                heat = 2 * heat - 1.0;
                red = warm.R + (hot.R - warm.R) * (float) heat;
                green = warm.G + (hot.G - warm.G) * (float) heat;
                blue = warm.B + (hot.B - warm.B) * (float) heat;
            }
            else
            {
                heat = 2 * heat;
                red = cold.R + (warm.R - cold.R) * (float) heat;
                green = cold.G + (warm.G - cold.G) * (float) heat;
                blue = cold.B + (warm.B - cold.B) * (float) heat;
            }

            // Display heat on all LEDs we have
            var heatColor = new Color(red, green, blue);
            this.mouseGrid.Set(heatColor);
            this.mousePadGrid.Set(heatColor);
        }

        private void DisplayElectricity(ColorScheme colorScheme)
        {
            var electricity = 0.0;

            if (colorScheme.OtherValues.ContainsKey("ElectricCharge"))
            {
                electricity = colorScheme.OtherValues["ElectricCharge"];
            }

            // Color the outside led rows with an electricity gauge, overwriting heat displays on a mouse
            var mouseElectricity = electricity * (Constants.MaxRows - 2);

            for (var i = 1; i < Constants.MaxRows - 1; i++)
            {
                var colorStrength = (float) Math.Min(mouseElectricity - (i - 1), 1.0);

                // Cyan has a standard code of 0,1,1; so passing in the raw strength has the same effect.
                var padLedColor = new Color(0.0, colorStrength, colorStrength);
                this.mouseGrid[Constants.MaxRows - 1 - i, 0] = padLedColor;
                this.mouseGrid[Constants.MaxRows - 1 - i, Constants.MaxColumns - 1] = padLedColor;
            }

            // Color the outside led rows with an electricity gauge, overwriting heat displays on a mousepad
            var padElectricity = electricity * 5.0;

            for (var i = 0; i < 5; i++)
            {
                var colorStrength = (float) Math.Min(padElectricity - i, 1.0);

                // Cyan has a standard code of 0,1,1; so passing in the raw strength has the same effect.
                var gripLedColor = new Color(0.0, colorStrength, colorStrength);

                this.mousePadGrid[4 - i] = gripLedColor;
                this.mousePadGrid[10 + i] = gripLedColor;
            }
        }

        private void ApplyGrids()
        {
            Mouse.Instance.SetGrid(this.mouseGrid);
            Mousepad.Instance.SetCustom(this.mousePadGrid);
            Headset.Instance.SetStatic(this.headSetGrid);
        }
    }
}