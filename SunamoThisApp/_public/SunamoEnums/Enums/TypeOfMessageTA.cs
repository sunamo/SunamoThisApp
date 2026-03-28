namespace SunamoThisApp._public.SunamoEnums.Enums;

/// <summary>
/// Defines the type of message for application status reporting.
/// </summary>
public enum TypeOfMessageTA
{
    /// <summary>
    /// Error message type.
    /// </summary>
    Error,

    /// <summary>
    /// Warning message type.
    /// </summary>
    Warning,

    /// <summary>
    /// Informational message type.
    /// </summary>
    Information,

    /// <summary>
    /// Ordinal (default) message type.
    /// </summary>
    Ordinal,

    /// <summary>
    /// Appeal message type for user attention.
    /// </summary>
    Appeal,

    /// <summary>
    /// Success message type.
    /// </summary>
    Success
}