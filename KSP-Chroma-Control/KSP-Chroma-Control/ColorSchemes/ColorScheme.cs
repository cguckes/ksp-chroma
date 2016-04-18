using Corale.Colore.Razer.Keyboard;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KSP_Chroma_Control.ColorSchemes
{
    /// <summary>
    /// Represents a base color scheme, saving all the colors per key.
    /// </summary>
    public class ColorScheme : Dictionary<Key, Color>
    {
        /// <summary>
        /// Creates a new ColorScheme rendering all keys black;
        /// </summary>
        public ColorScheme() : this(Color.black) {
        }

        /// <summary>
        /// Creates a new ColorScheme rendering all keys in the defined color.
        /// </summary>
        /// <param name="color">The color to use</param>
        public ColorScheme(Color color)
        {
            foreach (Key key in Enum.GetValues(typeof(Key)))
            {
                if (!this.ContainsKey(key))
                    this.Add(key, color);
                else
                    this[key] = color;
            }

            this.Remove(Key.Invalid);
        }


        public void SetKeyToColor(KeyCode key, Color color)
        {
            if (Config.Instance.keyMapping.ContainsKey(key))
                this[Config.Instance.keyMapping[key]] = color;
        }

        /// <summary>
        /// Sets a number of keys to the defined color
        /// </summary>
        /// <param name="keys">An array of keys to light up</param>
        /// <param name="color">The color to use</param>
        public void SetKeysToColor(KeyCode[] keys, Color color)
        {
            foreach (KeyCode key in keys)
            {
                SetKeyToColor(key, color);
            }
        }
    }
}
