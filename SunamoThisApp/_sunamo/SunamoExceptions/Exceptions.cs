namespace SunamoThisApp._sunamo.SunamoExceptions;

internal sealed partial class Exceptions
{
    internal static Tuple<string, string, string> PlaceOfException(bool isFillingAlsoFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceText = stackTrace.ToString();
        var lines = stackTraceText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        var index = 0;
        string typeName = string.Empty;
        string methodName = string.Empty;
        for (; index < lines.Count; index++)
        {
            var line = lines[index];
            if (isFillingAlsoFirstTwo)
                if (!line.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(line, out typeName, out methodName);
                    isFillingAlsoFirstTwo = false;
                }
            if (line.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(typeName, methodName, string.Join(Environment.NewLine, lines));
    }

    internal static void TypeAndMethodName(string line, out string typeName, out string methodName)
    {
        var methodCallText = line.Split("at ")[1].Trim();
        var qualifiedName = methodCallText.Split("(")[0];
        var nameParts = qualifiedName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = nameParts[^1];
        nameParts.RemoveAt(nameParts.Count - 1);
        typeName = string.Join(".", nameParts);
    }

    internal static string CallingMethod(int depth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(depth)?.GetMethod();
        if (methodBase is null)
        {
            return "Method name could not be obtained";
        }
        var methodName = methodBase.Name;
        return methodName;
    }

    internal static string? NotImplementedCase(string prefix, object notImplementedName)
    {
        var forSuffix = string.Empty;
        if (notImplementedName is not null)
        {
            forSuffix = " for ";
            if (notImplementedName.GetType() == typeof(Type))
                forSuffix += ((Type)notImplementedName).FullName;
            else
                forSuffix += notImplementedName.ToString();
        }
        return FormatPrefix(prefix) + "Not implemented case" + forSuffix + " . internal program error. Please contact developer" +
        ".";
    }

    internal static string FormatPrefix(string prefix)
    {
        return string.IsNullOrWhiteSpace(prefix) ? string.Empty : prefix + ": ";
    }
}
