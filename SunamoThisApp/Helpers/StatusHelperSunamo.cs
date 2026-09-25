namespace SunamoThisApp.Helpers;

public class StatusHelperSunamo
{
    // Determines the message type from the text prefix without modifying the input.
    public static TypeOfMessageTA IsStatusMessage(string text)
    {
        var result = text;
        return IsStatusMessage(ref result);
    }

    // Determines the message type from the text prefix and trims the matched prefix from the text.
    // If no prefix matches, returns TypeOfMessageTA.Ordinal.
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

    public const string ErrorPrefix = "error:";
    public const string WarningPrefix = "warning:";
    public const string SuccessPrefix = "success:";
    public const string InfoPrefix = "info:";
    public const string InformationPrefix = "information:";
    public const string AppealPrefix = "appeal:";
}
