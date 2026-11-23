namespace SunamoThisApp.Helpers;

/// <summary>
/// Recognizes the type of a status message from its text prefix (for example "error:").
/// </summary>
public static class StatusHelperSunamo
{
    /// <summary>
    /// Prefix of an error message.
    /// </summary>
    public const string ErrorPrefix = "error:";

    /// <summary>
    /// Prefix of a warning message.
    /// </summary>
    public const string WarningPrefix = "warning:";

    /// <summary>
    /// Prefix of a success message.
    /// </summary>
    public const string SuccessPrefix = "success:";

    /// <summary>
    /// Short prefix of an informational message.
    /// </summary>
    public const string InfoPrefix = "info:";

    /// <summary>
    /// Long prefix of an informational message.
    /// </summary>
    public const string InformationPrefix = "information:";

    /// <summary>
    /// Prefix of a message asking the user to do something.
    /// </summary>
    public const string AppealPrefix = "appeal:";

    /// <summary>
    /// Returns the type of the message given by its prefix, without changing the text.
    /// The prefix is matched regardless of letter case; a text without a known prefix is an ordinal message.
    /// </summary>
    /// <param name="text">Text of the message.</param>
    /// <returns>Type of the message.</returns>
    public static TypeOfMessageTA ParseMessageType(string text)
    {
        var textWithoutPrefix = text;
        return ParseMessageType(ref textWithoutPrefix);
    }

    /// <summary>
    /// Returns the type of the message given by its prefix and removes the matched prefix from the text.
    /// The prefix is matched regardless of letter case; a text without a known prefix is an ordinal message.
    /// </summary>
    /// <param name="text">Text of the message; the matched prefix is removed from it.</param>
    /// <returns>Type of the message.</returns>
    public static TypeOfMessageTA ParseMessageType(ref string text)
    {
        if (SHTrim.TrimIfStartsWith(ref text, ErrorPrefix))
        {
            return TypeOfMessageTA.Error;
        }

        if (SHTrim.TrimIfStartsWith(ref text, WarningPrefix))
        {
            return TypeOfMessageTA.Warning;
        }

        if (SHTrim.TrimIfStartsWith(ref text, SuccessPrefix))
        {
            return TypeOfMessageTA.Success;
        }

        if (SHTrim.TrimIfStartsWith(ref text, InfoPrefix) || SHTrim.TrimIfStartsWith(ref text, InformationPrefix))
        {
            return TypeOfMessageTA.Information;
        }

        if (SHTrim.TrimIfStartsWith(ref text, AppealPrefix))
        {
            return TypeOfMessageTA.Appeal;
        }

        return TypeOfMessageTA.Ordinal;
    }

    /// <summary>
    /// Returns the type of the message given by its prefix, without changing the text.
    /// </summary>
    /// <param name="text">Text of the message.</param>
    /// <returns>Type of the message.</returns>
    [Obsolete("Use ParseMessageType; this method returns the type of the message, not a yes/no answer.")]
    public static TypeOfMessageTA IsStatusMessage(string text)
    {
        return ParseMessageType(text);
    }

    /// <summary>
    /// Returns the type of the message given by its prefix and removes the matched prefix from the text.
    /// </summary>
    /// <param name="text">Text of the message; the matched prefix is removed from it.</param>
    /// <returns>Type of the message.</returns>
    [Obsolete("Use ParseMessageType; this method returns the type of the message, not a yes/no answer.")]
    public static TypeOfMessageTA IsStatusMessage(ref string text)
    {
        return ParseMessageType(ref text);
    }
}
