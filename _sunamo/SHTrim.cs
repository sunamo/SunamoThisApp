namespace SunamoThisApp._sunamo;

internal class SHTrim
{


    internal static bool TrimIfStartsWith(ref string s, string p)
    {
        if (s.StartsWith(p))
        {
            s = s.Substring(p.Length);
            return true;
        }
        return false;
    }


}
