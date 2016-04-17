using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSP_Chroma_Control
{
    interface DataDrain
    {
        /// <summary>
        /// Sends the requested color scheme to the implemented output channel
        /// </summary>
        /// <param name="scheme">The color scheme to apply to the implemented output device</param>
        void send(ColorSchemes.ColorScheme scheme);
    }
}
