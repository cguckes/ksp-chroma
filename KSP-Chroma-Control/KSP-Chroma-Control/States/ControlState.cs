using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSP_Chroma_Control
{
    interface ControlState
    {
        ColorSchemes.ColorScheme getKeyboardColors();
    }
}
