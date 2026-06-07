using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Revit.MediatR.Abstractions.Models;

namespace Toolkit.Revit.MediatR.Models;

public sealed class Response<TResult> : IResponce
{
    public TResult? Result { get; set; }
    public bool HasError { get; set; }
    public string Message { get; set; } = string.Empty;
    public Exception? Exception { get; set; }
}
