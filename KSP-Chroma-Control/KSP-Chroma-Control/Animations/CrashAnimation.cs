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
    internal class CrashAnimation : KeyboardAnimation
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
        static CrashAnimation()
        {
            ColorScheme[] uninterpolated = generateAnimationFrames();
            
            for(int i = 0; i < uninterpolated.Length - 1; i++)
            {
                frames.AddRange(AnimationUtils.InterpolateFrames(uninterpolated[i], uninterpolated[i + 1], 3));
            }
            frames.AddRange(AnimationUtils.InterpolateFrames(uninterpolated[uninterpolated.Length - 1], new ColorScheme(Color.black), 20));
        }

        private static ColorScheme[] generateAnimationFrames()
        {
            ColorScheme red = new ColorScheme(Color.red);
            ColorScheme yellow = new ColorScheme(Color.yellow);

            ColorScheme[] myReturn = new ColorScheme[20];
            
            for(int i = 0; i < 10; i++)
                myReturn[i] = (i % 2 == 0) ? red : yellow;

            for(int i = 10; i < myReturn.Length; i++)
            {
                myReturn[i] = AnimationUtils.CircularSineWave(Color.black, Color.yellow, i);
            }

           return myReturn;
        }

        /// <summary>
        /// Local copy to be able to pop the objects we need.
        /// </summary>
        private Queue<ColorScheme> localCopy;
        private int lastFrameTime = 0;
        private ColorScheme currentFrame;

        public CrashAnimation()
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
            //if (HighLogic.LoadedScene != GameScenes.FLIGHT)
            //    return true;
            return localCopy.Count() <= 0;
        }
    }
}