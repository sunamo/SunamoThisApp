namespace SunamoThisApp;


public class ResultWithExceptionTA<T>
{
    internal T Data { get; set; }
    /// <summary>
    ///     only string, because Message property isn't editable after instatiate
    ///     Usage: FubuCsprojFile
    /// </summary>
    internal string exc { get; set; }
    internal ResultWithExceptionTA(T data)
    {
        Data = data;
    }
    internal ResultWithExceptionTA(string exc)
    {
        this.exc = exc;
    }
    internal ResultWithExceptionTA(Exception exc)
    {
        this.exc = Exceptions.TextOfExceptions(exc);
    }
    /// <summary>
    /// Pro případ že data josu string což je typ i exception
    /// </summary>
    internal ResultWithExceptionTA()
    {
    }
}