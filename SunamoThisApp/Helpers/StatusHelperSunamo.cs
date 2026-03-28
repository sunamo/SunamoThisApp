namespace SunamoThisApp.Helpers;

/// <summary>
/// Helper class for parsing status message prefixes and determining message types.
/// </summary>
public class StatusHelperSunamo
{
    /// <summary>
    /// Determines the message type from the text prefix without modifying the input.
    /// </summary>
    /// <param name="text">The text to analyze for a status prefix.</param>
    /// <returns>The detected message type, or <see cref="TypeOfMessageTA.Ordinal"/> if no prefix matched.</returns>
    public static TypeOfMessageTA IsStatusMessage(string text)
    {
        var result = text;
        return IsStatusMessage(ref result);
    }

    /// <summary>
    /// Determines the message type from the text prefix and trims the matched prefix from the text.
    /// If no prefix matches, returns <see cref="TypeOfMessageTA.Ordinal"/>.
    /// </summary>
    /// <param name="text">The text to analyze and trim. Modified in place if a prefix is found.</param>
    /// <returns>The detected message type.</returns>
    public static TypeOfMessageTA IsStatusMessage(ref string text)
    {
        if (SHTrim.TrimIfStartsWith(ref text, ErrorPrefix))
        {
            return TypeOfMessageTA.Error;
        }
        else if (SHTrim.TrimIfStartsWith(ref text, WarningPrefix))
        {
            return TypeOfMessageTA.Warning;
        }
        else if (SHTrim.TrimIfStartsWith(ref text, SuccessPrefix))
        {
            return TypeOfMessageTA.Success;
        }
        else if (SHTrim.TrimIfStartsWith(ref text, InfoPrefix))
        {
            return TypeOfMessageTA.Information;
        }
        else if (SHTrim.TrimIfStartsWith(ref text, InformationPrefix))
        {
            return TypeOfMessageTA.Information;
        }
        else if (SHTrim.TrimIfStartsWith(ref text, AppealPrefix))
        {
            return TypeOfMessageTA.Appeal;
        }

        return TypeOfMessageTA.Ordinal;
    }

    /// <summary>
    /// The prefix string for error messages.
    /// </summary>
    public const string ErrorPrefix = "error:";

    /// <summary>
    /// The prefix string for warning messages.
    /// </summary>
    public const string WarningPrefix = "warning:";

    /// <summary>
    /// The prefix string for success messages.
    /// </summary>
    public const string SuccessPrefix = "success:";

    /// <summary>
    /// The prefix string for short informational messages.
    /// </summary>
    public const string InfoPrefix = "info:";

    /// <summary>
    /// The prefix string for informational messages.
    /// </summary>
    public const string InformationPrefix = "information:";

    /// <summary>
    /// The prefix string for appeal messages.
    /// </summary>
    public const string AppealPrefix = "appeal:";
}
