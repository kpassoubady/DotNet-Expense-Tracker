# 🏗️ Day 1 - Session 2: Personal Expense Tracker - Services & Business Logic (45 mins)

## 🎯 Learning Objectives

By the end of this session, you will:

- Create service interfaces and implementations with dependency injection
- Implement FluentValidation for business rules
- Generate comprehensive unit tests with xUnit
- Create realistic sample data using Copilot
- Master pattern-based AI code generation

**⏱️ Time Allocation: 45 minutes (includes AI-assisted testing)**

## 🤖 GitHub Copilot Focus: Pattern Recognition & Test Generation

**Today's Copilot Skills:**

- Pattern-based code generation for services
- FluentValidation rule creation
- Unit test generation with AAA pattern
- Realistic test data creation
- Intermediate context-aware suggestions

📚 **Reference**: [Copilot Mastery Guide](../0-copilot-mastery-guide.md)

---

## 📝 Step 1: Create DTOs (8 minutes)

### 📦 Create Data Transfer Objects

**🤖 Use GitHub Copilot to create DTOs:**

1. Create folder `DTOs/` if not exists
2. Use Copilot Chat with workspace context:

```text
/generate Create DTOs in DTOs/ directory based on #file:Models/Category.cs 
and #file:Models/Expense.cs:

1. CategoryDto.cs - read DTO with Id, Name, Description, Icon, Color, ExpenseCount, CreatedAt
2. CreateCategoryDto.cs - create DTO with Name (required), Description, Icon, Color with Data Annotations
3. UpdateCategoryDto.cs - same as Create
4. ExpenseDto.cs - read DTO with Id, Amount, Description, ExpenseDate, CategoryId, CategoryName, CategoryColor, CreatedAt
5. CreateExpenseDto.cs - create DTO with Amount (>0), Description, ExpenseDate, CategoryId with Data Annotations
6. UpdateExpenseDto.cs - same as Create
```

**🎯 Alternative Copilot Feature: Inline comment for individual DTO**

Create `DTOs/CategoryDto.cs` and type:

```csharp
// Record DTO for reading Category with Id, Name, Description, Icon, Color, ExpenseCount, CreatedAt
```

Press `Tab` to accept.

---

## 📝 Step 2: Create Service Interfaces (5 minutes)

### 🔧 Service Interface Definitions

**🤖 Use GitHub Copilot to create service interfaces:**

```text
/generate Create Services/ICategoryService.cs interface with async CRUD methods:
- GetAllCategoriesAsync() returning IEnumerable<CategoryDto>
- GetCategoryByIdAsync(Guid id) returning CategoryDto?
- CreateCategoryAsync(CreateCategoryDto dto)
- UpdateCategoryAsync(Guid id, UpdateCategoryDto dto)
- DeleteCategoryAsync(Guid id)
- CategoryExistsAsync(Guid id) returning bool
Reference #file:DTOs/CategoryDto.cs for return types.
```

**🎯 Copilot Feature: Generate matching interface from DTO**

```text
#file:DTOs/ExpenseDto.cs Create IExpenseService interface with CRUD operations 
plus GetExpensesByCategoryAsync, GetTotalExpensesAsync, GetExpensesByCategoryGroupAsync
```

**🎯 Copilot Feature: `/tests` to preview interface usage**

```text
/tests #file:Services/ICategoryService.cs Generate test method signatures 
to verify interface design is testable
```

---

## 📝 Step 3: Create FluentValidation Validators (10 minutes)

### ✅ FluentValidation Rules

**🤖 Use GitHub Copilot to create validators:**

```text
/generate Create Validators/CreateCategoryDtoValidator.cs using FluentValidation:
- Name: Required, max 100 chars, must be unique (sync check against database)
- Description: Optional, max 255 chars
- Color: Optional, must match hex pattern #RRGGBB
- Icon: Optional, max 50 chars
Inject ExpenseTrackerContext for uniqueness validation.
Use Must() not MustAsync() - ASP.NET validation pipeline is synchronous.
Reference #file:DTOs/CreateCategoryDto.cs and #file:Data/ExpenseTrackerContext.cs
```

**🎯 Copilot Feature: Pattern-based generation**

After creating first validator, use it as a pattern:

```text
#file:Validators/CreateCategoryDtoValidator.cs Create similar validator 
for CreateExpenseDto with:
- Amount: > 0, max 2 decimal places
- Description: Required, max 255
- ExpenseDate: Cannot be future date
- CategoryId: Must exist in database (use Must, not MustAsync)
```

> ⚠️ **Important**: Use `Must()` instead of `MustAsync()` for database checks. ASP.NET's automatic validation pipeline doesn't support async validators.

---

<details>
<summary>✅ Click to verify: Expected Validators/CreateCategoryDtoValidator.cs</summary>

```csharp
using FluentValidation;
using ExpenseTracker.Web.DTOs;
using ExpenseTracker.Web.Data;

namespace ExpenseTracker.Web.Validators;

public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    private readonly ExpenseTrackerContext _context;

    public CreateCategoryDtoValidator(ExpenseTrackerContext context)
    {
        _context = context;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required")
            .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters")
            .Must(BeUniqueName).WithMessage("Category name already exists");

        RuleFor(x => x.Description)
            .MaximumLength(255).WithMessage("Description cannot exceed 255 characters")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.Color)
            .Matches(@"^#[0-9A-Fa-f]{6}$").WithMessage("Color must be a valid hex code (#RRGGBB)")
            .When(x => !string.IsNullOrEmpty(x.Color));

        RuleFor(x => x.Icon)
            .MaximumLength(50).WithMessage("Icon cannot exceed 50 characters")
            .When(x => !string.IsNullOrEmpty(x.Icon));
    }

    private bool BeUniqueName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return true;
        return !_context.Categories.Any(c => c.Name.ToLower() == name.ToLower());
    }
}
```

</details>

---

## 📝 Step 4: Implement Service Layer (15 minutes)

### 🔧 ExpenseService Implementation

**🤖 Use GitHub Copilot to implement the service:**

```text
/generate Create Services/ExpenseService.cs implementing IExpenseService:
- Inject ExpenseTrackerContext and ILogger<ExpenseService>
- Implement all interface methods with async/await
- Map entities to DTOs (manual mapping or extension methods)
- Include try-catch with logging for error handling
- Add XML documentation comments
Reference #file:Services/IExpenseService.cs and #file:Data/ExpenseTrackerContext.cs
```

**🎯 Copilot Feature: Implement interface automatically**

Create the file and type:

```csharp
public class ExpenseService : IExpenseService
{
    // Copilot will suggest constructor and all interface implementations
```

Press `Tab` repeatedly to accept method implementations.

**🎯 Copilot Feature: `/doc` for documentation**

```text
/doc #file:Services/ExpenseService.cs Add XML documentation to all public methods
```

**Key Methods to Generate**:

```csharp
public class ExpenseService : IExpenseService
{
    private readonly ExpenseTrackerContext _context;
    private readonly ILogger<ExpenseService> _logger;

    public ExpenseService(ExpenseTrackerContext context, ILogger<ExpenseService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ExpenseDto>> GetAllExpensesAsync()
    {
        // Implementation with Include for Category
    }

    public async Task<ExpenseDto> CreateExpenseAsync(CreateExpenseDto dto)
    {
        // Validate category exists
        // Create entity
        // Save changes
        // Return DTO
    }

    // ... other methods
}
```

### 🔧 CategoryService Implementation

Similar pattern for `Services/CategoryService.cs` implementing `ICategoryService`.

---

## 📝 Step 5: Create Unit Tests (10 minutes)

### 🧪 Generate Comprehensive Tests

**Copilot Prompt:**

```text
/generate Create xUnit test project and test class for ExpenseService:

1. Create test project:
   dotnet new xunit -n ExpenseTracker.Tests -o tests/ExpenseTracker.Tests
   dotnet sln add tests/ExpenseTracker.Tests

2. Add packages:
   - Microsoft.EntityFrameworkCore.InMemory
   - Moq
   - FluentAssertions
   - xunit
   - xunit.runner.visualstudio

3. Create ExpenseServiceTests.cs with tests:
   - GetAllExpensesAsync_ReturnsAllExpenses
   - GetExpenseByIdAsync_ValidId_ReturnsExpense
   - GetExpenseByIdAsync_InvalidId_ReturnsNull
   - CreateExpenseAsync_ValidData_CreatesExpense
   - CreateExpenseAsync_InvalidCategoryId_ThrowsException
   - UpdateExpenseAsync_ValidData_UpdatesExpense
   - DeleteExpenseAsync_ValidId_DeletesExpense
   - GetTotalExpensesAsync_CalculatesCorrectTotal

Use Arrange-Act-Assert pattern, in-memory database, realistic test data
```

**Example Test**:

```csharp
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Web.Data;
using ExpenseTracker.Web.Services;
using ExpenseTracker.Web.Models;
using ExpenseTracker.Web.DTOs;
using Microsoft.Extensions.Logging.Abstractions;

namespace ExpenseTracker.Tests.Services;

public class ExpenseServiceTests : IDisposable
{
    private readonly ExpenseTrackerContext _context;
    private readonly ExpenseService _service;

    public ExpenseServiceTests()
    {
        var options = new DbContextOptionsBuilder<ExpenseTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ExpenseTrackerContext(options);
        _service = new ExpenseService(_context, NullLogger<ExpenseService>.Instance);

        SeedTestData();
    }

    private void SeedTestData()
    {
        var category = new Category { Id = Guid.NewGuid(), Name = "Food" };
        _context.Categories.Add(category);

        var expense = new Expense 
        { 
            Id = Guid.NewGuid(), 
            Amount = 25.50m, 
            Description = "Lunch",
            ExpenseDate = DateTime.Today,
            CategoryId = category.Id
        };
        _context.Expenses.Add(expense);

        _context.SaveChanges();
    }

    [Fact]
    public async Task GetAllExpensesAsync_ReturnsAllExpenses()
    {
        // Arrange - already done in constructor

        // Act
        var result = await _service.GetAllExpensesAsync();

        // Assert
        result.Should().NotBeEmpty();
        result.Should().HaveCount(1);
        result.First().Description.Should().Be("Lunch");
    }

    [Fact]
    public async Task CreateExpenseAsync_ValidData_CreatesExpense()
    {
        // Arrange
        var category = await _context.Categories.FirstAsync();
        var dto = new CreateExpenseDto
        {
            Amount = 50.00m,
            Description = "Dinner",
            ExpenseDate = DateTime.Today,
            CategoryId = category.Id
        };

        // Act
        var result = await _service.CreateExpenseAsync(dto);

        // Assert
        result.Should().NotBeNull();
        result.Amount.Should().Be(50.00m);
        result.Description.Should().Be("Dinner");

        var dbExpense = await _context.Expenses.FindAsync(result.Id);
        dbExpense.Should().NotBeNull();
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
```

### 🚀 Run Tests

```bash
# Run all tests
dotnet test

# Run with detailed output
dotnet test --logger "console;verbosity=detailed"

# Expected: All tests passing ✅
```

---

## 📝 Step 6: Register Services in DI Container (2 minutes)

### ⚙️ Update Program.cs

Add service registrations:

```csharp
// Add services to DI container
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
```

### CategoryService.cs

/generate Like ExpenseService.cs create CategoryService.cs


---

## ✅ Session Verification (5 minutes)

### 🧪 Final Verification

1. **Build Solution**: `dotnet build` - should succeed
2. **Run Tests**: `dotnet test` - all tests should pass
3. **Run Application**: `dotnet run` - should start without errors
4. **Check Services**: Services should be available via DI

---

## 🎉 Session Complete!

### ✅ What You've Built

- ✅ Complete DTO layer for data transfer
- ✅ Service interfaces with clear contracts
- ✅ FluentValidation rules with async database checks
- ✅ ExpenseService and CategoryService implementations
- ✅ 15+ comprehensive unit tests with xUnit
- ✅ Dependency injection configured

### 📚 Copilot Skills Learned

- ✅ Pattern-based service generation
- ✅ FluentValidation rule creation
- ✅ Comprehensive test generation
- ✅ Realistic test data creation
- ✅ Intermediate context-aware coding

### 🚀 Next Session

**Session 3: REST APIs & Controllers** - Build RESTful API endpoints with Chat Interface mastery!

**Great work! Take a 5-minute break before afternoon sessions.** ☕
