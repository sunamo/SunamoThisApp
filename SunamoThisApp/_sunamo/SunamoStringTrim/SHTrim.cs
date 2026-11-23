namespace SunamoThisApp._sunamo.SunamoStringTrim;

/// <summary>
/// Trimming helpers for strings.
/// </summary>
internal static class SHTrim
{
    /// <summary>
    /// Removes the prefix from the start of the text when the text starts with it, ignoring letter case and culture.
    /// </summary>
    /// <param name="text">Text to trim; it is changed only when the prefix matched.</param>
    /// <param name="prefix">Prefix to remove.</param>
    /// <returns>True when the prefix was removed.</returns>
    internal static bool TrimIfStartsWith(ref string text, string prefix)
    {
        if (text.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
        {
            text = text.Substring(prefix.Length);
            return true;
        }

        return false;
    }
}
