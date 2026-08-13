using Xunit;

namespace Revit.MediatR.Tests;

/// <summary>
/// <see cref="Registrator.AddMediatR"/> вызывает <c>AddEvents()</c> из <c>VolocyNazad.Revit.Events</c>,
/// который уже на этапе регистрации (а не только при использовании) обращается к
/// <c>RevitAPIUI.dll</c> — вне процесса/установки Revit эта сборка не загружается
/// (<see cref="System.IO.FileNotFoundException"/>). Поэтому DI-регистрацию из этого пакета
/// невозможно юнит-тестировать без реального Revit — это архитектурное ограничение,
/// а не недоделанные тесты.
/// </summary>
public class RegistratorTests
{
    [Fact(Skip = "Требует установленного Revit: AddMediatR() -> AddEvents() грузит RevitAPIUI.dll " +
                 "уже при регистрации в контейнере, а она не загружается вне процесса/установки Revit.")]
    public void AddMediatR_RegistersMediator()
    {
    }

    [Fact(Skip = "Требует установленного Revit: AddMediatR() -> AddEvents() грузит RevitAPIUI.dll " +
                 "уже при регистрации в контейнере, а она не загружается вне процесса/установки Revit.")]
    public void AddMediatR_RegistersLambdaTransactionCommandHandler()
    {
    }

    [Fact(Skip = "Требует установленного Revit: AddMediatR() -> AddEvents() грузит RevitAPIUI.dll " +
                 "уже при регистрации в контейнере, а она не загружается вне процесса/установки Revit.")]
    public void AddMediatR_RegistersLambdaExternalEventCommandHandler()
    {
    }

    [Fact(Skip = "Требует установленного Revit: AddMediatR() -> AddEvents() грузит RevitAPIUI.dll " +
                 "уже при регистрации в контейнере, а она не загружается вне процесса/установки Revit.")]
    public void AddMediatR_RegistersLambdaExternalEventTransactionCommandHandler()
    {
    }
}
