namespace SunamoThisApp;

public class ThisApp
{
    // The solution-level name of the application. In selling it is without spaces.
    // Name = Solution, Project = Project.
    public static string Name { get; set; } = null!;

    public static bool UseShortAsDateTime { get; set; } = true;

    public static bool RunInDebug { get; set; } = true;

    public static Func<string, string> FromKey { get; set; } = null!;

    public static bool Check { get; set; }

    private static string? project;

    public static readonly bool Initialized = false;

    public static string Namespace { get; set; } = "";

    private static string? eventLogName;

    public static dynamic Resources { get; set; } = null!;

    // Name = Solution, Project = Project.
    public static string Project
    {
        get
        {
            if (project is null) return Name;
            return project;
        }
        set => project = value;
    }

    public static string UnderscoreName => "_" + Name;

    // The event log name is automatically truncated to 8 characters if longer.
    public static string? EventLogName
    {
        get => eventLogName;
        set => eventLogName = string.IsNullOrEmpty(value) ? null : SHSubstring.SubstringIfAvailable(value, 8);
    }

    public static void SetName(string name)
    {
        Name = name;
    }

    public static void SetStatusXlf(TypeOfMessageTA typeOfMessage, string key)
    {
        SetStatus(typeOfMessage, FromKey(key));
    }

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

    public static void StatusFromText(string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            var typeOfMessage = StatusHelperSunamo.IsStatusMessage(ref text);
            SetStatus(typeOfMessage, text);
        }
    }

    public static void Success(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Success, text, args);
    }

    public static void Info(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Information, text, args);
    }

    public static void Error(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Error, text, args);
    }

    public static void Warning(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Warning, text, args);
    }

    public static void Ordinal(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Ordinal, text, args);
    }

    public static void Appeal(string text, params string[] args)
    {
        SetStatus(TypeOfMessageTA.Appeal, text, args);
    }

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
