namespace Revit.MediatR.Models;

/// <summary>
/// The universal result of processing a MediatR command: a value, an error flag, and (optionally) an exception.
/// </summary>
/// <typeparam name="TResult">The type of the result payload.</typeparam>
public sealed class Response<TResult>
{
    /// <summary>
    /// The value returned by the command handler.
    /// </summary>
    public TResult? Result { get; set; }

    /// <summary>
    /// <see langword="true"/> if an error occurred while processing the command.
    /// </summary>
    public bool HasError { get; set; }

    /// <summary>
    /// The error message, or an arbitrary comment on the result.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// The exception that caused the error, if one was caught.
    /// </summary>
    public Exception? Exception { get; set; }
}
