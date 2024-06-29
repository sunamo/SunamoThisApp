namespace SunamoThisApp;


public partial class StatusHelperSunamo
{
    public static TypeOfMessageTA IsStatusMessage(string resp)
    {
        var r = resp;
        return IsStatusMessage(ref r);
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
}
