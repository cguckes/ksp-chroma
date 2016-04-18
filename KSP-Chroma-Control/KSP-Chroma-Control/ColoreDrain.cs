using System;
using KSP_Chroma_Control.ColorSchemes;
using System.Collections.Generic;
using Corale.Colore.Razer.Keyboard;
using UnityEngine;

namespace KSP_Chroma_Control
{
    internal class ColoreDrain : DataDrain
    {
        public void send(ColorScheme scheme)
        {
            applyToKeyboard(scheme);
        }

        /// <summary>
        /// Applies the color scheme to the keyboard.
        /// </summary>
        /// <param name="colorScheme">The color scheme to apply.</param>
        private void applyToKeyboard(ColorScheme colorScheme)
        {
            foreach (KeyValuePair<Key, Color> entry in colorScheme)
            {
                Corale.Colore.Core.Keyboard.Instance.SetKey(entry.Key, entry.Value);
            }
        }
    }
}