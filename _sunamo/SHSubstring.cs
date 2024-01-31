namespace SunamoThisApp._sunamo;
public class SHSubstring
{
    public static string SubstringIfAvailable(string input, int lenght)
    {
        return input.Length > lenght ? input.Substring(0, lenght) : input;
    }
}
