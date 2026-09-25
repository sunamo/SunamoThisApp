namespace SunamoThisApp._sunamo.SunamoStringTrim;

/// <summary>
/// String trimming helper utilities.
/// </summary>
internal class SHTrim
{
    /// <summary>
    /// Trims the specified prefix from the beginning of the text if it starts with it.
    /// </summary>
    /// <param name="text">The text to trim. Modified in place if the prefix is found.</param>
    /// <param name="prefix">The prefix to remove from the start of the text.</param>
    /// <returns><c>true</c> if the text started with the prefix and was trimmed; otherwise <c>false</c>.</returns>
    internal static bool TrimIfStartsWith(ref string text, string prefix)
    {
        if (text.StartsWith(prefix))
        {
            text = text.Substring(prefix.Length);
            return true;
        }
        return false;
    }
}
