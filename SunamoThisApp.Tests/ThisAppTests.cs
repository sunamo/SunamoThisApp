namespace SunamoThisApp.Tests;

/// <summary>
/// Tests of <see cref="ThisApp"/>. They share static state and the console, so they run in one collection.
/// </summary>
[Collection(ConsoleCollection.Name)]
public class ThisAppTests
{
    /// <summary>
    /// A message is written to the console exactly once.
    /// </summary>
    [Fact]
    public void SetStatus_WritesMessageOnce()
    {
        using var console = new ConsoleCapture();

        ThisApp.Info("Application started");

        Assert.Equal(["Application started"], console.OutLines);
        Assert.Empty(console.ErrorLines);
    }

    /// <summary>
    /// Errors go to the standard error stream, not to the standard output.
    /// </summary>
    [Fact]
    public void Error_WritesToErrorStream()
    {
        using var console = new ConsoleCapture();

        ThisApp.Error("Connection failed");

        Assert.Equal(["Connection failed"], console.ErrorLines);
        Assert.Empty(console.OutLines);
    }

    /// <summary>
    /// A text with braces and no arguments is written as is instead of throwing a format exception.
    /// </summary>
    [Fact]
    public void SetStatus_TextWithBracesAndNoArgs_IsWrittenAsIs()
    {
        using var console = new ConsoleCapture();

        ThisApp.Info("File {a}.json contains {\"key\": 1}");

        Assert.Equal(["File {a}.json contains {\"key\": 1}"], console.OutLines);
    }

    /// <summary>
    /// Placeholders are filled when arguments are passed, and only once.
    /// </summary>
    [Fact]
    public void SetStatus_WithArgs_FormatsText()
    {
        using var console = new ConsoleCapture();

        ThisApp.Warning("Copied {0} of {1}", "3", "{5}");

        Assert.Equal(["Copied 3 of {5}"], console.OutLines);
    }

    /// <summary>
    /// A message that is empty after trimming is ignored.
    /// </summary>
    [Fact]
    public void SetStatus_BlankText_WritesNothing()
    {
        using var console = new ConsoleCapture();

        ThisApp.Info("   ");

        Assert.Empty(console.OutLines);
    }

    /// <summary>
    /// An undefined type of message is rejected with a specific exception.
    /// </summary>
    [Fact]
    public void SetStatus_UnknownType_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => ThisApp.SetStatus((TypeOfMessageTA)99, "text"));
    }

    /// <summary>
    /// The console color is the same after the message as before it.
    /// </summary>
    [Fact]
    public void SetStatus_RestoresConsoleColor()
    {
        using var console = new ConsoleCapture();
        var colorBefore = Console.ForegroundColor;

        ThisApp.Success("Done");

        Assert.Equal(colorBefore, Console.ForegroundColor);
    }

    /// <summary>
    /// The event receives the type and the final text of every message.
    /// </summary>
    [Fact]
    public void SetStatus_RaisesStatusSet()
    {
        using var console = new ConsoleCapture();
        var received = new List<(TypeOfMessageTA Type, string Message)>();
        void Handler(TypeOfMessageTA typeOfMessage, string message) => received.Add((typeOfMessage, message));

        ThisApp.StatusSet += Handler;
        try
        {
            ThisApp.Appeal(" Insert {0} ", "disk");
        }
        finally
        {
            ThisApp.StatusSet -= Handler;
        }

        Assert.Equal([(TypeOfMessageTA.Appeal, "Insert disk")], received);
    }

    /// <summary>
    /// The prefix decides the type of the message and is removed from the written text.
    /// </summary>
    [Fact]
    public void StatusFromText_UsesPrefix()
    {
        using var console = new ConsoleCapture();

        ThisApp.StatusFromText("Error: Something went wrong");
        ThisApp.StatusFromText("success:Saved");

        Assert.Equal(["Something went wrong"], console.ErrorLines);
        Assert.Equal(["Saved"], console.OutLines);
    }

    /// <summary>
    /// The event log name is truncated to 8 characters and an empty value becomes null.
    /// </summary>
    [Theory]
    [InlineData("VeryLongApplicationName", "VeryLong")]
    [InlineData("Short", "Short")]
    [InlineData("", null)]
    [InlineData(null, null)]
    public void EventLogName_IsTruncatedToEightCharacters(string? value, string? expected)
    {
        ThisApp.EventLogName = value;

        Assert.Equal(expected, ThisApp.EventLogName);
    }

    /// <summary>
    /// The project falls back to the name of the application until it is set.
    /// </summary>
    [Fact]
    public void Project_FallsBackToName()
    {
        var originalName = ThisApp.Name;
        var originalProject = ThisApp.Project;
        try
        {
            ThisApp.Project = null;
            ThisApp.SetName("MyApplication");
            Assert.Equal("MyApplication", ThisApp.Project);
            Assert.Equal("_MyApplication", ThisApp.UnderscoreName);

            ThisApp.Project = "MyProject";
            Assert.Equal("MyProject", ThisApp.Project);
        }
        finally
        {
            ThisApp.Name = originalName;
            ThisApp.Project = originalName == originalProject ? null : originalProject;
        }
    }

    /// <summary>
    /// A default result reports the error text.
    /// </summary>
    [Fact]
    public void ResultWithException_DefaultResult_ReportsError()
    {
        using var console = new ConsoleCapture();

        ThisApp.ResultWithException<string?>(null, "Loading failed", "Loaded");

        Assert.Equal(["Loading failed"], console.ErrorLines);
        Assert.Empty(console.OutLines);
    }

    /// <summary>
    /// A non-default result reports the replacement text, the text of the result, or nothing.
    /// </summary>
    [Fact]
    public void ResultWithException_Success_ReportsInfo()
    {
        using var console = new ConsoleCapture();

        ThisApp.ResultWithException(5, "Failed", "Loaded");
        ThisApp.ResultWithException(7, "Failed", isShowingToStringOnSuccess: true);
        ThisApp.ResultWithException(9, "Failed");

        Assert.Equal(["Loaded", "7"], console.OutLines);
        Assert.Empty(console.ErrorLines);
    }
}
