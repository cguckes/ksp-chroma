using KspChromaControl.ColorSchemes;
using System.Collections.Generic;
using UnityEngine;

namespace KspChromaControl
{
    /// <summary>
    /// Implement this to create an animation on your keyboard.
    /// </summary>
    internal abstract class KeyboardAnimation
    {
        protected static ColorScheme[] frames;

        private int currentFrame;
        private int lastFrameTime = 0;
        private int fps = 10;
        private List<GameScenes> validScenes;

        public KeyboardAnimation(int fps, List<GameScenes> validScenes)
        {
            this.fps = fps;
            this.currentFrame = 0;
            this.validScenes = validScenes;
        }

        /// <summary>
        /// Returns the current animation frame.
        /// </summary>
        /// <returns>the current animation frame.</returns>
        public virtual ColorScheme getFrame()
        {
            ColorScheme myReturn = null;

            if (lastFrameTime < (int)(Time.realtimeSinceStartup * fps))
            {
                currentFrame++;
                lastFrameTime = (int)(Time.realtimeSinceStartup * fps);
            }

            if(frames.Length > currentFrame)
            {
                myReturn = frames[currentFrame];
            }
            else
            {
                myReturn = frames[frames.Length - 1];
            }
            return myReturn;
        }

        /// <summary>
        /// Checks if the animation is complete.
        /// </summary>
        /// <returns>true, if the animation is finished.</returns>
        public virtual bool isFinished()
        {
            return !validScenes.Contains(HighLogic.LoadedScene) || currentFrame >= frames.Length;
        }
    }
}