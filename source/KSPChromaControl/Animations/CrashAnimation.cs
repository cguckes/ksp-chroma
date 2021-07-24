namespace KspChromaControl.Animations
{
    using System.Collections.Generic;
    using System.Linq;
    using KspChromaControl.ColorSchemes;
    using UnityEngine;

    /// <summary>
    ///     Animation displayed when you manage to explode the root part of your vessel.
    /// </summary>
    internal class CrashAnimation : KeyboardAnimation
    {
        /// <summary>
        ///     All animation frames for this animation.
        /// </summary>
        private static readonly ColorScheme[] frames;

        /// <summary>
        ///     List of scenes this animation is valid in.
        /// </summary>
        private static readonly List<GameScenes> validScenes = new List<GameScenes>
        {
            GameScenes.FLIGHT
        };

        /// <summary>
        ///     Static constructor adds lightning bolts in different colors to both frames
        /// </summary>
        static CrashAnimation()
        {
            var newFrames = new List<ColorScheme>();

            var red = new ColorScheme(Color.red);
            var yellow = new ColorScheme(Color.yellow);

            // Generate first few frames
            newFrames.AddRange(AnimationUtils.InterpolateFrames(red, yellow, 3));
            // Add the way back to the original
            newFrames.AddRange(newFrames.ToArray().Reverse());
            // Double it
            newFrames.AddRange(newFrames.ToArray());
            // Quadruple it
            newFrames.AddRange(newFrames.ToArray());
            // Octuple it
            newFrames.AddRange(newFrames.ToArray());

            for (var i = 0; i < 30; i++)
            {
                newFrames.Add(AnimationUtils.CircularSineWave(Color.red, Color.yellow, i));
            }

            newFrames.AddRange(AnimationUtils.InterpolateFrames(newFrames.Last(), new ColorScheme(Color.black), 10));

            frames = newFrames.ToArray();
        }

        /// <summary>
        ///     Initializes the base keyboard animation object.
        /// </summary>
        public CrashAnimation() : base(30, validScenes, frames)
        {
        }
    }
}