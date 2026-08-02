namespace SunamoThisApp._sunamo.SunamoExceptions;

/// <summary>
/// Provides helper methods for exception information extraction and formatting.
/// </summary>
internal sealed partial class Exceptions
{
    /// <summary>
    /// Extracts the type name, method name, and full stack trace text from the current call stack.
    /// </summary>
    /// <param name="isFillingAlsoFirstTwo">Whether to also fill the type and method name from the first non-ThrowEx frame.</param>
    /// <returns>A tuple of (typeName, methodName, stackTraceText).</returns>
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

    /// <summary>
    /// Parses a stack trace line to extract the type name and method name.
    /// </summary>
    /// <param name="line">A single stack trace line to parse.</param>
    /// <param name="typeName">The extracted type name.</param>
    /// <param name="methodName">The extracted method name.</param>
    internal static void TypeAndMethodName(string line, out string typeName, out string methodName)
    {
        var methodCallText = line.Split(new string[] { "at " }, StringSplitOptions.None)[1].Trim();
        var qualifiedName = methodCallText.Split(new string[] { "(" }, StringSplitOptions.None)[0];
        var nameParts = qualifiedName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = nameParts[^1];
        nameParts.RemoveAt(nameParts.Count - 1);
        typeName = string.Join(".", nameParts);
    }

    /// <summary>
    /// Returns the name of the calling method at the specified stack depth.
    /// </summary>
    /// <param name="depth">The stack frame depth to retrieve the method name from.</param>
    /// <returns>The name of the calling method, or a fallback message if unavailable.</returns>
    internal static string CallingMethod(int depth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(depth)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name could not be obtained";
        }
        var methodName = methodBase.Name;
        return methodName;
    }

    /// <summary>
    /// Creates an error message for a not-implemented case scenario.
    /// </summary>
    /// <param name="prefix">A prefix to prepend to the error message.</param>
    /// <param name="notImplementedName">The object or type name that is not implemented.</param>
    /// <returns>The formatted error message, or <c>null</c>.</returns>
    internal static string? NotImplementedCase(string prefix, object notImplementedName)
    {
        var forSuffix = string.Empty;
        if (notImplementedName != null)
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

    /// <summary>
    /// Returns the prefix formatted with a colon separator, or empty string if blank.
    /// </summary>
    /// <param name="prefix">The prefix text to format.</param>
    /// <returns>The formatted prefix or empty string.</returns>
    internal static string FormatPrefix(string prefix)
    {
        return string.IsNullOrWhiteSpace(prefix) ? string.Empty : prefix + ": ";
    }
}
