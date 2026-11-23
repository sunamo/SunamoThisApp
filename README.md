# SunamoThisApp

Application identity and colored status messages for console and UI applications of the sunamo platform.

## Features

- **ThisApp** - static class holding the application identity (name, project, event log name) and the single entry point for status messages.
- **ThisApp.StatusSet** - event raised for every message, so a UI application can show it (for example in a status bar).
- **StatusHelperSunamo** - recognizes the type of a message from its prefix (`error:`, `warning:`, `success:`, `info:`, `information:`, `appeal:`), regardless of letter case.
- **TypeOfMessageTA** - type of a message: Error, Warning, Information, Ordinal, Appeal, Success.

## Installation

```bash
dotnet add package SunamoThisApp
```

## Usage

```csharp
using SunamoThisApp;
using SunamoThisApp._public.SunamoEnums.Enums;

ThisApp.SetName("MyApplication");

// Each message is written once, in the color of its type.
ThisApp.Info("Application started");
ThisApp.Success("Copied {0} of {1} files", "3", "3");
ThisApp.Warning("Low disk space");

// Errors go to the standard error stream.
ThisApp.Error("Connection failed");

// The prefix decides the type of the message.
ThisApp.StatusFromText("error:Something went wrong");

// UI applications subscribe to the event instead of reading the console.
ThisApp.StatusSet += (typeOfMessage, message) => statusBar.Text = message;
```

The text is a composite format string only when arguments are passed, so a message with braces (for example a piece of JSON) is written as is.

`ThisApp.Name` is `null` until the application sets it.

## Target frameworks

`net10.0`, `net9.0`, `net8.0`, `netstandard2.0`

## Links

- [NuGet](https://www.nuget.org/packages/SunamoThisApp)
- [GitHub](https://github.com/sunamo/SunamoThisApp)
- [Developer site](https://sunamo.cz)

## License

MIT

## Contact

Request for new features / bug report: [Mail](mailto:radek@sunamo.cz) or on GitHub

<!-- sunamo:test-coverage:start -->
## Test coverage

| Metric | Value |
|---|---|
| Method | static |
| Test projects | 1 |
| Public types referenced by tests | 0/3 (0.0 %) |
| Public methods referenced by tests | 0/13 (0.0 %) |
| Line coverage | n/a |
| Branch coverage | n/a |
| Generated (UTC) | 2026-11-22 21:24 |

<!-- sunamo:test-coverage:data {"package":"SunamoThisApp","container":"pinp","testProjects":1,"publicTypes":3,"typesReferencedByTests":0,"publicMethods":13,"methodsReferencedByTests":0,"lineRate":null,"branchRate":null,"method":"static","generatedUtc":"2026-11-22 21:24"} -->
<!-- sunamo:test-coverage:end -->
