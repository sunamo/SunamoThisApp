namespace SunamoThisApp._sunamo.SunamoStringTrim;

internal class SHTrim
{
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
