namespace SunamoThisApp._sunamo.SunamoExceptions;
// © www.sunamo.cz. All Rights Reserved.
internal sealed partial class Exceptions
{
    internal static Tuple<string, string, string> PlaceOfException(
bool fillAlsoFirstTwo = true)
    {
        StackTrace st = new();
        var v = st.ToString();
        var l = v.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        l.RemoveAt(0);
        var i = 0;
        string type = string.Empty;
        string methodName = string.Empty;
        for (; i < l.Count; i++)
        {
            var item = l[i];
            if (fillAlsoFirstTwo)
                if (!item.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(item, out type, out methodName);
                    fillAlsoFirstTwo = false;
                }
            if (item.StartsWith("at System."))
            {
                l.Add(string.Empty);
                l.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(type, methodName, string.Join(Environment.NewLine, l));
    }
    internal static void TypeAndMethodName(string l, out string type, out string methodName)
    {
        var s2 = l.Split("at ")[1].Trim();
        var s = s2.Split("(")[0];
        var p = s.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = p[^1];
        p.RemoveAt(p.Count - 1);
        type = string.Join(".", p);
    }
    internal static string CallingMethod(int v = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(v)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }

    internal static string? NotImplementedCase(string before, object notImplementedName)
    {
        var fr = string.Empty;
        if (notImplementedName != null)
        {
            fr = " for ";
            if (notImplementedName.GetType() == typeof(Type))
                fr += ((Type)notImplementedName).FullName;
            else
                fr += notImplementedName.ToString();
        }
        return CheckBefore(before) + "Not implemented case" + fr + " . internal program error. Please contact developer" +
        ".";
    }

    internal static string CheckBefore(string before)
    {
        return string.IsNullOrWhiteSpace(before) ? string.Empty : before + ": ";
    }
}
