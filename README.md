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

### Environment Variables

Non-sensitive environment variables are set in `Api/Properties/launchSettings.json`.

To set sensitive environment variables for development, such as database connection strings or API 
keys, create a file `Api/secrets.json` containing the keys and values, in the following format 
(replace angle bracketed placeholders with real values).

```json
{
  "DATABASE_URL": "<connection string>"
}
```

> `DATABASE_URL` may not be needed, e.g. if running in development configuration (in which case 
> `USE_IN_MEMORY_DATABASE` is set to `TRUE` in `Api/Properties/launchSettings.json`).

This file will be automatically read by the API project on startup.

**Important**: Make sure nothing is changed that causes this file to be checked in to version 
control. It is ignored in `.gitignore`, but if it's renamed or moved, or `.gitignore` is modified, 
that may no longer be true.

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

### Migrations

To check previously applied migrations for the production database:

```bash
dotnet run --project Migrations/Migrations.csproj -- check \
  "$(heroku config:get DATABASE_URL -a poker-pal-backend)?ssl=require"
```

To apply any pending migrations:

```bash
dotnet run --project Migrations/Migrations.csproj -- migrate \
  "$(heroku config:get DATABASE_URL -a poker-pal-backend)?ssl=require"
```

