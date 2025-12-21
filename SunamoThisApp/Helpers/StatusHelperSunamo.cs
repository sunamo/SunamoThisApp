namespace SunamoThisApp.Helpers;

public class StatusHelperSunamo
{
    public static TypeOfMessageTA IsStatusMessage(string resp)
    {
        var result = resp;
        return IsStatusMessage(ref result);
    }

    /// <summary>
    /// If dont start with none, return Ordinal
    /// </summary>
    /// <param name = "resp"></param>
    public static TypeOfMessageTA IsStatusMessage(ref string resp)
    {
        if (SHTrim.TrimIfStartsWith(ref resp, error))
        {
            return TypeOfMessageTA.Error;
        }
        else if (SHTrim.TrimIfStartsWith(ref resp, warning))
        {
            return TypeOfMessageTA.Warning;
        }
        else if (SHTrim.TrimIfStartsWith(ref resp, success))
        {
            return TypeOfMessageTA.Success;
        }
        else if (SHTrim.TrimIfStartsWith(ref resp, info))
        {
            return TypeOfMessageTA.Information;
        }
        else if (SHTrim.TrimIfStartsWith(ref resp, information))
        {
            return TypeOfMessageTA.Information;
        }
        else if (SHTrim.TrimIfStartsWith(ref resp, appeal))
        {
            return TypeOfMessageTA.Appeal;
        }

        return TypeOfMessageTA.Ordinal;
    }

    public const string error = "error:";
    public const string warning = "warning:";
    public const string success = "success:";
    public const string info = "info:";
    public const string information = "information:";
    public const string appeal = "appeal:";
}