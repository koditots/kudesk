# Kudesk

A production-ready desktop POS (Point of Sale) application built with Avalonia, .NET 9, and SQLite.

## Features

- Offline-first SQLite storage
- Windows and macOS support
- Auto-update via Velopack
- Clean architecture (Core, Infrastructure, App layers)

## Installation

Download installers from the [Releases](https://github.com/koditots/kudesk/releases) page:

- **Windows**: `Kudesk-win-Setup.exe`
- **macOS**: `Kudesk-win-Setup.exe` (run via Wine/CrossOver or build on macOS)

## Auto-Update

Kudesk automatically checks for updates on startup when connected to the internet. When a new version is available, it downloads and installs it automatically, then restarts to apply the update.

## Development

```bash
# Restore dependencies
dotnet restore

# Build for Windows
dotnet publish src/Kudesk.App -c Release -r win-x64 --self-contained true -o publish/win

# Build for macOS (on macOS)
dotnet publish src/Kudesk.App -c Release -r osx-x64 --self-contained true -o publish/osx

# Package with Velopack
vpk pack -u Kudesk -v 1.0.0 -p publish/win -o releases/win
```

## Creating a Release

1. Push a version tag: `git tag v1.0.0 && git push origin v1.0.0`
2. The GitHub Actions workflow will build and create the release automatically
3. Or manually upload `releases/win/Kudesk-win-Setup.exe` to the GitHub Release

## License

MIT