namespace KspChromaControl.Animations
{
    using System.Collections.Generic;
    using KspChromaControl.ColorSchemes;
    using UnityEngine;

    /// <summary>
    ///     Slowly displays the KSP logo from a blue base scheme.
    /// </summary>
    internal class LogoAnimation : KeyboardAnimation
    {
        /// <summary>
        ///     The frames this animation consists of.
        /// </summary>
        private static readonly ColorScheme[] frames;

        /// <summary>
        ///     List of scenes this animation is valid in.
        /// </summary>
        private static readonly List<GameScenes> validScenes = new List<GameScenes>
        {
            GameScenes.MAINMENU,
            GameScenes.SPACECENTER,
            GameScenes.TRACKSTATION,
            GameScenes.CREDITS
        };

        /// <summary>
        ///     Static constructor interpolates from blue to logo.
        /// </summary>
        static LogoAnimation()
        {
            frames = AnimationUtils.InterpolateFrames(new ColorScheme(Color.blue), new LogoScheme(), 20);
        }

        /// <summary>
        ///     Initializes the base keyboard animation object.
        /// </summary>
        public LogoAnimation() : base(10, validScenes, frames)
        {
        }
    }
}