using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoThisApp._sunamo;
internal class SHSubstring
{
    internal static string SubstringIfAvailable(string input, int lenght)
    {
        return input.Length > lenght ? input.Substring(0, lenght) : input;
    }
}
