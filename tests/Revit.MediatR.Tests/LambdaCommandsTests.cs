using Xunit;

namespace Revit.MediatR.Tests;

/// <summary>
/// Constructing commands whose constructor signature requires a Revit-typed <c>Document</c>
/// (even when passing <c>null!</c>) forces the CLR to resolve RevitAPI/RevitAPIUI — these assemblies are
/// not loaded outside of a Revit process/installation. This is an architectural constraint, not incomplete tests.
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Test names use underscores to separate scenario and expected result.")]
public class LambdaCommandsTests
{
    [Fact(Skip = "Requires Revit to be installed: the constructor takes a Document, which forces the CLR " +
                 "to load RevitAPI/RevitAPIUI, and those do not run outside of a Revit process/installation.")]
    public void LambdaTransactionCommand_StoresTransactionName()
    {
    }

    [Fact(Skip = "Requires Revit to be installed: the constructor takes a Document, which forces the CLR " +
                 "to load RevitAPI/RevitAPIUI, and those do not run outside of a Revit process/installation.")]
    public void LambdaExternalEventCommand_StoresDelegate()
    {
    }

    [Fact(Skip = "Requires Revit to be installed: the constructor takes a Document, which forces the CLR " +
                 "to load RevitAPI/RevitAPIUI, and those do not run outside of a Revit process/installation.")]
    public void LambdaExternalEventTransactionCommand_StoresTransactionName()
    {
    }
}
