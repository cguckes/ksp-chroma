namespace KspChromaControl.SceneManagers
{
    using KspChromaControl.ColorSchemes;

    /// <summary>
    ///     Allows creation of keyboard color managers for multiple scenes.
    /// </summary>
    internal interface ISceneManager
    {
        /// <summary>
        ///     Gets the keyboard color scheme for the current frame.
        /// </summary>
        /// <returns>The new color scheme.</returns>
        ColorScheme GetScheme();
    }
}