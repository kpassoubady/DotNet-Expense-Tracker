# 🎨 Day 1 Setup: Frontend & Web Layer Prerequisites

## 🎯 Setup Overview

This setup ensures Day 1 backend is working correctly and prepares your environment for Day 1 frontend development (afternoon sessions). You'll verify the backend services and configure Razor Pages with static resources.

**⏱️ Estimated Setup Time: 5 minutes**

---

## ✅ Prerequisites Verification

### 🏗️ Day 1 Backend Must Be Complete

**Verify Backend Services Working**

```bash
# Navigate to your project directory
cd ExpenseTracker/src/ExpenseTracker.Web

# Start the application
dotnet run

# In another terminal, test the backend
curl -X GET https://localhost:5001/api/categories
curl -X GET https://localhost:5001/api/expenses

# You should see JSON responses with sample data
# Stop the application with Ctrl+C
```

**Required Backend Components**

- [ ] Application starts without errors (`dotnet run`)
- [ ] `ExpenseService` and `CategoryService` working
- [ ] SQLite database with sample data
- [ ] All unit tests passing: `dotnet test`
- [ ] API endpoints returning data

---

## 📦 Frontend Dependencies Setup

### 🔧 Install Client-Side Libraries

For this bootcamp, we'll use CDN links for simplicity. Alternatively, you can use LibMan (Library Manager).

**Option 1: CDN Links (Recommended for Quick Setup)**

No installation needed - we'll reference Bootstrap, jQuery, and Chart.js via CDN in our Razor layout.

**Option 2: LibMan (Library Manager) - Professional Approach**

```bash
# Initialize LibMan
dotnet tool install --global Microsoft.Web.LibraryManager.Cli

# Navigate to project directory
cd src/ExpenseTracker.Web

# Install Bootstrap
libman install bootstrap@5.3.2 -p unpkg -d wwwroot/lib/bootstrap

# Install jQuery
libman install jquery@3.7.1 -p unpkg -d wwwroot/lib/jquery

# Install Chart.js
libman install chart.js@4.4.0 -p unpkg -d wwwroot/lib/chart.js
```

Create `libman.json` in project root:

```json
{
  "version": "1.0",
  "defaultProvider": "unpkg",
  "libraries": [
    {
      "library": "bootstrap@5.3.2",
      "destination": "wwwroot/lib/bootstrap"
    },
    {
      "library": "jquery@3.7.1",
      "destination": "wwwroot/lib/jquery"
    },
    {
      "library": "chart.js@4.4.0",
      "destination": "wwwroot/lib/chart.js"
    }
  ]
}
```

---

## 🗂️ Frontend Directory Structure

### 📁 Create Frontend Directories

```bash
# From src/ExpenseTracker.Web directory
mkdir -p Pages/Shared
mkdir -p Pages/Expenses
mkdir -p Pages/Categories
mkdir -p wwwroot/css
mkdir -p wwwroot/js
mkdir -p wwwroot/lib
mkdir -p wwwroot/images
```

### 🎨 Expected Structure After Web Layer Session

```text
src/ExpenseTracker.Web/
├── Pages/
│   ├── Shared/
│   │   ├── _Layout.cshtml
│   │   └── _ValidationScriptsPartial.cshtml
│   ├── Expenses/
│   │   ├── Index.cshtml
│   │   ├── Index.cshtml.cs
│   │   ├── Create.cshtml
│   │   ├── Create.cshtml.cs
│   │   ├── Edit.cshtml
│   │   └── Edit.cshtml.cs
│   ├── Categories/
│   │   ├── Index.cshtml
│   │   └── Index.cshtml.cs
│   ├── Index.cshtml
│   ├── Index.cshtml.cs
│   └── _ViewImports.cshtml
├── wwwroot/
│   ├── css/
│   │   ├── site.css
│   │   └── dashboard.css
│   ├── js/
│   │   ├── site.js
│   │   ├── dashboard.js
│   │   └── expenses.js
│   ├── lib/                   # If using LibMan
│   │   ├── bootstrap/
│   │   ├── jquery/
│   │   └── chart.js/
│   └── images/
└── Models/
```

---

## 🔧 Configuration Updates

### ⚙️ Update Program.cs

Ensure Razor Pages and static files are configured in `Program.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddControllers(); // For API endpoints

// Add DbContext
builder.Services.AddDbContext<ExpenseTrackerContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Important for serving wwwroot files

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers(); // For API routes

app.Run();
```

### 📄 Create _ViewImports.cshtml

Create `Pages/_ViewImports.cshtml`:

```cshtml
@using ExpenseTracker.Web
@using ExpenseTracker.Web.Models
@using ExpenseTracker.Web.DTOs
@namespace ExpenseTracker.Web.Pages
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

### 📄 Create _ViewStart.cshtml

Create `Pages/_ViewStart.cshtml`:

```cshtml
@{
    Layout = "_Layout";
}
```

---

## 🎨 Static File Configuration

### 📝 Update appsettings.json

Add static file options to `appsettings.json`:

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
  "AllowedHosts": "*",
  "StaticFiles": {
    "CacheControl": "public,max-age=3600"
  }
}
```

### 🎨 Create Base CSS

Create `wwwroot/css/site.css`:

```css
/* Custom styles for Expense Tracker */
:root {
    --primary-color: #0d6efd;
    --success-color: #198754;
    --danger-color: #dc3545;
    --warning-color: #ffc107;
}

html {
    font-size: 14px;
}

@media (min-width: 768px) {
    html {
        font-size: 16px;
    }
}

body {
    margin-bottom: 60px;
}

.expense-card {
    transition: transform 0.2s;
}

.expense-card:hover {
    transform: translateY(-5px);
    box-shadow: 0 4px 8px rgba(0,0,0,0.1);
}

.category-badge {
    font-size: 0.875rem;
    padding: 0.25rem 0.5rem;
}
```

---

## ✅ Setup Verification

### 🧪 Test Razor Pages Setup

Create a simple test page `Pages/Test.cshtml`:

```cshtml
@page
@model ExpenseTracker.Web.Pages.TestModel
@{
    ViewData["Title"] = "Test Page";
}

<div class="container mt-4">
    <h1>Frontend Setup Test</h1>
    <div class="alert alert-success">
        <h4>✅ Razor Pages Working!</h4>
        <p>Current Time: @DateTime.Now</p>
        <p>Environment: @Model.Environment</p>
    </div>
</div>
```

Create `Pages/Test.cshtml.cs`:

```csharp
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExpenseTracker.Web.Pages
{
    public class TestModel : PageModel
    {
        private readonly IWebHostEnvironment _env;

        public TestModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string Environment => _env.EnvironmentName;

        public void OnGet()
        {
        }
    }
}
```

### 🚀 Run Verification Tests

```bash
# Build the project
dotnet build

# Run the application
dotnet run

# Open browser to:
# https://localhost:5001/Test

# You should see a success message with current time
```

---

## 🔍 Verify Static Files

### 🧪 Test Static File Serving

Create `wwwroot/test.html`:

```html
<!DOCTYPE html>
<html>
<head>
    <title>Static File Test</title>
    <link href="/css/site.css" rel="stylesheet" />
</head>
<body>
    <h1>Static Files Working! ✅</h1>
    <p>If you can see this page, static file serving is configured correctly.</p>
</body>
</html>
```

Visit: `https://localhost:5001/test.html`

---

## 📚 Frontend Development Tools

### 🔍 Useful Commands

```bash
# Run with hot reload (automatically refreshes on file changes)
dotnet watch run

# Build CSS/JS bundles (if using bundling)
dotnet bundle

# Clean wwwroot/lib (if using LibMan)
libman clean

# Restore client libraries
libman restore
```

### 🌐 Access Points (after app starts)

- **Home Page**: <https://localhost:5001/>
- **Test Page**: <https://localhost:5001/Test>
- **Static Test**: <https://localhost:5001/test.html>
- **Swagger UI**: <https://localhost:5001/swagger>

---

## 🚨 Troubleshooting

### Common Issues and Solutions

**Static Files Not Serving**

```csharp
// Ensure in Program.cs:
app.UseStaticFiles();

// Verify wwwroot directory exists in project root
```

**Razor Pages Not Found**

```csharp
// Ensure in Program.cs:
builder.Services.AddRazorPages();
app.MapRazorPages();

// Check that Pages directory exists
```

**CSS/JS Not Loading**

```html
<!-- Verify paths in _Layout.cshtml -->
<link rel="stylesheet" href="~/css/site.css" />
<script src="~/js/site.js"></script>

<!-- Clear browser cache (Cmd+Shift+R or Ctrl+Shift+F5) -->
```

**LibMan Restore Failing**

```bash
# Clear LibMan cache
libman cache clean

# Restore libraries
libman restore
```

**Tag Helpers Not Working**

```cshtml
<!-- Ensure _ViewImports.cshtml exists with: -->
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

---

## ✅ Ready for Frontend Sessions

### 🎯 Success Checklist

- [ ] Backend services verified and working
- [ ] Razor Pages configured in Program.cs
- [ ] Static file serving enabled
- [ ] Frontend directory structure created
- [ ] Bootstrap, jQuery available (CDN or LibMan)
- [ ] Test page loads successfully
- [ ] Static files accessible
- [ ] Hot reload working (`dotnet watch run`)

### 🚀 Next Steps

**Session Sequence:**
1. **Session 3**: REST APIs & Controllers - Build API endpoints first
2. **Session 4**: Razor Pages & Web Interface - Build the UI (uses this frontend setup)

You're now ready for **Day 1 Afternoon Sessions**!

The frontend setup should have taken about 5 minutes. If you encountered any issues, please resolve them before continuing.

**Let's build a beautiful UI with GitHub Copilot! 🎨**

---

## 📖 Additional Resources

- [Razor Pages Documentation](https://docs.microsoft.com/aspnet/core/razor-pages)
- [Static Files in ASP.NET Core](https://docs.microsoft.com/aspnet/core/fundamentals/static-files)
- [Tag Helpers Documentation](https://docs.microsoft.com/aspnet/core/mvc/views/tag-helpers/intro)
- [LibMan Documentation](https://docs.microsoft.com/aspnet/core/client-side/libman/)
- [Bootstrap 5 Documentation](https://getbootstrap.com/docs/5.3/)
