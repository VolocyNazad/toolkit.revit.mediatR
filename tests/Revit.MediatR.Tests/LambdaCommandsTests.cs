using Xunit;

namespace Revit.MediatR.Tests;

/// <summary>
/// Конструирование команд, требующее Revit-типизированный <c>Document</c> в сигнатуре конструктора
/// (даже при передаче <c>null!</c>), форсирует у CLR резолв RevitAPI/RevitAPIUI — эти сборки не
/// загружаются вне процесса/установки Revit. Это архитектурное ограничение, а не недоделанные тесты.
/// </summary>
public class LambdaCommandsTests
{
    [Fact(Skip = "Требует установленного Revit: конструктор принимает Document, что заставляет CLR " +
                 "грузить RevitAPI/RevitAPIUI, а они не запускаются вне процесса/установки Revit.")]
    public void LambdaTransactionCommand_StoresTransactionName()
    {
    }

    [Fact(Skip = "Требует установленного Revit: конструктор принимает Document, что заставляет CLR " +
                 "грузить RevitAPI/RevitAPIUI, а они не запускаются вне процесса/установки Revit.")]
    public void LambdaExternalEventCommand_StoresDelegate()
    {
    }

    [Fact(Skip = "Требует установленного Revit: конструктор принимает Document, что заставляет CLR " +
                 "грузить RevitAPI/RevitAPIUI, а они не запускаются вне процесса/установки Revit.")]
    public void LambdaExternalEventTransactionCommand_StoresTransactionName()
    {
    }
}
