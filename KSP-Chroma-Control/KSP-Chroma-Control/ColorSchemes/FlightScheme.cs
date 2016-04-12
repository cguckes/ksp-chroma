using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSP_Chroma_Control.ColorSchemes
{
    class FlightScheme : ColorScheme
    {
        /// <summary>
        /// Lists all the keys we want covered in the default color. Based on the key binding list for every
        /// mode in the ksp wiki (http://wiki.kerbalspaceprogram.com/wiki/Key_bindings)
        /// Discontinued this idea because it looks ridiculous and helps noone.
        /// </summary>
        private static string[] KeyMap =
        {
            "w", "s", "a", "d", "q", "e", "c", "v", "leftshift", "leftctrl", "space", "t", "f", "capslock",
            "r", "h", "n", "i", "j", "k", "l", "x", "z", "x", "z", "g", "b", "u", "1", "2", "3", "4", "5",
            "6", "7", "8", "9", "0", "backspace", "home", "end"
        };

        public FlightScheme()
        {
            string[] whitekeys = { "w", "a", "s", "d", "q", "e", "space" };
            SetKeysToColor(whitekeys, Color.white);

            string[] yellowkeys = { "h", "n", "j", "k", "l", "i" };
            SetKeysToColor(yellowkeys, Color.yellow);

            string[] redkeys = { "leftshift", "leftctrl", "z", "x"};
            SetKeysToColor(redkeys, Color.red);

            string[] greenkeys = { "r", "t", "f"};
            SetKeysToColor(greenkeys, Color.green);

            string[] bluekeys = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            SetKeysToColor(bluekeys, Color.blue);
        }
    }
}
