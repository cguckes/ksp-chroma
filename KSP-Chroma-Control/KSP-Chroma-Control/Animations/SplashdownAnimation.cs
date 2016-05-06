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
    internal class SplashdownAnimation : KeyboardAnimation
    {
        private static ColorScheme[] frames;

        private static List<GameScenes> validScenes = new List<GameScenes>() {
            GameScenes.FLIGHT
        };

        /// <summary>
        /// Static constructor adds lightning bolts in different colors to both frames
        /// </summary>
        static SplashdownAnimation()
        {
            List<ColorScheme> newFrames = new List<ColorScheme>();

            ColorScheme[] uninterpolated = generateAnimationFrames();
            
            for(int i = 0; i < uninterpolated.Length - 1; i++)
            {
                newFrames.AddRange(AnimationUtils.InterpolateFrames(uninterpolated[i], uninterpolated[i + 1], 5));
            }
            newFrames.AddRange(AnimationUtils.InterpolateFrames(uninterpolated[uninterpolated.Length - 1], new ColorScheme(new Color(0f, 0f, .2f)), 10));

            frames = newFrames.ToArray();
        }

        private static ColorScheme[] generateAnimationFrames()
        {
            /// Init with sine wave
            ColorScheme[] myReturn = new ColorScheme[40];
            for(int i = 0; i < myReturn.Length; i++)
            {
                myReturn[i] = AnimationUtils.CircularSineWave(new Color(0f, 0f, .2f) , new Color(.3f, .3f, 1f), i);
            }

           return myReturn;
        }

        public SplashdownAnimation() : base(40, validScenes, frames)
        {
        }
    }
}