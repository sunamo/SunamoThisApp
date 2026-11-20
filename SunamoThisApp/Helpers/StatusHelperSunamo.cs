namespace SunamoThisApp.Helpers;

/// <summary>
/// Represents the status helper sunamo type.
/// </summary>
public class StatusHelperSunamo
{
    // Determines the message type from the text prefix without modifying the input.
    /// <summary>
    /// Is status message.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The is status message result.</returns>
    public static TypeOfMessageTA IsStatusMessage(string text)
    {
        var result = text;
        return IsStatusMessage(ref result);
    }

    // Determines the message type from the text prefix and trims the matched prefix from the text.
    // If no prefix matches, returns TypeOfMessageTA.Ordinal.
    /// <summary>
    /// Is status message.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The is status message result.</returns>
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
    /// The error prefix constant.
    /// </summary>
    public const string ErrorPrefix = "error:";
    /// <summary>
    /// The warning prefix constant.
    /// </summary>
    public const string WarningPrefix = "warning:";
    /// <summary>
    /// The success prefix constant.
    /// </summary>
    public const string SuccessPrefix = "success:";
    /// <summary>
    /// The info prefix constant.
    /// </summary>
    public const string InfoPrefix = "info:";
    /// <summary>
    /// The information prefix constant.
    /// </summary>
    public const string InformationPrefix = "information:";
    /// <summary>
    /// The appeal prefix constant.
    /// </summary>
    public const string AppealPrefix = "appeal:";
}
