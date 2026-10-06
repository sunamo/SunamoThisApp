namespace SunamoThisApp._sunamo.SunamoExceptions;

internal partial class ThrowEx
{
    internal static bool NotImplementedCase(object notImplementedName)
    {
        return ThrowIsNotNull(Exceptions.NotImplementedCase, notImplementedName);
    }

    internal static bool ThrowIsNotNull<TArgument>(Func<string, TArgument, string?> exceptionFactory, TArgument argument)
    {
        string? exceptionText = exceptionFactory(FullNameOfExecutedCode(), argument);
        return ThrowIsNotNull(exceptionText);
    }

    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

    static string FullNameOfExecutedCode(object type, string methodName, bool isFromThrowEx = false)
    {
        if (methodName is null)
        {
            int depth = 2;
            if (isFromThrowEx)
            {
                depth++;
            }

            methodName = Exceptions.CallingMethod(depth);
        }
        string typeFullName;
        if (type is Type typeFromCast)
        {
            typeFullName = typeFromCast.FullName ?? "Type could not be obtained from Type cast";
        }
        else if (type is MethodBase methodBase)
        {
            typeFullName = methodBase.ReflectedType?.FullName ?? "Type could not be obtained from MethodBase";
            methodName = methodBase.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString() ?? "Type could not be obtained from string";
        }
        else
        {
            Type resolvedType = type.GetType();
            typeFullName = resolvedType.FullName ?? "Type could not be obtained from GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    internal static bool ThrowIsNotNull(string? exceptionText, bool isReallyThrowing = true)
    {
        if (exceptionText is not null)
        {
            Debugger.Break();
            if (isReallyThrowing)
            {
                throw new Exception(exceptionText);
            }
            return true;
        }
        return false;
    }
}
