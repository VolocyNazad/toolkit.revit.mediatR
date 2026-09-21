using Xunit;

namespace Revit.MediatR.Tests;

/// <summary>
/// <see cref="Registrator.AddMediatR"/> calls <c>AddEvents()</c> from <c>VolocyNazad.Revit.Events</c>,
/// which reaches into <c>RevitAPIUI.dll</c> already at registration time (not only when used) —
/// outside of a Revit process/installation this assembly is not loaded
/// (<see cref="System.IO.FileNotFoundException"/>). Because of this, DI registration from this package
/// cannot be unit-tested without a real Revit installation — this is an architectural constraint,
/// not incomplete tests.
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Test names use underscores to separate scenario and expected result.")]
public class RegistratorTests
{
    [Fact(Skip = "Requires Revit to be installed: AddMediatR() -> AddEvents() loads RevitAPIUI.dll " +
                 "already at container registration time, and it is not loaded outside of a Revit process/installation.")]
    public void AddMediatR_RegistersMediator()
    {
    }

    [Fact(Skip = "Requires Revit to be installed: AddMediatR() -> AddEvents() loads RevitAPIUI.dll " +
                 "already at container registration time, and it is not loaded outside of a Revit process/installation.")]
    public void AddMediatR_RegistersLambdaTransactionCommandHandler()
    {
    }

    [Fact(Skip = "Requires Revit to be installed: AddMediatR() -> AddEvents() loads RevitAPIUI.dll " +
                 "already at container registration time, and it is not loaded outside of a Revit process/installation.")]
    public void AddMediatR_RegistersLambdaExternalEventCommandHandler()
    {
    }

    [Fact(Skip = "Requires Revit to be installed: AddMediatR() -> AddEvents() loads RevitAPIUI.dll " +
                 "already at container registration time, and it is not loaded outside of a Revit process/installation.")]
    public void AddMediatR_RegistersLambdaExternalEventTransactionCommandHandler()
    {
    }
}
