# 🏗️ Day 1 - Session 1: Personal Expense Tracker - Models & DbContext (45 mins)

## 🎯 Learning Objectives

By the end of this session, you will:

- Create ASP.NET Core project structure with EF Core entities
- Implement DbContext with Fluent API configuration
- Set up SQLite database and migrations
- Master GitHub Copilot interface and context selection
- Verify entities and database work correctly

**⏱️ Time Allocation: 45 minutes (includes Copilot mastery)**

## 🤖 GitHub Copilot Focus: Basic Interface & Code Generation

**Today's Copilot Skills:**

- Interface basics (inline completions, chat, command palette)
- Context selection with `#selection`, `#editor`
- Model selection and basic prompting
- Code generation for entities and DbContext

📚 **Reference**: [Copilot Mastery Guide](../0-copilot-mastery-guide.md) for advanced techniques

---

## 📋 Prerequisites Check (2 minutes)

- ✅ .NET 10 SDK installed
- ✅ Entity Framework Core tools installed
- ✅ IDE setup (VS Code or Visual Studio)
- ✅ GitHub Copilot enabled and authenticated

**Quick Test**: Verify setup by running:

```bash
dotnet --version
dotnet ef --version
```

---

## 🚀 Session Overview

In this first session, you'll build the foundation data layer of the expense tracker. We'll focus on creating solid Entity Framework Core entities and DbContext without rushing into complex business logic.

### 🎯 What You'll Build (45 minutes)

- **EF Core Entities**: Category and Expense with relationships
- **DbContext Layer**: Fluent API configuration and database setup
- **Database Migrations**: Initial database schema
- **Basic Validation**: Essential entity validation with Data Annotations

---

## 📝 Step 1: Project Setup & Configuration (8 minutes)

### 🔧 Verify Current Project Structure

First, let's examine what we have:

```bash
# Navigate to project directory
cd ExpenseTracker/src/ExpenseTracker.Web

# Check current structure
ls -la
```

### 📄 Configure appsettings.json with Copilot

**🤖 Use GitHub Copilot to configure the database:**

1. Open `appsettings.json` in your editor
2. Open Copilot Chat (`Ctrl+Shift+I` / `Cmd+Shift+I`)


**🤖 Copilot Practice - Model Comparison:**

```text
# Try different models and compare responses:
/model gpt-4o
"What database configuration is missing for development?"

/model claude-sonnet-4  
"Review this SQLite setup and suggest improvements"
```
---

## 📝 Step 1.5: Create Specialized Development Assistant (5 minutes)

### 🤖 GitHub Copilot Agent Setup

Create a specialized .NET/EF Core assistant:

**Create `.github/agents/dotnet-expert.agent.md`:**

```text
/generate Create specialized GitHub Copilot agent for .NET development:

Filepath: .github/agents/dotnet-expert.agent.md

Include YAML frontmatter with:
- description: 'ASP.NET Core & EF Core development specialist for expense tracker'
- tools: []

Specialized behavior for:
- Project Context: ASP.NET Core 10.0, C# 13, Entity Framework Core 10.0
- Database: SQLite (dev), SQL Server (prod optional)
- Project structure: Models, Data, Services, DTOs, Validators

Expertise areas:
- Entity design with proper EF Core conventions
- DbContext configuration with Fluent API
- Repository pattern and service layer
- FluentValidation rules
- REST API best practices
- Razor Pages development

Make responses project-specific and actionable
```

**Test Your .NET Expert:**

```text
@workspace /agents dotnet-expert
#file:2.0-DotNet-Project-Overview-Personal-Expense-Tracker.md 
"Help me design the Category entity with proper EF Core attributes and relationships."
```

---

## 📝 Step 2: Create Category Entity (10 minutes)

### 🏷️ Category Entity Implementation

**🤖 Use GitHub Copilot to create the Category entity:**

1. Create new file: `Models/Category.cs`
2. Use Copilot Chat with this prompt:

```text
@workspace /newfile Create Models/Category.cs - an EF Core entity with:
- Guid Id (primary key)
- string Name (required, max 100 chars, unique)
- string? Description (optional, max 255 chars)
- string? Icon (max 50 chars for FontAwesome like "fas fa-utensils")
- string? Color (7 chars hex color, default "#6c757d")
- DateTime CreatedAt/UpdatedAt (auto timestamps)
- ICollection<Expense> Expenses navigation property
Include Data Annotations and both default + parameterized constructors.
```

**🎯 Alternative: Inline Comment Prompt**

Create `Models/Category.cs` and type:

```csharp
// EF Core Category entity with Guid Id, Name (required, max 100), Description, Icon, Color (hex), 
// CreatedAt/UpdatedAt timestamps, and Expenses navigation property. Include Data Annotations.
```

Press `Tab` to accept Copilot's multi-line suggestion.

**🎯 Copilot Feature: `#selection` context**

After generating, select the entire class and ask:

```text
#selection Analyze this entity and suggest EF Core best practice improvements
```

---

<details>
<summary>✅ Click to verify: Expected Models/Category.cs</summary>

```csharp
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Web.Models;

public class Category
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Description { get; set; }

    [MaxLength(50)]
    public string? Icon { get; set; }

    [MaxLength(7)]
    [RegularExpression(@"^#[0-9A-Fa-f]{6}$")]
    public string? Color { get; set; } = "#6c757d";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public Category()
    {
    }

    public Category(string name, string? description = null, string? icon = null, string? color = null)
    {
        Name = name;
        Description = description;
        Icon = icon;
        Color = color ?? "#6c757d";
    }
}
```

</details>

---

### ✅ Verification Checklist

- [ ] File compiles without errors
- [ ] Data Annotations present (`[Key]`, `[Required]`, `[MaxLength]`)
- [ ] Navigation property `Expenses` defined
- [ ] Both constructors present

---

## 📝 Step 3: Create Expense Entity (10 minutes)

### 💰 Expense Entity Implementation

**🤖 Use GitHub Copilot to create the Expense entity:**

1. Create new file: `Models/Expense.cs`
2. Use Copilot Chat:

```text
@workspace /newfile Create Models/Expense.cs - an EF Core entity with:
- Guid Id (primary key)
- decimal Amount (required, precision 18,2, must be positive)
- string Description (required, max 255 chars)
- DateTime ExpenseDate (defaults to today)
- Guid CategoryId (foreign key to Category)
- Category navigation property (required)
- DateTime CreatedAt/UpdatedAt audit fields
- [NotMapped] CategoryName computed property
Include Data Annotations, Range validation, and constructors.
```

**🎯 Copilot Feature: Reference existing file**

Use `#file` to reference the Category entity:

```text
#file:Models/Category.cs Create a related Expense entity that references 
this Category with proper foreign key and navigation properties
```

**🎯 Copilot Feature: `/doc` command**

After creating the entity, add XML documentation:

```text
/doc #file:Models/Expense.cs Add XML documentation comments to all public members
```

---

<details>
<summary>✅ Click to verify: Expected Models/Expense.cs</summary>

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Web.Models;

public class Expense
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public decimal Amount { get; set; }

    [Required]
    [MaxLength(255)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public DateTime ExpenseDate { get; set; } = DateTime.Today;

    [Required]
    public Guid CategoryId { get; set; }

    // Navigation property
    [Required]
    public Category Category { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Computed property
    [NotMapped]
    public string CategoryName => Category?.Name ?? "Unknown";

    public Expense()
    {
    }

    public Expense(decimal amount, string description, DateTime expenseDate, Guid categoryId)
    {
        Amount = amount;
        Description = description;
        ExpenseDate = expenseDate;
        CategoryId = categoryId;
    }
}
```

</details>

---

### ✅ Verification Checklist

- [ ] `[Column(TypeName = "decimal(18,2)")]` present on Amount
- [ ] `[Range]` validation for positive amounts
- [ ] Foreign key `CategoryId` and navigation `Category` defined
- [ ] `[NotMapped]` on computed `CategoryName`

---

## 📝 Step 4: Create DbContext (10 minutes)

### 🗄️ ExpenseTrackerContext Implementation

**🤖 Use GitHub Copilot to create the DbContext:**

1. Create new file: `Data/ExpenseTrackerContext.cs`
2. Use Copilot Chat with multiple file context:

```text
/generate Create Data/ExpenseTrackerContext.cs - EF Core DbContext with:
- DbSet<Category> and DbSet<Expense>
- OnModelCreating with Fluent API:
  * Category.Name unique index
  * Expense-Category cascade delete relationship
  * decimal(18,2) for Amount
  * Default "#6c757d" for Color
- Override SaveChanges/SaveChangesAsync to auto-update CreatedAt/UpdatedAt
- SeedData method with 5 categories (Food, Transportation, Entertainment, Shopping, Healthcare)
Reference #file:Models/Category.cs and #file:Models/Expense.cs
```

**🎯 Copilot Feature: `@workspace` for project context**

```text
@workspace Create a DbContext that works with all entity models in the Models folder, 
with proper relationships and seed data
```

**🎯 Copilot Feature: `/explain` command**

After generating, learn about the code:

```text
/explain #file:Data/ExpenseTrackerContext.cs Explain the OnModelCreating 
configuration and why we override SaveChanges
```

---

<details>
<summary>✅ Click to verify: Expected Data/ExpenseTrackerContext.cs</summary>

```csharp
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Web.Models;

namespace ExpenseTracker.Web.Data;

public class ExpenseTrackerContext : DbContext
{
    public ExpenseTrackerContext(DbContextOptions<ExpenseTrackerContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Expense> Expenses => Set<Expense>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Category configuration
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(c => c.Name).IsUnique();
            entity.Property(c => c.Color).HasDefaultValue("#6c757d");
        });

        // Expense configuration
        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasOne(e => e.Category)
                  .WithMany(c => c.Expenses)
                  .HasForeignKey(e => e.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var categories = new[]
        {
            new Category { Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), Name = "Food & Dining", Icon = "fas fa-utensils", Color = "#FF6384" },
            new Category { Id = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f12345678901"), Name = "Transportation", Icon = "fas fa-car", Color = "#36A2EB" },
            new Category { Id = Guid.Parse("c3d4e5f6-a7b8-9012-cdef-123456789012"), Name = "Entertainment", Icon = "fas fa-film", Color = "#FFCE56" },
            new Category { Id = Guid.Parse("d4e5f6a7-b8c9-0123-def0-234567890123"), Name = "Shopping", Icon = "fas fa-shopping-cart", Color = "#4BC0C0" },
            new Category { Id = Guid.Parse("e5f6a7b8-c9d0-1234-ef01-345678901234"), Name = "Healthcare", Icon = "fas fa-heartbeat", Color = "#9966FF" }
        };

        modelBuilder.Entity<Category>().HasData(categories);
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
            }
            entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
        }
    }
}
```

</details>

---

### ✅ Verification Checklist

- [ ] Both `DbSet` properties defined
- [ ] Unique index on `Category.Name`
- [ ] Cascade delete configured
- [ ] 5 seed categories with fixed GUIDs
- [ ] `SaveChanges` override updates timestamps

---

## 📝 Step 5: Configure Services & Create Migration (5 minutes)

### ⚙️ Update Program.cs

**🤖 Use GitHub Copilot to configure dependency injection:**

1. Open `Program.cs`
2. Place cursor after `var builder = WebApplication.CreateBuilder(args);`
3. Use Copilot Chat:

```text
#file:Program.cs Add ExpenseTrackerContext to DI container using SQLite 
from appsettings.json "DefaultConnection". Enable sensitive data logging 
and detailed errors in Development environment only.
```

**🎯 Copilot Feature: Inline completion with context**

Type this comment in Program.cs:

```csharp
// Register ExpenseTrackerContext with SQLite from appsettings, enable dev logging
```

Press `Tab` to accept the suggestion.

**🎯 Copilot Feature: `/fix` for missing usings**

If you see red squiggles:

```text
/fix Add missing using statements for ExpenseTrackerContext and EntityFrameworkCore
```

---

<details>
<summary>✅ Click to verify: Expected Program.cs additions</summary>

```csharp
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<ExpenseTrackerContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});

// Rest of configuration...
```

</details>

---

### 🗄️ Create Initial Migration

**🤖 Use Copilot with `@terminal` for commands:**

```text
@terminal Generate EF Core migration commands for initial database setup
```

**Run these commands:**

```bash
# Create initial migration
dotnet ef migrations add InitialCreate --project src/ExpenseTracker.Web

# Apply migration to database
dotnet ef database update --project src/ExpenseTracker.Web

# Verify database created
ls -la *.db
```

---

## ✅ Session Verification (5 minutes)

### 🧪 Test Database Setup

**🤖 Use GitHub Copilot to create a test page:**

```text
/generate Create Pages/DbTest.cshtml and Pages/DbTest.cshtml.cs - 
a Razor Page that tests database connectivity by showing count of 
Categories and Expenses from ExpenseTrackerContext. Include Bootstrap 
styling with success alert.
```

**🎯 Copilot Feature: Generate paired Razor files**

```text
/generate Create a Razor Page pair (DbTest.cshtml + DbTest.cshtml.cs) that:
- Injects ExpenseTrackerContext
- Counts Categories and Expenses using async EF Core queries
- Displays results in a Bootstrap success alert
- Shows current timestamp
```

---

<details>
<summary>✅ Click to verify: Expected Pages/DbTest.cshtml</summary>

```cshtml
@page
@model ExpenseTracker.Web.Pages.DbTestModel
@{
    ViewData["Title"] = "Database Test";
}

<div class="container mt-4">
    <h1><i class="fas fa-database"></i> Database Connection Test</h1>
    <div class="alert alert-success">
        <h4>✅ Database Connected Successfully!</h4>
        <p><strong>Categories:</strong> @Model.CategoryCount</p>
        <p><strong>Expenses:</strong> @Model.ExpenseCount</p>
        <p><em>Test completed at: @DateTime.Now</em></p>
    </div>
</div>
```

</details>

<details>
<summary>✅ Click to verify: Expected Pages/DbTest.cshtml.cs</summary>

```csharp
using Microsoft.AspNetCore.Mvc.RazorPages;
using ExpenseTracker.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Web.Pages;

public class DbTestModel : PageModel
{
    private readonly ExpenseTrackerContext _context;

    public DbTestModel(ExpenseTrackerContext context)
    {
        _context = context;
    }

    public int CategoryCount { get; set; }
    public int ExpenseCount { get; set; }

    public async Task OnGetAsync()
    {
        CategoryCount = await _context.Categories.CountAsync();
        ExpenseCount = await _context.Expenses.CountAsync();
    }
}
```

</details>

---

### 🚀 Run and Verify

```bash
# Run application
dotnet run

# Open browser: https://localhost:5001/DbTest
# You should see: Categories: 5, Expenses: 0
```

---

## 🎉 Session Complete!

### ✅ What You've Built

- ✅ Category and Expense entities with proper EF Core configuration
- ✅ ExpenseTrackerContext with Fluent API and seed data
- ✅ SQLite database with migrations
- ✅ Automatic timestamp tracking
- ✅ Verified database connectivity

### 📚 Copilot Skills Learned

- ✅ Context selection (`#selection`, `#editor`)
- ✅ Model switching for comparison
- ✅ Agent creation for specialized assistance
- ✅ Code generation for entities and DbContext

### 🚀 Next Session

**Session 2: Services & Business Logic** - Build the service layer with business logic, FluentValidation, and unit tests!

**Great work! Take a 5-minute break before continuing.** ☕
