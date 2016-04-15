using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSP_Chroma_Control.ColorSchemes
{
    class FlightScheme : ColorScheme
    {
        public FlightScheme()
        {
            string[] whitekeys = { "w", "a", "s", "d", "q", "e", "space" };
            SetKeysToColor(whitekeys, Color.white);

            string[] yellowkeys = { "h", "n", "j", "k", "l", "i" };
            SetKeysToColor(yellowkeys, Color.yellow);

            string[] redkeys = { "leftshift", "leftctrl", "z", "x"};
            SetKeysToColor(redkeys, Color.red);

            string[] greenkeys = { "r", "t", "f", "b" };
            SetKeysToColor(greenkeys, Color.green);

            string[] bluekeys = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            SetKeysToColor(bluekeys, Color.blue);
        }
    }
}
