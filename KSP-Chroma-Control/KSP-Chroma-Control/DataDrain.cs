namespace KspChromaControl
{
    using KspChromaControl.ColorSchemes;

    /// <summary>
    ///     Implement this to use the mod with other devices.
    /// </summary>
    internal interface IDataDrain
    {
        /// <summary>
        ///     Sends the requested color scheme to the implemented output channel
        /// </summary>
        /// <param name="scheme">The color scheme to apply to the implemented output device</param>
        void Send(ColorScheme scheme);
    }
}