using KSP_Chroma_Control.ColorSchemes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSP_Chroma_Control
{
    /// <summary>
    /// Handles all animations to avoid confusion and multiple animations running at the same time.
    /// </summary>
    class AnimationManager
    {
        /// <summary>
        /// Singleton instance
        /// </summary>
        private static AnimationManager instance = null;

        /// <summary>
        /// Instance getter
        /// </summary>
        public static AnimationManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new AnimationManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// The currently running animation or null
        /// </summary>
        private KeyboardAnimation activeAnimation = null;

        /// <summary>
        /// Private constructor to avoid out of singleton instantiation
        /// </summary>
        private AnimationManager()
        {
        }

        /// <summary>
        /// Set the current animation
        /// </summary>
        /// <param name="animation">the animation to display</param>
        public void setAnimation(KeyboardAnimation animation)
        {
            this.activeAnimation = animation;
        }

        /// <summary>
        /// Fetches one frame from the animation
        /// </summary>
        /// <returns>the current animation frame</returns>
        public ColorScheme getFrame()
        {
            return (animationRunning()) ? activeAnimation.getFrame() : new ColorScheme();
        }

        /// <summary>
        /// Checks if there is still an animation running.
        /// </summary>
        /// <returns></returns>
        public Boolean animationRunning()
        {
            if (activeAnimation != null && activeAnimation.isFinished())
                activeAnimation = null;
            return activeAnimation != null && !activeAnimation.isFinished();
        }
    }
}
