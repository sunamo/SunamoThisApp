namespace SunamoThisApp._sunamo;

/// <summary>
/// Writes status messages to the console in the color of their type.
/// </summary>
internal static class CL
{
    // Color, text and color restore of one message must not interleave with another thread.
    private static readonly object ConsoleLock = new();

    /// <summary>
    /// Writes one message in the color of its type and then restores the previous console color.
    /// Errors go to the standard error stream, everything else to the standard output.
    /// </summary>
    /// <param name="typeOfMessage">Type of the message.</param>
    /// <param name="text">Final text of the message; it is written as is, without formatting.</param>
    internal static void Write(TypeOfMessageTA typeOfMessage, string text)
    {
        var writer = typeOfMessage == TypeOfMessageTA.Error ? Console.Error : Console.Out;

        lock (ConsoleLock)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ColorOf(typeOfMessage);
            try
            {
                writer.WriteLine(text);
            }
            finally
            {
                Console.ForegroundColor = previousColor;
            }
        }
    }

    /// <summary>
    /// Returns the console color used for the type of message.
    /// </summary>
    /// <param name="typeOfMessage">Type of the message.</param>
    /// <returns>Console color of the message.</returns>
    internal static ConsoleColor ColorOf(TypeOfMessageTA typeOfMessage)
    {
        return typeOfMessage switch
        {
            TypeOfMessageTA.Error => ConsoleColor.Red,
            TypeOfMessageTA.Warning => ConsoleColor.Yellow,
            TypeOfMessageTA.Appeal => ConsoleColor.Magenta,
            TypeOfMessageTA.Success => ConsoleColor.Green,
            _ => ConsoleColor.White
        };
    }
}
