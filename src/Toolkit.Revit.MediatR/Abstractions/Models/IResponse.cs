using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Revit.MediatR.Abstractions.Models;

public interface IResponce
{
    bool HasError { get; set; }
    string Message { get; set; }
    Exception? Exception { get; set; }
}
