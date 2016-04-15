using System;
using System.Collections.Generic;
using UnityEngine;

namespace KSP_Chroma_Control.ColorSchemes
{
    /// <summary>
    /// Represents a base color scheme, saving all the colors per key.
    /// </summary>
    public class ColorScheme : Dictionary<string, Color>
    {
        /// <summary>
        /// Takes a dictionary and parses the RGBA-Strings from Unity's color class
        /// </summary>
        /// <param name="colors">The dictionary to transform.</param>
        /// <returns>A parsed color scheme.</returns>
        public static ColorScheme createFromDictionary(Dictionary<string, string> colors)
        {
            ColorScheme myReturn = new ColorScheme();

            foreach (KeyValuePair<string, string> entry in colors)
            {
                if (myReturn.ContainsKey(entry.Key))
                {
                    myReturn[entry.Key] = RgbaToColor(entry.Value);
                }
            }

            return myReturn;
        }

        /// <summary>
        /// Translates an RGBA text into a Unity color.
        /// </summary>
        /// <param name="rgba">The text to transform</param>
        /// <returns>The generated Unity color</returns>
        private static Color RgbaToColor(string rgba)
        {
            Color myReturn = Color.black;

            rgba = rgba.Remove(rgba.Length - 1);
            rgba = rgba.Remove(0, 5).Replace(" ", "");

            string[] split = rgba.Split(',');

            try
            {
                myReturn.r = float.Parse(split[0]);
                myReturn.g = float.Parse(split[1]);
                myReturn.b = float.Parse(split[2]);
                myReturn.a = float.Parse(split[3]);
            } catch(FormatException e)
            {
                Console.Error.WriteLine(e.Message);
            }

            return myReturn;
        }

        /// <summary>
        /// Standard keyboard map for the Razer Black Widow Chroma. Should work on
        /// most layouts though.
        /// </summary>
        private static string[] KeyMap =
        {
            /// Standard keyboard (GB Layout)
            "esc", "f1", "f2", "f3", "f4", "f5", "f6", "f7", "f8", "f9", "f10", "f11", "f12", "prtsc", "scrlk", "break",
            "`", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=", "backspace", "ins", "home", "pageup",
            "tab", "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "[", "]", "enter", "del", "end", "pagedown",
            "capslock", "a", "s", "d", "f", "g", "h", "j", "k", "l", ";", "'", "hash",
            "leftshift", "backslash", "z", "x", "c", "v", "b", "n", "m", ",", ".", "slash", "rightshift", "up",
            "leftctrl", "windows", "alt", "space", "altgr", "fn", "contextmenu", "rightctrl", "left", "down", "right",

            /// Numpad
             "numlk", "num/", "num*", "num-",
             "num7", "num8", "num9", "num+",
             "num4", "num5", "num6",
             "num1", "num2", "num3", "numenter",
             "num0", "num.",

            /// Macro keys
            "m1", "m2", "m3", "m4", "m5"
        };

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
            foreach (string key in KeyMap)
            {
                this.Add(key, color);
            }
        }

        /// <summary>
        /// JSON representation of the dictionary.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string myReturn = "{\n";

            foreach(KeyValuePair<string, Color> entry in this)
            {
                myReturn += "\"" + entry.Key + "\":\"" + entry.Value + "\",\n";
            }

            myReturn += "}\n";

            return myReturn;
        }

        /// <summary>
        /// Sets a single key to the defined color.
        /// </summary>
        /// <param name="key">The key to light up</param>
        /// <param name="color">The color it should be lit in</param>
        public void SetKeyToColor(string key, Color color)
        {
            key = key.ToLower();
            if (this.ContainsKey(key))
            {
                this[key] = color;
            }
        }

        /// <summary>
        /// Sets a number of keys to the defined color
        /// </summary>
        /// <param name="keys">An array of keys to light up</param>
        /// <param name="color">The color to use</param>
        public void SetKeysToColor(string[] keys, Color color)
        {
            foreach (string key in keys)
            {
                SetKeyToColor(key, color);
            }
        }
    }
}
