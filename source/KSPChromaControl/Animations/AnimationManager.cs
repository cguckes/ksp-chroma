namespace KspChromaControl.Animations
{
    using KspChromaControl.ColorSchemes;

    /// <summary>
    ///     Handles all animations to avoid confusion and multiple animations running at the same time.
    /// </summary>
    internal class AnimationManager
    {
        /// <summary>
        ///     Singleton instance
        /// </summary>
        private static AnimationManager instance;

        /// <summary>
        ///     The currently running animation or null
        /// </summary>
        private KeyboardAnimation activeAnimation;

        /// <summary>
        ///     Private constructor to avoid out of singleton instantiation
        /// </summary>
        private AnimationManager()
        {
        }

        /// <summary>
        ///     Instance getter
        /// </summary>
        public static AnimationManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AnimationManager();
                }

                return instance;
            }
        }

        /// <summary>
        ///     Set the current animation
        /// </summary>
        /// <param name="animation">the animation to display</param>
        public void SetAnimation(KeyboardAnimation animation) => this.activeAnimation = animation;

        /// <summary>
        ///     Fetches one frame from the animation
        /// </summary>
        /// <returns>the current animation frame</returns>
        public ColorScheme GetFrame() => this.AnimationRunning() ? this.activeAnimation.GetFrame() : new ColorScheme();

        /// <summary>
        ///     Checks if there is still an animation running.
        /// </summary>
        /// <returns></returns>
        public bool AnimationRunning() => this.activeAnimation != null && !this.activeAnimation.IsFinished();
    }
}