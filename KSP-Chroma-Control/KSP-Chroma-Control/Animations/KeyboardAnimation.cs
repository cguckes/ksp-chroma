using KSP_Chroma_Control.ColorSchemes;
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

        public KeyboardAnimation(int fps)
        {
            this.fps = fps;
            this.currentFrame = 0;
        }

        /// <summary>
        /// Returns the current animation frame.
        /// </summary>
        /// <returns>the current animation frame.</returns>
        public ColorScheme getFrame()
        {
            if (lastFrameTime < (int)(Time.realtimeSinceStartup * fps))
            {
                currentFrame++;
                lastFrameTime = (int)(Time.realtimeSinceStartup * fps);
            }
            return frames[currentFrame];
        }

        /// <summary>
        /// Checks if the animation is complete.
        /// </summary>
        /// <returns>true, if the animation is finished.</returns>
        public bool isFinished()
        {
            return currentFrame >= frames.Length;
        }
    }
}