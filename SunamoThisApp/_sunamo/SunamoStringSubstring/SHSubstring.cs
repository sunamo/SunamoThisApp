namespace SunamoThisApp._sunamo.SunamoStringSubstring;

internal class SHSubstring
{
    internal static string SubstringIfAvailable(string text, int length)
        => text.Length > length ? text.Substring(0, length) : text;
}
