using System.Diagnostics.CodeAnalysis;

namespace Revit.MediatR;

internal static class Guard
{
    public static void ThrowIfNull([NotNull] object? argument, string parameterName)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(argument, parameterName);
#else
        if (argument is null) throw new ArgumentNullException(parameterName);
#endif
    }
}
