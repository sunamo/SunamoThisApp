namespace SunamoThisApp;

public class ThisApp
{
    /// <summary>
    ///     Name = Solution
    ///     Project = Project
    ///     In selling is without spaces
    /// </summary>
    public static string Name;

    //public static Langs l = Langs.en;
    public static bool useShortAsDt = true;
    public static bool runInDebug = true;

    /// <summary>
    ///     sess.i18n
    /// </summary>
    public static Func<string, string> i18n;

    // Everywhere is used just ThisApp.cd. 
    //public static Dispatcher cd = null;
    //public static DispatcherPriority cdp = DispatcherPriority.Normal;
    /*
     * Nemůže tu být
     * Pokud si chci vybrat zda má SunamoLogger dědit od SunamoThisApp nebo naopak
     * tak je logičtější první možnost
     *
     * dočasně to musím zakomentovat než vyřeším zbytek možností
     */
    //        public static TypedLoggerBase NopeOrDebugTyped()
    //        {
    //#if DEBUG2
    //                    return TypedDebugLogger.Instance;
    //#elif !DEBUG2
    //            // Is possible also use CmdApp.ConsoleOrDebugTyped
    //            return TypedDummyLogger.Instance;
    //            //return TypedConsoleLogger.Instance;
    //#endif
    //        }

    public static bool check = false;


    private static string project;


    public static readonly bool initialized = false;
    public static string Namespace = "";

    private static string eventLogName;

    public static /*ResourcesHelper*/ dynamic Resources;

    /// <summary>
    ///     Name = Solution
    ///     Project = Project
    /// </summary>
    public static string Project
    {
        get
        {
            if (project == null) return Name;
            return project;
        }
        set => project = value;
    }

    public static string _Name => "_" + Name;

    /// <summary>
    ///     může být null, pak se EL nebude využívat
    /// </summary>
    public static string EventLogName
    {
        get => eventLogName;
        set => eventLogName = string.IsNullOrEmpty(value) ? null : SHSubstring.SubstringIfAvailable(value, 8);
    }

    public static void SetName(string name)
    {
        Name = name;
    }


    public static void SetStatusXlf(TypeOfMessageTA st, string key)
    {
        SetStatus(st, i18n(key));
    }

    //public static event SetStatusDelegate StatusSetted;

    public static void SetStatus(TypeOfMessageTA st, string status, params string[] args)
    {
        var format = string.Format(status, args).Trim();
        if (format != string.Empty)
        {
            // Dříve pokud bylo StatusSetted null tak vypisovalo do Debugu. Možná by se dalo detekovat že je to UT https://g.co/gemini/share/b99264f846d4 tímto. 
            // Ale možná UT umí sami odchytávat výstup do konzole, takže budu zapisovat jen tam. 

            Console.WriteLine(format);
        }
    }

    public static void StatusFromText(string v)
    {
        if (!string.IsNullOrEmpty(v))
        {
            var tom = StatusHelperSunamo.IsStatusMessage(ref v);
            SetStatus(tom, v);
        }
    }

    /// <summary>
    ///     Strings which is on lines calling this method is not translate
    ///     Debug method when I running app on release and app is behave extraordinary
    /// </summary>
    /// <param name="v"></param>
    /// <param name="o"></param>
    public static void a(string v, params string[] o)
    {
        Appeal(v, o);
    }

    public static void Success(string v, params string[] o)
    {
        SetStatus(TypeOfMessageTA.Success, v, o);
    }

    public static void Info(string v, params string[] o)
    {
        SetStatus(TypeOfMessageTA.Information, v, o);
    }

    public static void Error(string v, params string[] o)
    {
        SetStatus(TypeOfMessageTA.Error, v, o);
    }

    public static void Warning(string v, params string[] o)
    {
        SetStatus(TypeOfMessageTA.Warning, v, o);
    }

    public static void Ordinal(string v, params string[] o)
    {
        SetStatus(TypeOfMessageTA.Ordinal, v, o);
    }

    public static void Appeal(string v, params string[] o)
    {
        SetStatus(TypeOfMessageTA.Appeal, v, o);
    }

    public static void ResultWithException<T>(T Data, string exc, string replacementWhenSuccess = null,
        bool showToStringWhenSuccess = false)
    {
        if (!EqualityComparer<T>.Default.Equals(Data, default))
        {
            if (showToStringWhenSuccess)
                Info(Data.ToString());
            else if (replacementWhenSuccess != null) Info(replacementWhenSuccess);
        }
        else
        {
            Error(exc);
        }
    }
}