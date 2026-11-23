namespace SunamoThisApp._sunamo.SunamoStringSubstring;

/// <summary>
/// Substring helpers for strings.
/// </summary>
internal static class SHSubstring
{
    /// <summary>
    /// Returns the first characters of the text up to the given length; a shorter text is returned unchanged.
    /// </summary>
    /// <param name="text">Text to shorten.</param>
    /// <param name="length">Maximum length of the result.</param>
    /// <returns>Text no longer than the given length.</returns>
    internal static string SubstringIfAvailable(string text, int length)
        => text.Length > length ? text.Substring(0, length) : text;
}
