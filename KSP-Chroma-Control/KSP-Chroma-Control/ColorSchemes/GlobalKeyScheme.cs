using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSP_Chroma_Control.ColorSchemes
{
    class GlobalKeyScheme : ColorScheme
    {
        /// <summary>
        /// Lists all the keys we want covered in the default color. Based on the key binding list for every
        /// mode in the ksp wiki (http://wiki.kerbalspaceprogram.com/wiki/Key_bindings)
        /// </summary>
        private static string[] KeyMap =
            {
            "left", "up", "down", "right", "num+", "num-", "m", "tab", "backspace", "ins", "del", ".", ",",
            "num.", "]", "[", "f1", "f2", "f3", "f4", "f5", "f9", "f10", "f11", "f12"
        };

        public GlobalKeyScheme() : this(Color.green)
        {

        }

        public GlobalKeyScheme(Color color)
        {
            foreach (string key in KeyMap)
            {
                if (this.ContainsKey(key))
                {
                    this[key] = color;
                }
            }
        }
    }
}
