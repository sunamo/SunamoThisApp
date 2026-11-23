namespace SunamoThisApp.Tests;

/// <summary>
/// Redirects the standard output and error streams to memory for the duration of a test.
/// </summary>
internal sealed class ConsoleCapture : IDisposable
{
    private readonly TextWriter originalOut = Console.Out;
    private readonly TextWriter originalError = Console.Error;
    private readonly StringWriter outWriter = new();
    private readonly StringWriter errorWriter = new();

    /// <summary>
    /// Starts capturing both console streams.
    /// </summary>
    internal ConsoleCapture()
    {
        Console.SetOut(outWriter);
        Console.SetError(errorWriter);
    }

    /// <summary>
    /// Lines written to the standard output so far.
    /// </summary>
    internal string[] OutLines => SplitLines(outWriter.ToString());

    /// <summary>
    /// Lines written to the standard error stream so far.
    /// </summary>
    internal string[] ErrorLines => SplitLines(errorWriter.ToString());

    /// <summary>
    /// Restores the original console streams.
    /// </summary>
    public void Dispose()
    {
        Console.SetOut(originalOut);
        Console.SetError(originalError);
        outWriter.Dispose();
        errorWriter.Dispose();
    }

    /// <summary>
    /// Splits captured text into non-empty lines.
    /// </summary>
    private static string[] SplitLines(string text)
    {
        return text.Split(["\r\n", "\n"], StringSplitOptions.RemoveEmptyEntries);
    }
}
