namespace SunamoThisApp;

/// <summary>
/// Application-wide identity (name, project, event log name) and the single entry point for status messages.
/// </summary>
public static class ThisApp
{
    private static string? project;

    private static string? eventLogName;

    /// <summary>
    /// Raised after every status message is written, so UI applications can show it (for example in a status bar).
    /// Handlers run on the thread that reported the message.
    /// </summary>
    public static event SetStatusDelegate? StatusSet;

    /// <summary>
    /// Solution-level name of the application, without spaces. It is null until the application sets it.
    /// </summary>
    public static string? Name { get; set; }

    /// <summary>
    /// Gets or sets whether dates are formatted in the short form.
    /// </summary>
    public static bool UseShortAsDateTime { get; set; } = true;

    /// <summary>
    /// Gets or sets whether the application runs in debug mode.
    /// </summary>
    public static bool RunInDebug { get; set; } = true;

    /// <summary>
    /// Gets or sets a general purpose flag for the application.
    /// </summary>
    public static bool Check { get; set; }

    /// <summary>
    /// Gets or sets the root namespace of the application.
    /// </summary>
    public static string Namespace { get; set; } = "";

    /// <summary>
    /// Name of the project; falls back to <see cref="Name"/> when it was not set.
    /// </summary>
    public static string? Project
    {
        get => project ?? Name;
        set => project = value;
    }

    /// <summary>
    /// Gets <see cref="Name"/> prefixed with an underscore.
    /// </summary>
    public static string UnderscoreName => "_" + Name;

    /// <summary>
    /// Name of the event log source; a longer value is truncated to 8 characters and an empty one becomes null.
    /// </summary>
    public static string? EventLogName
    {
        get => eventLogName;
        set => eventLogName = string.IsNullOrEmpty(value) ? null : SHSubstring.SubstringIfAvailable(value!, 8);
    }

    /// <summary>
    /// Sets the name of the application.
    /// </summary>
    /// <param name="name">Solution-level name of the application.</param>
    public static void SetName(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Writes a status message to the console in the color of its type and raises <see cref="StatusSet"/>.
    /// Errors go to the standard error stream. A message that is empty after trimming is ignored.
    /// </summary>
    /// <param name="typeOfMessage">Type of the message.</param>
    /// <param name="text">Text of the message; it is a composite format string only when <paramref name="args"/> are passed.</param>
    /// <param name="args">Values for the placeholders in <paramref name="text"/>.</param>
    /// <exception cref="ArgumentOutOfRangeException">The type of the message is not a defined value.</exception>
    public static void SetStatus(TypeOfMessageTA typeOfMessage, string text, params string[] args)
    {
        if (typeOfMessage < TypeOfMessageTA.Error || typeOfMessage > TypeOfMessageTA.Success)
        {
            throw new ArgumentOutOfRangeException(nameof(typeOfMessage), typeOfMessage, "Unknown type of message.");
        }

        if (text is null)
        {
            return;
        }

        var hasArgs = args is not null && args.Length > 0;
        var formattedText = (hasArgs ? string.Format(text, args!) : text).Trim();
        if (formattedText.Length == 0)
        {
            return;
        }

        CL.Write(typeOfMessage, formattedText);
        StatusSet?.Invoke(typeOfMessage, formattedText);
    }

    /// <summary>
    /// Reports a message whose type is given by its prefix (for example "error:"); without a prefix it is an ordinal message.
    /// </summary>
    /// <param name="text">Text of the message, optionally starting with a type prefix.</param>
    public static void StatusFromText(string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            var typeOfMessage = StatusHelperSunamo.ParseMessageType(ref text);
            SetStatus(typeOfMessage, text);
        }
    }

    /// <summary>
    /// Reports a success message.
    /// </summary>
    /// <param name="text">Text of the message; a composite format string when <paramref name="args"/> are passed.</param>
    /// <param name="args">Values for the placeholders in <paramref name="text"/>.</param>
    public static void Success(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Success, text, args);
    }

    /// <summary>
    /// Reports an informational message.
    /// </summary>
    /// <param name="text">Text of the message; a composite format string when <paramref name="args"/> are passed.</param>
    /// <param name="args">Values for the placeholders in <paramref name="text"/>.</param>
    public static void Info(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Information, text, args);
    }

    /// <summary>
    /// Reports an error message.
    /// </summary>
    /// <param name="text">Text of the message; a composite format string when <paramref name="args"/> are passed.</param>
    /// <param name="args">Values for the placeholders in <paramref name="text"/>.</param>
    public static void Error(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Error, text, args);
    }

    /// <summary>
    /// Reports a warning message.
    /// </summary>
    /// <param name="text">Text of the message; a composite format string when <paramref name="args"/> are passed.</param>
    /// <param name="args">Values for the placeholders in <paramref name="text"/>.</param>
    public static void Warning(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Warning, text, args);
    }

    /// <summary>
    /// Reports a message without a specific type.
    /// </summary>
    /// <param name="text">Text of the message; a composite format string when <paramref name="args"/> are passed.</param>
    /// <param name="args">Values for the placeholders in <paramref name="text"/>.</param>
    public static void Ordinal(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Ordinal, text, args);
    }

    /// <summary>
    /// Reports a message asking the user to do something.
    /// </summary>
    /// <param name="text">Text of the message; a composite format string when <paramref name="args"/> are passed.</param>
    /// <param name="args">Values for the placeholders in <paramref name="text"/>.</param>
    public static void Appeal(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Appeal, text, args);
    }

    /// <summary>
    /// Reports the outcome of an operation: an error when the result is the default value of its type, otherwise an optional informational message.
    /// </summary>
    /// <typeparam name="T">Type of the result.</typeparam>
    /// <param name="data">Result of the operation; the default value means failure.</param>
    /// <param name="exceptionText">Error text reported on failure.</param>
    /// <param name="replacementWhenSuccess">Text reported on success; nothing is reported when it is null.</param>
    /// <param name="isShowingToStringOnSuccess">Whether success reports the text of the result instead of <paramref name="replacementWhenSuccess"/>.</param>
    public static void ResultWithException<T>(T data, string exceptionText, string? replacementWhenSuccess = null,
        bool isShowingToStringOnSuccess = false)
    {
        if (EqualityComparer<T>.Default.Equals(data, default!))
        {
            Error(exceptionText);
            return;
        }

        var successText = isShowingToStringOnSuccess ? data!.ToString() : replacementWhenSuccess;
        if (successText is not null)
        {
            Info(successText);
        }
    }
}
