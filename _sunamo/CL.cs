namespace SunamoThisApp._sunamo;
internal class CL
{
    public static void ChangeColorOfConsoleAndWrite(TypeOfMessageTA tz, string text, params object[] args)
    {

        SetColorOfConsole(tz);

        Console.WriteLine(text, args);
        SetColorOfConsole(TypeOfMessageTA.Ordinal);
    }

    public static void SetColorOfConsole(TypeOfMessageTA tz)
    {
        var bk = ConsoleColor.White;

        switch (tz)
        {
            case TypeOfMessageTA.Error:
                bk = ConsoleColor.Red;
                break;
            case TypeOfMessageTA.Warning:
                bk = ConsoleColor.Yellow;
                break;
            case TypeOfMessageTA.Information:

            case TypeOfMessageTA.Ordinal:
                bk = ConsoleColor.White;
                break;
            case TypeOfMessageTA.Appeal:
                bk = ConsoleColor.Magenta;
                break;
            case TypeOfMessageTA.Success:
                bk = ConsoleColor.Green;
                break;
        }

        if (bk != ConsoleColor.Black)
            Console.ForegroundColor = bk;
        else
            Console.ResetColor();
    }

    /// <summary>
    ///     For TextWriter use Error2
    /// </summary>
    /// <param name="text"></param>
    /// <param name="p"></param>
    public static void Error(string text, params string[] p)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageTA.Error, text, p);
    }

    /// <summary>
    ///     In every task - Start
    /// </summary>
    /// <param name="text"></param>
    /// <param name="p"></param>
    public static void Warning(string text, params string[] p)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageTA.Warning, text, p);
    }

    public static void Information(string text, params string[] p)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageTA.Information, text, p);
    }

    /// <summary>
    ///     In every task - end
    /// </summary>
    /// <param name="text"></param>
    /// <param name="p"></param>
    public static void Success(string text, params string[] p)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageTA.Success, text, p);
    }

    /// <summary>
    ///     RunInCycle both
    /// </summary>
    /// <param name="appeal"></param>
    public static void Appeal(string appeal)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageTA.Appeal, appeal);
    }
}
