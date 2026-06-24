namespace SunamoThisApp._sunamo;

// Console logging helper that writes colored messages to the console based on message type.
internal class CL
{
    internal static void ChangeColorOfConsoleAndWrite(TypeOfMessageTA typeOfMessage, string text, params object[] args)
    {
        SetColorOfConsole(typeOfMessage);
        Console.WriteLine(text, args);
        SetColorOfConsole(TypeOfMessageTA.Ordinal);
    }

    internal static void SetColorOfConsole(TypeOfMessageTA typeOfMessage)
    {
        var consoleColor = ConsoleColor.White;

        switch (typeOfMessage)
        {
            case TypeOfMessageTA.Error:
                consoleColor = ConsoleColor.Red;
                break;
            case TypeOfMessageTA.Warning:
                consoleColor = ConsoleColor.Yellow;
                break;
            case TypeOfMessageTA.Information:
            case TypeOfMessageTA.Ordinal:
                consoleColor = ConsoleColor.White;
                break;
            case TypeOfMessageTA.Appeal:
                consoleColor = ConsoleColor.Magenta;
                break;
            case TypeOfMessageTA.Success:
                consoleColor = ConsoleColor.Green;
                break;
        }

        if (consoleColor != ConsoleColor.Black)
            Console.ForegroundColor = consoleColor;
        else
            Console.ResetColor();
    }

    // For TextWriter use Error2.
    internal static void Error(string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageTA.Error, text, args);
    }

    internal static void Warning(string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageTA.Warning, text, args);
    }

    internal static void Information(string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageTA.Information, text, args);
    }

    internal static void Success(string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageTA.Success, text, args);
    }

    internal static void Appeal(string text)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageTA.Appeal, text);
    }
}
