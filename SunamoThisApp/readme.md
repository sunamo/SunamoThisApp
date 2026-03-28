# SunamoThisApp

One of the base foundations for the Sunamo application platform, providing centralized application configuration, status reporting, and console logging utilities.

## Features

- **ThisApp** - Central static class for application-wide configuration (name, project, namespace, event log) and status message dispatching
- **StatusHelperSunamo** - Parses status message prefixes (error:, warning:, success:, info:, information:, appeal:) to determine message types
- **TypeOfMessageTA** - Enum defining message types: Error, Warning, Information, Ordinal, Appeal, Success
- **CL** - Console logging helper that writes color-coded messages based on message type

## Installation

```bash
dotnet add package SunamoThisApp
```

## Usage

```csharp
using SunamoThisApp;

// Set up application name
ThisApp.SetName("MyApplication");

// Display status messages
ThisApp.Info("Application started");
ThisApp.Success("Operation completed successfully");
ThisApp.Warning("Low disk space");
ThisApp.Error("Connection failed");

// Parse status text with prefix
ThisApp.StatusFromText("error:Something went wrong");
```

## Links

- [NuGet](https://www.nuget.org/profiles/sunamo)
- [GitHub](https://github.com/sunamo/PlatformIndependentNuGetPackages)
- [Developer site](https://sunamo.cz)

## Target Frameworks

`net10.0;net9.0;net8.0`

## License

MIT

## Contact

Request for new features / bug report: [Mail](mailto:radek.jancik@sunamo.cz) or on GitHub
