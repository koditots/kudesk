# Kudesk

A production-ready desktop POS (Point of Sale) application built with Avalonia, .NET 9, and SQLite.

## Features

- Offline-first SQLite storage
- Windows and macOS support
- Auto-update via Velopack
- Clean architecture

## Installation

### Windows
Download `KudeskSetup.exe` from the [latest release](https://github.com/YOUR_USERNAME/Kudesk/releases) and run the installer.

### macOS
Download `Kudesk.dmg` from the [latest release](https://github.com/YOUR_USERNAME/Kudesk/releases), open it, and drag Kudesk to your Applications folder.

## Auto-Update

Kudesk automatically checks for updates on startup when connected to the internet. When a new version is available, it downloads and installs it automatically, then restarts to apply the update.

## Development

```bash
# Restore dependencies
dotnet restore

# Build
dotnet build src/Kudesk.App -c Release

# Run
dotnet run --project src/Kudesk.App
```

## License

MIT