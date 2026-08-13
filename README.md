# Revit.MediatR

[![Revit 2021-2027](https://img.shields.io/badge/Revit-2021–2027-green.svg)](https://autodesk.com/revit)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![VolocyNazad](https://img.shields.io/badge/VolocyNazad-blue.svg)](https://github.com/VolocyNazad)

> Реализации сервисов пайплайнов MediatR для Revit API.

Revit.MediatR — это набор сервисов реализации паттерна пайплайн для Revit API с поддержкой
DI-контейнеризации: команды и обработчики, которые умеют переносить выполнение в поток Revit API
через `ExternalEvent` ([VolocyNazad.Revit.Events](https://github.com/VolocyNazad/toolkit.revit.events))
и оборачивать его в именованную транзакцию — без ручного написания `IExternalEventHandler` и
`Transaction` в каждом месте вызова.

## Установка

```
dotnet add package VolocyNazad.Revit.MediatR
```

Версия пакета привязана к году Revit: `{RevitYear}.0.{patch}` (например, `2025.0.7`). Собирается
под конкретную версию Revit в зависимости от выбранной конфигурации сборки — так же, как и
`Revit_All_Main_Versions_API_x64`, от которого зависит проект.

## Регистрация в DI

```csharp
using Revit.MediatR.DI;

services.AddMediatR();
```

Вызов регистрирует `VolocyNazad.Revit.Events` (`AddEvents`) и `VolocyNazad.Revit.Context`
(`AddRevitContext`), затем сам MediatR вместе с обработчиками встроенных lambda-команд и
Revit-специфичными поведениями конвейера (`RevitTransactionBehaviour`,
`RevitExternalEventBehaviour`), которые автоматически открывают транзакцию и/или переносят
выполнение в поток Revit API.

## Встроенные команды

| Команда | Транзакция | ExternalEvent |
|---|---|---|
| `LambdaTransactionCommand` | да | нет |
| `LambdaExternalEventCommand` | нет | да |
| `LambdaExternalEventTransactionCommand` | да | да |

Все выполняются в контексте документа (явно переданного или активного из `IRevitContext`) и
возвращают `Response<bool>` — обёртку с результатом, флагом ошибки, сообщением и исключением.

### Пример: транзакция на активном документе

```csharp
Response<bool> response = await mediator.Send(
    new LambdaTransactionCommand(
        document: null!, // берётся ActiveDocument из IRevitContext
        transactionName: "Переместить стену",
        @delegate: (doc, ct) =>
        {
            // изменения в модели
            return Task.FromResult(true);
        }));
```

### Пример: выполнение через ExternalEvent (например, из модального окна WPF)

```csharp
Response<bool> response = await mediator.Send(
    new LambdaExternalEventTransactionCommand(
        document: activeDocument,
        transactionName: "Создать уровень",
        @delegate: (doc, ct) =>
        {
            // код, которому обязательно нужен поток Revit API, внутри транзакции
            return Task.FromResult(true);
        }));
```

### Собственные команды

Свои команды можно завести, унаследовавшись от `Revit.MediatR.Base.DocumentCommand` и реализовав
маркерные интерфейсы `Revit.MediatR.Abstractions.Models.ITransactionCommand` и/или
`IExternalEventCommand`, а также написав для них `IRequestHandler<TCommand, Response<bool>>` —
поведения конвейера подхватят их автоматически по этим интерфейсам.

## Поддерживаемые версии Revit

Пакет собирается под все версии Revit из списка `Configurations` в
`src/Revit.MediatR/Revit.MediatR.csproj` (2011–2027), таргетируясь на `net48` или `net8.0-windows`
в зависимости от года — переключение и генерацию define-констант (`IS2025`, `BEFORE2021`,
`AFTER2021` и т.д.) берёт на себя общий MSBuild SDK
[VolocyNazad.Revit.Sdk](https://github.com/VolocyNazad/toolkit.revit.sdk).

## Разработка

- `dotnet build --configuration "Release_2025.0.0"` — сборка под конкретную версию Revit.
- `dotnet test --configuration "Release_2025.0.0"` — юнит-тесты (`tests/Revit.MediatR.Tests`).
  Тесты, которым для проверки нужны реальные Revit-типизированные объекты (`Document`), помечены
  `Skip` — вне процесса Revit сборки `RevitAPI`/`RevitAPIUI` не загружаются.
- Версия пакета автоверсионируется через [MinVer](https://github.com/adamralph/minver) по git-тегам
  вида `0.0.7`; чтобы выпустить новую версию, поставьте тег и запушьте его:
  `git tag 0.0.7 && git push origin main --follow-tags`.

## Лицензия

[MIT](LICENSE.md)
