using KSP_Chroma_Control.ColorSchemes;

namespace KSP_Chroma_Control
{
    /// <summary>
    /// Implement this to create an animation on your keyboard.
    /// </summary>
    public interface KeyboardAnimation
    {
        /// <summary>
        /// Returns the current animation frame.
        /// </summary>
        /// <returns>the current animation frame.</returns>
        ColorScheme getFrame();

        /// <summary>
        /// Checks if the animation is complete.
        /// </summary>
        /// <returns>true, if the animation is finished.</returns>
        bool isFinished();
    }
}