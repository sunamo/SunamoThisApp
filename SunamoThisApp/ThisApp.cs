namespace SunamoThisApp;

/// <summary>
/// Represents the this app type.
/// </summary>
public class ThisApp
{
    // The solution-level name of the application. In selling it is without spaces.
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public static string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the use short as date time.
    /// </summary>
    public static bool UseShortAsDateTime { get; set; } = true;

    /// <summary>
    /// Gets or sets the run in debug.
    /// </summary>
    public static bool RunInDebug { get; set; } = true;

    /// <summary>
    /// Gets or sets the from key.
    /// </summary>
    public static Func<string, string> FromKey { get; set; } = null!;

    /// <summary>
    /// Gets or sets the check.
    /// </summary>
    public static bool Check { get; set; }

    private static string? project;

    /// <summary>
    /// The initialized.
    /// </summary>
    public static readonly bool Initialized = false;

    /// <summary>
    /// Gets or sets the namespace.
    /// </summary>
    public static string Namespace { get; set; } = "";

    private static string? eventLogName;

    /// <summary>
    /// Gets or sets the resources.
    /// </summary>
    public static dynamic Resources { get; set; } = null!;

    /// <summary>
    /// The project.
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
    /// Gets the underscore name.
    /// </summary>
    public static string UnderscoreName => "_" + Name;

    // The event log name is automatically truncated to 8 characters if longer.
    /// <summary>
    /// The event log name.
    /// </summary>
    public static string? EventLogName
    {
        get => eventLogName;
        set => eventLogName = string.IsNullOrEmpty(value) ? null : SHSubstring.SubstringIfAvailable(value, 8);
    }

    /// <summary>
    /// Set name.
    /// </summary>
    /// <param name="name">The name.</param>
    public static void SetName(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Set status xlf.
    /// </summary>
    /// <param name="null">The null.</param>
    /// <returns>The set status xlf result.</returns>
    public static void SetStatusXlf(TypeOfMessageTA typeOfMessage, string key)
    {
        SetStatus(typeOfMessage, FromKey(key));
    }

    /// <summary>
    /// Set status.
    /// </summary>
    /// <param name="null">The null.</param>
    /// <returns>The set status result.</returns>
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
    /// Status from text.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="args">The args.</param>
    /// <returns>The status from text result.</returns>
    public static void StatusFromText(string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            var typeOfMessage = StatusHelperSunamo.IsStatusMessage(ref text);
            SetStatus(typeOfMessage, text);
        }
    }

    /// <summary>
    /// Success.
    /// </summary>
    /// <param name="formattedText">The formatted text.</param>
    /// <returns>The success result.</returns>
    public static void Success(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Success, text, args);
    }

    /// <summary>
    /// Info.
    /// </summary>
    /// <param name="formattedText">The formatted text.</param>
    /// <returns>The info result.</returns>
    public static void Info(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Information, text, args);
    }

    /// <summary>
    /// Initializes a new instance of the error class.
    /// </summary>
    /// <param name="formattedText">The formatted text.</param>
    public static void Error(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Error, text, args);
    }

    /// <summary>
    /// Warning.
    /// </summary>
    /// <param name="typeOfMessage">The type of message.</param>
    /// <returns>The warning result.</returns>
    public static void Warning(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Warning, text, args);
    }

    /// <summary>
    /// Initializes a new instance of the ordinal class.
    /// </summary>
    /// <param name="formattedText">The formatted text.</param>
    public static void Ordinal(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Ordinal, text, args);
    }

    /// <summary>
    /// Appeal.
    /// </summary>
    /// <param name="!string.IsNullOrEmpty(text)">The string is null or empty text.</param>
    /// <returns>The appeal result.</returns>
    public static void Appeal(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Appeal, text, args);
    }

    /// <summary>
    /// Result with exception t.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="args">The args.</param>
    /// <returns>The result with exception t result.</returns>
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
