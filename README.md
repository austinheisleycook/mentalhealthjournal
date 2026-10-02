# Mental Health Journal

A lightweight ASP.NET Core MVC application for tracking personal health and wellness entries. Users can log entries with a name, description, and date, then review previous entries in a journal-style list.

## Overview

Mental Health Journal is built with ASP.NET Core MVC and uses Entity Framework Core with SQL Server to persist journal entries. The application exposes a simple interface for:

- creating a new health entry
- saving the entry to a database
- listing prior entries in reverse chronological order
- viewing the current journal at the root health page

## Tech Stack

- ASP.NET Core MVC
- C#
- .NET 10
- Entity Framework Core
- SQL Server
- Bootstrap styling in Razor views

## Project Structure

```text
mentalhealthjournal/
├── healthjournal/
│   ├── Controllers/
│   │   ├── HealthController.cs
│   │   └── HomeController.cs
│   ├── Models/
│   │   ├── HealthConfig.cs
│   │   ├── HealthModel.cs
│   │   └── ErrorViewModel.cs
│   ├── Views/
│   │   ├── health/
│   │   │   ├── create.cshtml
│   │   │   └── index.cshtml
│   │   ├── Shared/
│   │   ├── _ViewImports.cshtml
│   │   └── _ViewStart.cshtml
│   ├── Program.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── healthjournal.csproj
│   └── wwwroot/
├── README.md
└── .gitignore
```

## Data Model

The core entity is `HealthModel`:

```csharp
public class HealthModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
}
```

This model is exposed through `HealthConfig`, which inherits from `DbContext` and defines the `HealthModels` table.

## Features

### Journal Dashboard

The `HealthController.Index()` action retrieves all health journal entries, orders them by most recent date first, and renders them in the `Views/health/index.cshtml` page.

### Entry Creation

The `HealthController.Create()` actions support both GET and POST requests. The form in `Views/health/create.cshtml` allows a user to enter:

- entry name
- description
- date

On submission, the entry is saved to the database and the user is redirected back to the journal index.

## Configuration

The application uses SQL Server via a connection string in `healthjournal/appsettings.json`.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=healthjournal;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

The application also automatically applies pending EF Core migrations at startup in `Program.cs`.

## Local Development

### Prerequisites

- .NET 10 SDK
- SQL Server or LocalDB
- A configured `DefaultConnection` connection string

### Run the project

```bash
dotnet restore
dotnet build
dotnet run
```

Then open the app in a browser, typically at:

```text
https://localhost:5001/
```

or the local port assigned by the ASP.NET Core development server.

## IIS Deployment

This application can be deployed to IIS on a Windows server, but it must be configured to access a real SQL Server instance. The app does not work reliably with LocalDB in a hosted IIS environment.

### Prerequisites

- Windows Server with IIS enabled
- .NET 10 Hosting Bundle installed
- SQL Server or Azure SQL available to the server
- A valid HTTPS certificate for the site

### Publish the app

```bash
dotnet publish -c Release -o C:\Publish\mentalhealthjournal
```

### Configure IIS

1. Open IIS Manager.
2. Create a new site or application pool.
3. Set the physical path to `C:\Publish\mentalhealthjournal`.
4. Use an application pool with:
   - Managed pipeline: Integrated
   - .NET CLR Version: No Managed Code
5. Bind the site to HTTPS and assign a certificate.

### Update the connection string

Set the connection string to a production SQL Server value:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SQL_SERVER;Database=mentalhealthjournal;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
  }
}
```

### Important server requirements

- The application pool identity needs permission to access the publish folder.
- The SQL Server must accept connections from the IIS server.
- The app will run `Database.Migrate()` on startup, so the database must exist and be accessible.

### Troubleshooting IIS

Common issues include:

- missing .NET 10 Hosting Bundle
- SQL Server firewall or authentication problems
- incorrect connection string or database permissions
- invalid HTTPS binding

Check the Windows Event Viewer and ASP.NET Core logs if the site fails to start.

## Default Route

The app is configured with the following default route:

```csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Health}/{action=Index}/{id?}");
```

This means the app opens on the health journal index by default.

## Notes

- The project appears to be a simple personal journaling and wellness tracking app rather than a broader clinical or mental-health assessment platform.
- This repository currently contains the baseline MVC app and persistence layer; it can be expanded with editing, deletion, authentication, or richer wellness tracking features.

## License

This project does not currently include a license file. If you plan to share or distribute it publicly, consider adding an appropriate open-source license.
