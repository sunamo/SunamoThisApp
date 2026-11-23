namespace SunamoThisApp._public.SunamoEnums.Enums;

/// <summary>
/// Type of a status message; it decides the color and the output stream of the message.
/// </summary>
public enum TypeOfMessageTA
{
    /// <summary>
    /// Something failed; written in red to the standard error stream.
    /// </summary>
    Error,
    /// <summary>
    /// Something needs attention but did not fail; written in yellow.
    /// </summary>
    Warning,
    /// <summary>
    /// Information about what the application is doing; written in white.
    /// </summary>
    Information,
    /// <summary>
    /// Message without a specific type; written in white.
    /// </summary>
    Ordinal,
    /// <summary>
    /// Request for the user to do something; written in magenta.
    /// </summary>
    Appeal,
    /// <summary>
    /// Something finished successfully; written in green.
    /// </summary>
    Success
}
