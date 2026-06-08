namespace Toolkit.Revit.MediatR.Models;

public sealed class Response<TResult>
{
    public TResult? Result { get; set; }
    public bool HasError { get; set; }
    public string Message { get; set; } = string.Empty;
    public Exception? Exception { get; set; }
}
