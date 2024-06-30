namespace SunamoThisApp;


public class ResultWithExceptionTA<T>
{
    public T Data { get; set; }
    
    
    
    
    public string exc { get; set; }
    public ResultWithExceptionTA(T data)
    {
        Data = data;
    }
    public ResultWithExceptionTA(string exc)
    {
        this.exc = exc;
    }
    public ResultWithExceptionTA(Exception exc)
    {
        this.exc = Exceptions.TextOfExceptions(exc);
    }
    
    
    
    public ResultWithExceptionTA()
    {
    }
}