namespace SunamoThisApp._sunamo;

/// <summary>
/// Console logging helper that writes colored messages to the console based on message type.
/// </summary>
internal class CL
{
    /// <summary>
    /// Changes the console color according to the message type, writes the text, then resets to ordinal.
    /// </summary>
    /// <param name="typeOfMessage">The type of message determining the console color.</param>
    /// <param name="text">The format string to write.</param>
    /// <param name="args">Optional format arguments.</param>
    internal static void ChangeColorOfConsoleAndWrite(TypeOfMessageTA typeOfMessage, string text, params object[] args)
    {
        SetColorOfConsole(typeOfMessage);
        Console.WriteLine(text, args);
        SetColorOfConsole(TypeOfMessageTA.Ordinal);
    }

    /// <summary>
    /// Sets the console foreground color based on the message type.
    /// </summary>
    /// <param name="typeOfMessage">The type of message determining the color.</param>
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

    /// <summary>
    /// Writes an error message to the console. For TextWriter use Error2.
    /// </summary>
    /// <param name="text">The error message format string.</param>
    /// <param name="args">Optional format arguments.</param>
    internal static void Error(string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageTA.Error, text, args);
    }

    /// <summary>
    /// Writes a warning message to the console.
    /// </summary>
    /// <param name="text">The warning message format string.</param>
    /// <param name="args">Optional format arguments.</param>
    internal static void Warning(string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageTA.Warning, text, args);
    }

    /// <summary>
    /// Writes an informational message to the console.
    /// </summary>
    /// <param name="text">The informational message format string.</param>
    /// <param name="args">Optional format arguments.</param>
    internal static void Information(string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageTA.Information, text, args);
    }

    /// <summary>
    /// Writes a success message to the console.
    /// </summary>
    /// <param name="text">The success message format string.</param>
    /// <param name="args">Optional format arguments.</param>
    internal static void Success(string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageTA.Success, text, args);
    }

    /// <summary>
    /// Writes an appeal message to the console to draw user attention.
    /// </summary>
    /// <param name="text">The appeal message text.</param>
    internal static void Appeal(string text)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageTA.Appeal, text);
    }
}
