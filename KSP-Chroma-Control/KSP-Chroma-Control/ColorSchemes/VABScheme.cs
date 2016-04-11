using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSP_Chroma_Control.ColorSchemes
{
    class VABScheme : GlobalKeyScheme
    {
        /// <summary>
        /// Lists all the keys we want covered in the default color. Based on the key binding list for every
        /// mode in the ksp wiki (http://wiki.kerbalspaceprogram.com/wiki/Key_bindings)
        /// </summary>
        private static string[] KeyMap =
        {
            "alt", "c", "x", "pageup", "pagedown", "w", "a", "s", "d", "e", "q", "leftshift", "space", "1",
            "2", "3", "4", "f", "r"
        };

        public VABScheme() : this(Color.green)
        {

        }

        public VABScheme(Color color)
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
