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
        /// <summary>
        /// Array of frames we iterate over to show the animation.
        /// </summary>
        private ColorScheme[] frames;

        /// <summary>
        /// The index of the currently displayed frame.
        /// </summary>
        private int currentFrame;

        /// <summary>
        /// The realtime the last frame was displayed
        /// </summary>
        private int lastFrameTime = 0;

        /// <summary>
        /// The fps value for this animation
        /// </summary>
        private int fps = 24;

        /// <summary>
        /// The list of scenes, this animation can be shown in.
        /// </summary>
        private List<GameScenes> validScenes;

        /// <summary>
        /// Creates a new keyboard animation with the given parameters
        /// </summary>
        /// <param name="fps">The number of frames per second we want to use.</param>
        /// <param name="validScenes">A list of scenes, the animation should be valid in.</param>
        /// <param name="frames">A list of all frames, containing the actual animation</param>
        public KeyboardAnimation(int fps, List<GameScenes> validScenes, ColorScheme[] frames)
        {
            this.fps = fps;
            this.currentFrame = 0;
            this.validScenes = validScenes;
            this.frames = frames;
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