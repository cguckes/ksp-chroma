namespace KspChromaControl.ColorSchemes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using UnityEngine;

    /// <summary>
    ///     Represents a base color scheme, saving a base color and colors for modified keys. Implements the
    ///     flyweight design pattern, to keep the memory footprint low. This is necessary for the animations to
    ///     work smoothly. Also allows the storage of values for other gauge display devices, should range from 0 to 1.
    /// </summary>
    [Serializable]
    internal class ColorScheme : Dictionary<KeyCode, Color>
    {
        /// <summary>
        ///     Creates a new ColorScheme rendering all keys black;
        /// </summary>
        public ColorScheme() : this(Color.black)
        {
        }

        /// <summary>
        ///     Creates a new ColorScheme rendering all keys in the defined color.
        /// </summary>
        /// <param name="color">The color to use</param>
        public ColorScheme(Color color)
        {
            this.BaseColor = color;
        }

        public Color BaseColor { get; }
        public Dictionary<string, double> OtherValues { get; } = new Dictionary<string, double>();

        public new Color this[KeyCode key]
        {
            get
            {
                Color myReturn;

                if (this.ContainsKey(key))
                {
                    myReturn = base[key];
                }
                else
                {
                    myReturn = this.BaseColor;
                }

                return myReturn;
            }
            set { base[key] = value; }
        }

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public void SetKeyToColor(KeyCode key, Color color)
        {
            if (color.r == this.BaseColor.r &&
                color.g == this.BaseColor.g &&
                color.b == this.BaseColor.b &&
                this.ContainsKey(key))
            {
                this.Remove(key);
            }
            else
            {
                if (this.ContainsKey(key))
                {
                    this[key] = color;
                }
                else
                {
                    this.Add(key, color);
                }
            }
        }

        public void SetKeyToColor(int x, int y, Color color)
            => this.SetKeyToColor(Config.Instance.KeyByPosition[y, x], color);

        /// <summary>
        ///     Sets a number of keys to the defined color
        /// </summary>
        /// <param name="keys">An array of keys to light up</param>
        /// <param name="color">The color to use</param>
        public void SetKeysToColor(KeyCode[] keys, Color color)
        {
            foreach (var key in keys)
            {
                this.SetKeyToColor(key, color);
            }
        }
    }
}