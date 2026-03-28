namespace SunamoThisApp._sunamo.SunamoExceptions;

/// <summary>
/// Provides methods that throw exceptions for common error conditions.
/// </summary>
internal partial class ThrowEx
{
    /// <summary>
    /// Throws an exception for a not-implemented case with the given name.
    /// </summary>
    /// <param name="notImplementedName">The name or value of the unimplemented case.</param>
    /// <returns><c>true</c> if an exception was generated; otherwise <c>false</c>.</returns>
    internal static bool NotImplementedCase(object notImplementedName)
    {
        return ThrowIsNotNull(Exceptions.NotImplementedCase, notImplementedName);
    }

    /// <summary>
    /// Invokes the exception factory with the current execution context and throws if the result is not null.
    /// </summary>
    /// <typeparam name="TArgument">The type of the argument passed to the exception factory.</typeparam>
    /// <param name="exceptionFactory">A function that creates an exception message from a context string and argument.</param>
    /// <param name="argument">The argument to pass to the exception factory.</param>
    /// <returns><c>true</c> if an exception was generated; otherwise <c>false</c>.</returns>
    internal static bool ThrowIsNotNull<TArgument>(Func<string, TArgument, string?> exceptionFactory, TArgument argument)
    {
        string? exceptionText = exceptionFactory(FullNameOfExecutedCode(), argument);
        return ThrowIsNotNull(exceptionText);
    }

    /// <summary>
    /// Returns the fully qualified name of the currently executed code (type.method).
    /// </summary>
    /// <returns>A string in the format "Namespace.Type.Method".</returns>
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

    /// <summary>
    /// Returns the fully qualified name from the given type and method name.
    /// </summary>
    /// <param name="type">The type object, Type instance, MethodBase, or string representing the type.</param>
    /// <param name="methodName">The method name. If null, it will be resolved from the call stack.</param>
    /// <param name="isFromThrowEx">Whether the call originates from ThrowEx, affecting stack depth calculation.</param>
    /// <returns>A string in the format "TypeFullName.MethodName".</returns>
    static string FullNameOfExecutedCode(object type, string methodName, bool isFromThrowEx = false)
    {
        if (methodName == null)
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

    /// <summary>
    /// Throws an exception if the exception text is not null.
    /// </summary>
    /// <param name="exceptionText">The exception message. If not null, an exception is thrown.</param>
    /// <param name="isReallyThrowing">Whether to actually throw the exception or just return the result.</param>
    /// <returns><c>true</c> if the exception text was not null; otherwise <c>false</c>.</returns>
    internal static bool ThrowIsNotNull(string? exceptionText, bool isReallyThrowing = true)
    {
        if (exceptionText != null)
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
