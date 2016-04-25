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
        public static string getName()
        {
            return "KeyBoardAnimation";
        }

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
        public virtual ColorScheme getFrame()
        {
            if (lastFrameTime < (int)(Time.realtimeSinceStartup * fps))
            {
                currentFrame++;
                lastFrameTime = (int)(Time.realtimeSinceStartup * fps);
            }
            return (frames.Length > currentFrame) ? frames[currentFrame] : frames[currentFrame - 1];
        }

        /// <summary>
        /// Checks if the animation is complete.
        /// </summary>
        /// <returns>true, if the animation is finished.</returns>
        public virtual bool isFinished()
        {
            return currentFrame >= frames.Length;
        }
    }
}