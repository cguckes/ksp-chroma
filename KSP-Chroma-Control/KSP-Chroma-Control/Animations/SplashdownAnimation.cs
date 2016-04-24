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
    internal class SplashdownAnimation : KeyboardAnimation
    {
        /// <summary>
        /// List of all animation frames.
        /// </summary>
        private static List<ColorScheme> frames = new List<ColorScheme>();

        /// <summary>
        /// The framerate of this animation.
        /// </summary>
        private static double fps = 40;

        /// <summary>
        /// Static constructor adds lightning bolts in different colors to both frames
        /// </summary>
        static SplashdownAnimation()
        {
            ColorScheme[] uninterpolated = generateAnimationFrames();
            
            for(int i = 0; i < uninterpolated.Length - 1; i++)
            {
                frames.AddRange(AnimationUtils.InterpolateFrames(uninterpolated[i], uninterpolated[i + 1], 5));
            }
            frames.AddRange(AnimationUtils.InterpolateFrames(uninterpolated[uninterpolated.Length - 1], new ColorScheme(new Color(0f, 0f, .2f)), 10));
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

        /// <summary>
        /// Local copy to be able to pop the objects we need.
        /// </summary>
        private Queue<ColorScheme> localCopy;
        private int lastFrameTime = 0;
        private ColorScheme currentFrame;

        public SplashdownAnimation()
        {
            localCopy = new Queue<ColorScheme>(frames);
        }

        /// <summary>
        /// <see cref="KeyboardAnimation.getFrame"/>
        /// </summary>
        /// <returns>the current animation frame.</returns>
        public ColorScheme getFrame()
        {
            if(lastFrameTime < (int)(Time.realtimeSinceStartup * fps))
            {
                currentFrame = this.localCopy.Dequeue();
                lastFrameTime = (int)(Time.realtimeSinceStartup * fps);
            }
            return currentFrame;
        }

        /// <summary>
        /// <see cref="KeyboardAnimation.isFinished"/>
        /// </summary>
        /// <returns>true, if the animation is finished, false if not.</returns>
        public bool isFinished()
        {
            /// Exit if the scene changes.
            if (HighLogic.LoadedScene != GameScenes.FLIGHT)
                return true;
            return localCopy.Count() <= 0;
        }
    }
}