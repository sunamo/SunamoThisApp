// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoThisApp._sunamo.SunamoStringSubstring;
internal class SHSubstring
{
    internal static string SubstringIfAvailable(string input, int lenght)
    {
        return input.Length > lenght ? input.Substring(0, lenght) : input;
    }
}
