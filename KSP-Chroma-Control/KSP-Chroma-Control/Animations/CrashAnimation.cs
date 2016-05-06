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
    internal class CrashAnimation : KeyboardAnimation
    {
        private static ColorScheme[] frames;

        private static List<GameScenes> validScenes = new List<GameScenes>() {
            GameScenes.FLIGHT
        };

        /// <summary>
        /// Static constructor adds lightning bolts in different colors to both frames
        /// </summary>
        static CrashAnimation()
        {
            List<ColorScheme> newFrames = new List<ColorScheme>();

            ColorScheme red = new ColorScheme(Color.red);
            ColorScheme yellow = new ColorScheme(Color.yellow);

            // Generate first few frames
            newFrames.AddRange(AnimationUtils.InterpolateFrames(red, yellow, 3));
            // Add the way back to the original
            newFrames.AddRange(newFrames.ToArray().Reverse());
            // Double it
            newFrames.AddRange(newFrames.ToArray());
            // Quadruple it
            newFrames.AddRange(newFrames.ToArray());
            // Octuple it
            newFrames.AddRange(newFrames.ToArray());

            for (int i = 0; i < 30; i++)
            {
                newFrames.Add(AnimationUtils.CircularSineWave(Color.red, Color.yellow, i));
            }

            newFrames.AddRange(AnimationUtils.InterpolateFrames(newFrames.Last(), new ColorScheme(Color.black), 10));

            frames = newFrames.ToArray();
        }
        
        public CrashAnimation() : base(30, validScenes, frames)
        {
        }
    }
}