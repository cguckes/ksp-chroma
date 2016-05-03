using KSP_Chroma_Control.ColorSchemes;
using System.Collections.Generic;
using UnityEngine;

namespace KSP_Chroma_Control
{
    /// <summary>
    /// Implement this to create an animation on your keyboard.
    /// </summary>
    public abstract class KeyboardAnimation
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

            if(frames != null && frames.Length > currentFrame)
            {
                myReturn = frames[currentFrame];
            }
            else
            {
                myReturn = new ColorScheme(Color.black);
                frames = null;
            }
            return myReturn;
        }

        /// <summary>
        /// Checks if the animation is complete.
        /// </summary>
        /// <returns>true, if the animation is finished.</returns>
        public virtual bool isFinished()
        {
            return !validScenes.Contains(HighLogic.LoadedScene) || frames == null || currentFrame >= frames.Length;
        }
    }
}