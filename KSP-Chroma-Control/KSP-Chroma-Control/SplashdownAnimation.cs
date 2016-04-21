using System;
using KSP_Chroma_Control.ColorSchemes;
using System.Collections.Generic;

namespace KSP_Chroma_Control
{
    internal class SplashdownAnimation : KeyboardAnimation
    {
        private Dictionary<int, ColorScheme> frames = new Dictionary<int, ColorScheme>();

        public SplashdownAnimation()
        {
        }

        public ColorScheme getFrame()
        {
            return new ColorScheme();
        }
    }
}