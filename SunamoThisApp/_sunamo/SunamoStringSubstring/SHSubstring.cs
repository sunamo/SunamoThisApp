namespace SunamoThisApp._sunamo.SunamoStringSubstring;

/// <summary>
/// String substring helper utilities.
/// </summary>
internal class SHSubstring
{
    /// <summary>
    /// Returns a substring of the specified length if the text is long enough, otherwise returns the full text.
    /// </summary>
    /// <param name="text">The source text to substring.</param>
    /// <param name="length">The maximum length of the resulting substring.</param>
    /// <returns>A substring of the given length, or the original text if shorter.</returns>
    internal static string SubstringIfAvailable(string text, int length)
    {
        return text.Length > length ? text.Substring(0, length) : text;
    }
}
