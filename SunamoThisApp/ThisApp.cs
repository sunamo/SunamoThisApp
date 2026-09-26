namespace SunamoThisApp;

/// <summary>
/// Central application configuration and status reporting class for Sunamo platform applications.
/// </summary>
public class ThisApp
{
    /// <summary>
    /// The solution-level name of the application. In selling it is without spaces.
    /// Name = Solution, Project = Project.
    /// </summary>
    public static string Name { get; set; } = null!;

    /// <summary>
    /// Whether to use short format for DateTime display.
    /// </summary>
    public static bool UseShortAsDateTime { get; set; } = true;

    /// <summary>
    /// Whether the application is running in debug mode.
    /// </summary>
    public static bool RunInDebug { get; set; } = true;

    /// <summary>
    /// Translation function that resolves a key to a localized string. Used by <see cref="SetStatusXlf"/>.
    /// </summary>
    public static Func<string, string> FromKey { get; set; } = null!;

    /// <summary>
    /// General-purpose check flag for application state.
    /// </summary>
    public static bool Check { get; set; }

    private static string? project;

    /// <summary>
    /// Whether the application has been initialized.
    /// </summary>
    public static readonly bool Initialized = false;

    /// <summary>
    /// The namespace associated with the application.
    /// </summary>
    public static string Namespace { get; set; } = "";

    private static string? eventLogName;

    /// <summary>
    /// Dynamic reference to the application's resource helper.
    /// </summary>
    public static dynamic Resources { get; set; } = null!;

    /// <summary>
    /// The project-level name. Falls back to <see cref="Name"/> if not set.
    /// Name = Solution, Project = Project.
    /// </summary>
    public static string Project
    {
        get
        {
            if (project is null) return Name;
            return project;
        }
        set => project = value;
    }

    /// <summary>
    /// Returns the application name prefixed with an underscore.
    /// </summary>
    public static string UnderscoreName => "_" + Name;

    /// <summary>
    /// The event log name. Can be null, in which case event logging will not be used.
    /// The value is automatically truncated to 8 characters if longer.
    /// </summary>
    public static string? EventLogName
    {
        get => eventLogName;
        set => eventLogName = string.IsNullOrEmpty(value) ? null : SHSubstring.SubstringIfAvailable(value, 8);
    }

    /// <summary>
    /// Sets the application name.
    /// </summary>
    /// <param name="name">The name to set.</param>
    public static void SetName(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Sets the status using a translated key from the XLF translation function.
    /// </summary>
    /// <param name="typeOfMessage">The type of the status message.</param>
    /// <param name="key">The translation key to resolve via <see cref="FromKey"/>.</param>
    public static void SetStatusXlf(TypeOfMessageTA typeOfMessage, string key)
    {
        SetStatus(typeOfMessage, FromKey(key));
    }

    /// <summary>
    /// Sets and displays a formatted status message with the specified type.
    /// </summary>
    /// <param name="typeOfMessage">The type of the status message.</param>
    /// <param name="text">The status message format string.</param>
    /// <param name="args">Optional format arguments for the status message.</param>
    public static void SetStatus(TypeOfMessageTA typeOfMessage, string text, params string[] args)
    {
        var formattedText = string.Format(text, args).Trim();
        if (formattedText != string.Empty)
        {
            switch (typeOfMessage)
            {
                case TypeOfMessageTA.Error:
                    CL.Error(formattedText);
                    break;
                case TypeOfMessageTA.Warning:
                    CL.Warning(formattedText);
                    break;
                case TypeOfMessageTA.Information:
                    CL.Information(formattedText);
                    break;
                case TypeOfMessageTA.Ordinal:
                    CL.Information(formattedText);
                    break;
                case TypeOfMessageTA.Appeal:
                    CL.Appeal(formattedText);
                    break;
                case TypeOfMessageTA.Success:
                    CL.Success(formattedText);
                    break;
                default:
                    ThrowEx.NotImplementedCase(typeOfMessage);
                    break;
            }

            Console.WriteLine(formattedText);
        }
    }

    /// <summary>
    /// Parses the text for a status message prefix and displays it with the appropriate message type.
    /// </summary>
    /// <param name="text">The text to parse and display.</param>
    public static void StatusFromText(string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            var typeOfMessage = StatusHelperSunamo.IsStatusMessage(ref text);
            SetStatus(typeOfMessage, text);
        }
    }

    /// <summary>
    /// Displays a success status message.
    /// </summary>
    /// <param name="text">The message format string.</param>
    /// <param name="args">Optional format arguments.</param>
    public static void Success(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Success, text, args);
    }

    /// <summary>
    /// Displays an informational status message.
    /// </summary>
    /// <param name="text">The message format string.</param>
    /// <param name="args">Optional format arguments.</param>
    public static void Info(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Information, text, args);
    }

    /// <summary>
    /// Displays an error status message.
    /// </summary>
    /// <param name="text">The message format string.</param>
    /// <param name="args">Optional format arguments.</param>
    public static void Error(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Error, text, args);
    }

    /// <summary>
    /// Displays a warning status message.
    /// </summary>
    /// <param name="text">The message format string.</param>
    /// <param name="args">Optional format arguments.</param>
    public static void Warning(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Warning, text, args);
    }

    /// <summary>
    /// Displays an ordinal (default) status message.
    /// </summary>
    /// <param name="text">The message format string.</param>
    /// <param name="args">Optional format arguments.</param>
    public static void Ordinal(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Ordinal, text, args);
    }

    /// <summary>
    /// Displays an appeal status message to draw user attention.
    /// </summary>
    /// <param name="text">The message format string.</param>
    /// <param name="args">Optional format arguments.</param>
    public static void Appeal(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Appeal, text, args);
    }

    /// <summary>
    /// Reports the result of an operation: shows info on success or error on failure.
    /// </summary>
    /// <typeparam name="T">The type of the result data.</typeparam>
    /// <param name="data">The result data to evaluate.</param>
    /// <param name="exceptionText">The error message to display if the data is default.</param>
    /// <param name="replacementWhenSuccess">Optional text to display instead of ToString on success.</param>
    /// <param name="isShowingToStringOnSuccess">Whether to display the data's ToString value on success.</param>
    public static void ResultWithException<T>(T data, string exceptionText, string? replacementWhenSuccess = null,
        bool isShowingToStringOnSuccess = false)
    {
        if (!EqualityComparer<T>.Default.Equals(data, default))
        {
            if (isShowingToStringOnSuccess)
                Info(data!.ToString()!);
            else if (replacementWhenSuccess is not null) Info(replacementWhenSuccess);
        }
        else
        {
            Error(exceptionText);
        }
    }
}
