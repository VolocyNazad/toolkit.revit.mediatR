namespace Revit.MediatR.Models;

/// <summary>
/// Универсальный результат обработки MediatR-команды: значение, флаг ошибки и (опционально) исключение.
/// </summary>
/// <typeparam name="TResult">Тип полезной нагрузки результата.</typeparam>
public sealed class Response<TResult>
{
    /// <summary>
    /// Значение, возвращённое обработчиком команды.
    /// </summary>
    public TResult? Result { get; set; }

    /// <summary>
    /// <see langword="true"/>, если при обработке команды произошла ошибка.
    /// </summary>
    public bool HasError { get; set; }

    /// <summary>
    /// Сообщение об ошибке или произвольный комментарий к результату.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Исключение, ставшее причиной ошибки, если оно было перехвачено.
    /// </summary>
    public Exception? Exception { get; set; }
}
