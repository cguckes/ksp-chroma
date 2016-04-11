using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSP_Chroma_Control.ColorSchemes
{
    class DockingScheme : GlobalKeyScheme
    {
        /// <summary>
        /// Lists all the keys we want covered in the default color. Based on the key binding list for every
        /// mode in the ksp wiki (http://wiki.kerbalspaceprogram.com/wiki/Key_bindings)
        /// </summary>
        private static string[] KeyMap =
        {
            "w", "s", "a", "d", "q", "e", "leftshift", "leftctrl", "space"
        };

        public DockingScheme() : this(Color.green)
        {

        }

        public DockingScheme(Color color)
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
