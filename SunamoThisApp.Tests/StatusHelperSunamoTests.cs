namespace SunamoThisApp.Tests;

/// <summary>
/// Tests of <see cref="StatusHelperSunamo"/>.
/// </summary>
public class StatusHelperSunamoTests
{
    /// <summary>
    /// Every known prefix gives its type, regardless of letter case, and is removed from the text.
    /// </summary>
    [Theory]
    [InlineData("error:Disk is full", TypeOfMessageTA.Error, "Disk is full")]
    [InlineData("Error:Disk is full", TypeOfMessageTA.Error, "Disk is full")]
    [InlineData("WARNING: Low space", TypeOfMessageTA.Warning, " Low space")]
    [InlineData("success:Saved", TypeOfMessageTA.Success, "Saved")]
    [InlineData("info:Started", TypeOfMessageTA.Information, "Started")]
    [InlineData("Information:Started", TypeOfMessageTA.Information, "Started")]
    [InlineData("appeal:Insert disk", TypeOfMessageTA.Appeal, "Insert disk")]
    [InlineData("Plain text", TypeOfMessageTA.Ordinal, "Plain text")]
    [InlineData("", TypeOfMessageTA.Ordinal, "")]
    public void ParseMessageType_RecognizesPrefixAndRemovesIt(string text, TypeOfMessageTA expectedType, string expectedText)
    {
        var typeOfMessage = StatusHelperSunamo.ParseMessageType(ref text);

        Assert.Equal(expectedType, typeOfMessage);
        Assert.Equal(expectedText, text);
    }

    /// <summary>
    /// The overload without ref returns the same type as the one that trims the prefix.
    /// </summary>
    [Fact]
    public void ParseMessageType_WithoutRef_ReturnsType()
    {
        Assert.Equal(TypeOfMessageTA.Warning, StatusHelperSunamo.ParseMessageType("warning:Low space"));
    }
}
