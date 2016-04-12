using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSP_Chroma_Control.SceneManagers
{
    /// <summary>
    /// Allows creation of keyboard color managers for multiple scenes.
    /// </summary>
    interface SceneManager
    {
        /// <summary>
        /// Gets the keyboard color scheme for the current frame.
        /// </summary>
        /// <returns>The new color scheme.</returns>
        ColorSchemes.ColorScheme getScheme();
    }
}
