# 🏗️ Day 1 Setup: Backend & Service Layer Prerequisites

## 🎯 Setup Overview

This setup ensures you have everything needed to successfully complete Day 1 of the Personal Expense Tracker .NET project. You'll verify .NET SDK, Entity Framework tools, IDE setup, and create the initial project structure.

**⏱️ Estimated Setup Time: 10-15 minutes**

---

## ✅ Prerequisites Checklist

### 📋 Required Software

**.NET SDK**

```bash
# Check .NET version (should be 10.0 or higher)
dotnet --version

# Expected output should show version 10.0.x or higher
# If not installed, download from: https://dotnet.microsoft.com/download
```

**Entity Framework Core Tools**

```bash
# Check EF Core tools version
dotnet ef --version

# If not installed:
dotnet tool install --global dotnet-ef

# Update if already installed:
dotnet tool update --global dotnet-ef
```

**IDE and Extensions**

- VS Code or Visual Studio 2022 installed
- GitHub Copilot extension enabled and authenticated
- C# Dev Kit (for VS Code) or C# extension
- .NET Extension Pack (for VS Code - optional but recommended)

---

## 🚀 Project Structure Setup

### 📁 Create Project Directory

```bash
# Navigate to your workspace
cd /path/to/your/workspace

# Create solution and project
dotnet new sln -n ExpenseTracker
dotnet new webapp -n ExpenseTracker.Web -o src/ExpenseTracker.Web
dotnet sln add src/ExpenseTracker.Web

# Navigate into project
cd src/ExpenseTracker.Web
```

### 🔧 Verify GitHub Copilot

Test Copilot functionality:

1. Open VS Code in the solution directory: `code .` (from root, not src/)
2. Create a new file: `Test.cs`
3. Type: `// Create a simple record for Expense with Id, Description, Amount, Date`
4. Press `Tab` to accept Copilot suggestion
5. Verify Copilot generates appropriate C# code

---

## 📦 NuGet Package Installation

### 🎯 Add Required Packages

**🤖 Use GitHub Copilot to identify required packages:**

```text
@workspace What NuGet packages do I need for an ASP.NET Core 10 app with:
- Entity Framework Core with SQLite
- FluentValidation for input validation
- OpenAPI/Swagger for API documentation
List the dotnet add package commands.
```

**Run these commands:**

```bash
# Navigate to project directory (if not already there)
cd src/ExpenseTracker.Web

# Entity Framework Core with SQLite
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools

# Validation
dotnet add package FluentValidation.AspNetCore

# OpenAPI/Swagger for API documentation
dotnet add package Microsoft.AspNetCore.OpenApi
dotnet add package Swashbuckle.AspNetCore
```

**🎯 Copilot Feature: `@terminal` for package commands**

```text
@terminal What is the command to add Entity Framework Core SQLite package to my .NET project?
```

### 📋 Verify Packages Installed

```bash
# List all packages
dotnet list package
```

---

<details>
<summary>✅ Click to verify: Expected ExpenseTracker.Web.csproj</summary>

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="FluentValidation.AspNetCore" Version="11.3.1" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="10.0.1">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="10.0.1" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="10.0.1">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="10.0.1" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="10.0.1" />
  </ItemGroup>

</Project>
```

</details>

---

## 🗄️ Database Setup

### 💾 SQLite Database Configuration

**🤖 Use GitHub Copilot to modify appsettings.json:**

1. Open `appsettings.json` in your editor
2. Open Copilot Chat (Ctrl+Shift+I / Cmd+Shift+I)
3. Use this prompt:

```text
#file:appsettings.json Add SQLite database connection string named "DefaultConnection" 
with database file "expensetracker.db". Also add Entity Framework Core 
logging at Information level for database commands.
```

**Alternative: Use `/edit` command:**

```text
/edit #file:appsettings.json Add ConnectionStrings section with DefaultConnection 
for SQLite database "expensetracker.db" and enable EF Core command logging
```

---

<details>
<summary>✅ Click to verify: Expected appsettings.json</summary>

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=expensetracker.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

</details>

---

### 🔒 Add Database to .gitignore

**🤖 Use GitHub Copilot to create .gitignore:**

```text
Create a .gitignore file for an ASP.NET Core project 
with SQLite database. Include: database files (*.db, *.db-shm, *.db-wal), 
build artifacts, IDE folders (.vscode, .idea), but keep .vscode/tasks.json 
and launch.json.
```

---

<details>
<summary>✅ Click to verify: Expected .gitignore</summary>

```gitignore
# Database files
*.db
*.db-shm
*.db-wal

# User-specific files
*.user
*.suo
*.userosscache

# Build results
bin/
obj/
[Dd]ebug/
[Rr]elease/

# VS Code
.vscode/
!.vscode/tasks.json
!.vscode/launch.json

# Rider
.idea/
```

</details>

---

## 📁 Project Structure Setup

### 🗂️ Create Directory Structure

```bash
# From src/ExpenseTracker.Web directory
mkdir Models
mkdir Data
mkdir Services
mkdir Validators
mkdir DTOs
```

### 📂 Expected Structure

```text
ExpenseTracker/                  # Solution root
├── ExpenseTracker.sln
├── src/
│   └── ExpenseTracker.Web/
│       ├── Models/              # EF Core entities
│       ├── Data/                # DbContext
│       ├── Services/            # Business logic
│       ├── Validators/          # FluentValidation
│       ├── DTOs/                # Data Transfer Objects
│       ├── Pages/               # Razor Pages
│       ├── wwwroot/            # Static files
│       ├── Program.cs
│       ├── appsettings.json
│       └── ExpenseTracker.Web.csproj
└── tests/                       # Will be created later
```

---

## ✅ Setup Verification

### 🧪 Test Basic Setup

**🤖 Use GitHub Copilot to add a health check endpoint:**

1. Open `Program.cs`
2. Place cursor before `app.Run();`
3. Use inline comment prompt:

```csharp
// Add a minimal API health check endpoint at /health that returns JSON with Status and Timestamp
```

4. Press `Tab` to accept Copilot's suggestion

**Alternative: Use Copilot Chat with context:**

```text
#file:Program.cs Add a minimal API health check endpoint at /health 
that returns a HealthResponse record with Status="Healthy" and current UTC timestamp.
```

---

<details>
<summary>✅ Click to verify: Expected health check code</summary>

```csharp
// Add HealthResponse record at the end of Program.cs
public record HealthResponse(string Status, DateTime Timestamp);

// Add this before app.Run();
app.MapGet("/health", () => Results.Ok(new HealthResponse("Healthy", DateTime.UtcNow)))
    .WithName("Health")
    .Produces<HealthResponse>(StatusCodes.Status200OK, "application/json");
```

</details>

### 🚀 Run Verification

```bash
# Build the project
dotnet build

# Run the application
dotnet run

# Expected output (port may vary based on launchSettings.json):
# info: Microsoft.Hosting.Lifetime[14]
#       Now listening on: http://localhost:<port>
# info: Microsoft.Hosting.Lifetime[0]
#       Application started. Press Ctrl+C to shut down.

# In another terminal, test the health endpoint (use the port shown in console):
curl http://localhost:<port>/health

# Expected response:
# {"status":"Healthy","timestamp":"2025-12-07T..."}
```

> **📝 Note**: The port number is configured in `Properties/launchSettings.json`. 
> Check the console output for the actual port your application is using.

---

## 🌐 Configure Launch Settings & HTTPS

### 📝 Configure launchSettings.json

**🤖 Use GitHub Copilot to configure launch settings:**

```text
#file:Properties/launchSettings.json Configure launch profiles for ASP.NET Core:
- "http" profile on port 5000
- "https" profile on ports 5001 (HTTPS) and 5000 (HTTP)
- Set ASPNETCORE_ENVIRONMENT to Development
- Enable launch browser to swagger
```

---

<details>
<summary>✅ Click to verify: Expected Properties/launchSettings.json</summary>

```json
{
  "$schema": "http://json.schemastore.org/launchsettings.json",
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "swagger",
      "applicationUrl": "http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "swagger",
      "applicationUrl": "https://localhost:5001;http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

</details>

---

### 🔒 Disable HTTPS Redirection for Development

If you see the warning `Failed to determine the https port for redirect`, you can disable HTTPS redirection for local development.

**🤖 Use GitHub Copilot to update Program.cs:**

```text
#file:Program.cs Wrap app.UseHttpsRedirection() in a condition 
to only enable it in non-Development environments.
```

---

<details>
<summary>✅ Click to verify: Expected HTTPS configuration</summary>

```csharp
// In Program.cs, replace:
// app.UseHttpsRedirection();

// With:
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
```

</details>

---

## 🔧 IDE Configuration

### 📝 VS Code Settings

**🤖 Use GitHub Copilot to create VS Code settings:**

```text
Create .vscode/settings.json for ASP.NET Core C# development 
with: default solution "ExpenseTracker.slnx", Roslyn analyzers enabled, 
format on save for C#, organize imports on save.
```

---

<details>
<summary>✅ Click to verify: Expected .vscode/settings.json</summary>

```json
{
    "dotnet.defaultSolution": "ExpenseTracker.slnx",
    "omnisharp.enableRoslynAnalyzers": true,
    "omnisharp.enableEditorConfigSupport": true,
    "omnisharp.organizeImportsOnFormat": true,
    "[csharp]": {
        "editor.formatOnSave": true,
        "editor.codeActionsOnSave": {
            "source.organizeImports": "explicit"
        }
    }
}
```

</details>

---

### 🎨 GitHub Copilot Settings

**🤖 Use GitHub Copilot to configure itself:**

```text
Add to .vscode/settings.json: GitHub Copilot settings 
for C# development - enable for all except yaml/plaintext, 
set agent temperature to 0.1 for consistent suggestions.
```

---

<details>
<summary>✅ Click to verify: Expected Copilot settings</summary>

```json
{
    "github.copilot.enable": {
        "*": true,
        "yaml": false,
        "plaintext": false
    },
    "github.copilot.chat.agent.temperature": 0.1
}
```

</details>

---

### 🚀 VS Code Tasks (Optional)

**🤖 Use GitHub Copilot to create build tasks:**

```text
@workspace /newfile .vscode/tasks.json with two tasks:
1. "build" - runs dotnet build on ExpenseTracker.sln
2. "run" - runs dotnet run on src/ExpenseTracker.Web
Use $msCompile problem matcher.
```

---

<details>
<summary>✅ Click to verify: Expected .vscode/tasks.json</summary>

```json
{
    "version": "2.0.0",
    "tasks": [
        {
            "label": "build",
            "command": "dotnet",
            "type": "process",
            "args": [
                "build",
                "${workspaceFolder}/ExpenseTracker.slnx"
            ],
            "problemMatcher": "$msCompile"
        },
        {
            "label": "run",
            "command": "dotnet",
            "type": "process",
            "args": [
                "run",
                "--project",
                "${workspaceFolder}/src/ExpenseTracker.Web"
            ],
            "problemMatcher": "$msCompile"
        }
    ]
}
```

</details>

---

## 📚 Development Tools

### 🔍 Useful Commands

```bash
# Build solution
dotnet build

# Run application with hot reload
dotnet watch run

# Run watch with specific project
dotnet watch --project src/ExpenseTracker.Web/ExpenseTracker.Web.csproj run

# Run specific project
dotnet run --project src/ExpenseTracker.Web/ExpenseTracker.Web.csproj

# Clean build artifacts
dotnet clean

# Restore NuGet packages
dotnet restore

# Run tests (after creating test project)
dotnet test

# Create EF Core migration
dotnet ef migrations add InitialCreate --project src/ExpenseTracker.Web/ExpenseTracker.Web.csproj

# Update database
dotnet ef database update --project src/ExpenseTracker.Web/ExpenseTracker.Web.csproj
```

### 🌐 Access Points (after app starts)

- **Application**: <http://localhost:5000> or <https://localhost:5001>
- **Swagger UI**: <https://localhost:5001/swagger>
- **Health Check**: <http://localhost:5000/health>

---

## 🚨 Troubleshooting

### Common Issues and Solutions

**NuGet Packages Not Restoring**

```bash
# Clear NuGet cache
dotnet nuget locals all --clear

# Restore packages
dotnet restore
```

**.NET SDK Version Issues**

```bash
# List installed SDKs
dotnet --list-sdks

# Install .NET 10 SDK if missing:
# Download from https://dotnet.microsoft.com/download/dotnet/10.0
```

**EF Core Tools Not Found**

```bash
# Install or update globally
dotnet tool install --global dotnet-ef

# Verify installation
dotnet ef --version
```

**Port Already in Use**

```bash
# Change port in Properties/launchSettings.json
# Or kill process using port:
# macOS/Linux: lsof -ti:5000 | xargs kill
# Windows: netstat -ano | findstr :5000
```

**IDE Not Recognizing C#**

- Restart VS Code
- Run: `.NET: Restart Language Server` from Command Palette (Cmd+Shift+P)
- Verify C# Dev Kit extension is installed and enabled
- Check that OmniSharp server is running (bottom status bar)

**GitHub Copilot Not Working**

- Check authentication: `GitHub Copilot: Sign In` in Command Palette
- Verify subscription status: `GitHub Copilot: Check Status`
- Restart VS Code
- Check Copilot output panel for errors

---

## ✅ Ready for Day 1

### 🎯 Success Checklist

- [ ] .NET 10 SDK installed and verified
- [ ] Entity Framework Core tools installed
- [ ] IDE configured with C# and Copilot extensions
- [ ] Solution and project structure created
- [ ] NuGet packages installed successfully
- [ ] Application builds without errors
- [ ] Application runs and health endpoint responds
- [ ] GitHub Copilot generating suggestions

### 🚀 Next Steps

You're now ready to start **Day 1 - Session 1: Models & DbContext**!

The setup process should have taken about 10-15 minutes. If you encountered any issues, please resolve them before starting the main exercise.

**Time to build something amazing with GitHub Copilot! 🎉**

---

## 📖 Additional Resources

- [.NET CLI Documentation](https://docs.microsoft.com/dotnet/core/tools/)
- [Entity Framework Core Getting Started](https://docs.microsoft.com/ef/core/get-started/)
- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [VS Code C# Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
