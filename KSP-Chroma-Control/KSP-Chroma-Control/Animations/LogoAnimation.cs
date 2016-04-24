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
        /// List of all animation frames.
        /// </summary>
        private static List<ColorScheme> frames = new List<ColorScheme>();

        /// <summary>
        /// The framerate of this animation.
        /// </summary>
        private static double fps = 10;

        /// <summary>
        /// Static constructor adds lightning bolts in different colors to both frames
        /// </summary>
        static LogoAnimation()
        {
            frames.AddRange(AnimationUtils.InterpolateFrames(new ColorScheme(Color.blue), new LogoScheme(), 20));
        }

        /// <summary>
        /// Local copy to be able to pop the objects we need.
        /// </summary>
        private Queue<ColorScheme> localCopy;
        private int lastFrameTime = 0;
        private ColorScheme currentFrame;

        public LogoAnimation()
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
            return localCopy.Count() <= 0;
        }
    }
}