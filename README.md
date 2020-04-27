# PokerPal Backend

![C#](https://github.com/PokerPal/poker-pal-backend/workflows/C%23/badge.svg)

This repository contains code for the backend of the PokerPal application. The backend is an ASP.NET 
Core app written in C#.

## Building the Project

### Prerequisites

First, you'll need to install:

- [.NET Core 3.1](https://docs.microsoft.com/en-us/dotnet/core/install/sdk)
- [`dotnet-ef`](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet)

You also might want to install one of:

- [JetBrains Rider](https://www.jetbrains.com/rider/) (recommended)
- [Visual Studio](https://visualstudio.microsoft.com/)

### Building

You have a couple of options:

- Open the solution in Rider or Visual Studio and run the build action.
- Run `dotnet build` from the root of the repository.

### Running

Again, several options:

- Run one of the two launch profiles from within Rider or Visual Studio; most of the time for 
  development you'll want the Development profile.
- Manually run `dotnet run --launch-profile "Development"`
- Manually run `dotnet run --launch-profile "Staging"`

