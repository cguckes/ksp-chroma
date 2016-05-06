using System;
using KspChromaControl.ColorSchemes;
using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using KspChromaControl.Animations;

namespace KspChromaControl
{
    /// <summary>
    /// Displays a warning on the keyboard, indicating that the vessel is currently out of power and cannot
    /// be controlled. Consists of two frames alternating at 1fps.
    /// </summary>
    internal class LogoAnimation : KeyboardAnimation
    {
        private static List<GameScenes> validScenes = new List<GameScenes>() {
            GameScenes.MAINMENU,
            GameScenes.SPACECENTER,
            GameScenes.TRACKSTATION,
            GameScenes.CREDITS
        };

        /// <summary>
        /// Static constructor interpolates from blue to logo.
        /// </summary>
        static LogoAnimation()
        {
            frames = AnimationUtils.InterpolateFrames(new ColorScheme(Color.blue), new LogoScheme(), 20);
        }

        public LogoAnimation() : base(10, validScenes)
        {
        }
    }
}