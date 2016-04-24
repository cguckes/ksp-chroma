using System;
using KSP_Chroma_Control.ColorSchemes;
using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using KSP_Chroma_Control.Animations;

namespace KSP_Chroma_Control
{
    /// <summary>
    /// Displays a warning on the keyboard, indicating that the vessel is currently out of power and cannot
    /// be controlled. Consists of two frames alternating at 1fps.
    /// </summary>
    internal class LogoAnimation : KeyboardAnimation
    {
        /// <summary>
        /// Static constructor adds lightning bolts in different colors to both frames
        /// </summary>
        static LogoAnimation()
        {
            frames = AnimationUtils.InterpolateFrames(new ColorScheme(Color.blue), new LogoScheme(), 20);
        }

        public LogoAnimation() : base(10)
        {
        }
    }
}