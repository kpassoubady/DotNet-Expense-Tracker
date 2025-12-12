# 🏗️ Day 1 - Session 3: Personal Expense Tracker - REST APIs & Controllers (45 mins)

## 🎯 Learning Objectives

By the end of this session, you will:

- Master GitHub Copilot Chat Interface for interactive development
- Build REST API endpoints using Controllers or Minimal APIs
- Implement proper HTTP status codes and error handling
- Use Copilot agents (`@workspace`, `@terminal`, `@git`)
- Test APIs using Swagger and terminal tools

**⏱️ Time Allocation: 45 minutes (Chat Interface mastery focus)**

## 🤖 GitHub Copilot Focus: Chat Interface & Agent Mastery

**Today's Copilot Skills:**

- Advanced Chat Interface usage
- Agent-based context (`@workspace`, `@terminal`, `@git`)
- Slash commands (`/explain`, `/fix`, `/generate`)
- Chat-guided API design patterns
- Terminal-integrated testing with Copilot

---

## 📝 Step 1: Choose Your API Style (5 minutes)

### 🎯 Two Approaches

| Feature         | **Option A: Minimal APIs**         | **Option B: Controllers**          |
|-----------------|------------------------------------|------------------------------------|
| **Style**       | Modern, lightweight (like FastAPI) | Traditional MVC (like Spring Boot) |
| **Location**    | Functions in `Program.cs`          | Class files in `Controllers/`      |
| **Boilerplate** | Less code, more concise            | More structure, more verbose       |
| **Best For**    | Microservices, small APIs          | Large applications, team projects  |
| **Background**  | New to .NET                        | Java/Spring developers             |

**📌 For This Bootcamp**: We'll follow **Option B: Controllers** as the primary approach because:
- Better organized for enterprise applications
- More familiar to Java/Spring developers
- Clearer separation of concerns

> **Note**: Option A (Minimal APIs) is shown as an alternative reference in Step 2B. You only need to complete ONE option, not both.

---

## 📝 Step 2: Create API Controllers (Option B) (20 minutes)

### 🔧 ExpensesController Implementation

**🤖 Use GitHub Copilot Chat to create the controller:**

1. Create folder: `Controllers/Api/`
2. Use Copilot Chat:

```text
/generate Create Controllers/Api/ExpensesController.cs - ASP.NET Core API Controller:
- Route: api/expenses
- Inject IExpenseService and ILogger
- GET all: returns 200 OK with IEnumerable<ExpenseDto>
- GET {id}: returns 200 OK or 404 NotFound
- POST: returns 201 Created with location header
- PUT {id}: returns 204 NoContent or 404
- DELETE {id}: returns 204 NoContent or 404
- GET total: returns total expenses amount
- GET by-category: returns grouped expenses
Include [ProducesResponseType] attributes and XML docs.
Reference #file:Services/IExpenseService.cs for methods.
```

**🎯 Copilot Feature: `@workspace` for service context**

```text
@workspace Create an API controller that uses IExpenseService, 
following REST best practices with proper HTTP status codes
```

**🎯 Copilot Feature: Inline completion for controller methods**

Type in the controller file:

```csharp
/// <summary>
/// Get all expenses
/// </summary>
[HttpGet]
// Copilot will suggest the full method implementation
```

---

<details>
<summary>✅ Click to verify: Expected ExpensesController.cs</summary>

```csharp
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Web.Services;
using ExpenseTracker.Web.DTOs;

namespace ExpenseTracker.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _expenseService;
    private readonly ILogger<ExpensesController> _logger;

    public ExpensesController(IExpenseService expenseService, ILogger<ExpensesController> logger)
    {
        _expenseService = expenseService;
        _logger = logger;
    }

    /// <summary>
    /// Get all expenses
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExpenseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetAllExpenses()
    {
        var expenses = await _expenseService.GetAllExpensesAsync();
        return Ok(expenses);
    }

    /// <summary>
    /// Get expense by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ExpenseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExpenseDto>> GetExpense(Guid id)
    {
        var expense = await _expenseService.GetExpenseByIdAsync(id);
        if (expense == null)
        {
            _logger.LogWarning("Expense with ID {Id} not found", id);
            return NotFound();
        }
        return Ok(expense);
    }

    /// <summary>
    /// Create new expense
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ExpenseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExpenseDto>> CreateExpense([FromBody] CreateExpenseDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _expenseService.CreateExpenseAsync(dto);
        return CreatedAtAction(nameof(GetExpense), new { id = created.Id }, created);
    }

    /// <summary>
    /// Update existing expense
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateExpense(Guid id, [FromBody] UpdateExpenseDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _expenseService.UpdateExpenseAsync(id, dto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Delete expense
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteExpense(Guid id)
    {
        try
        {
            await _expenseService.DeleteExpenseAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Get total expenses amount
    /// </summary>
    [HttpGet("total")]
    [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
    public async Task<ActionResult<decimal>> GetTotal()
    {
        var total = await _expenseService.GetTotalExpensesAsync();
        return Ok(total);
    }

    /// <summary>
    /// Get expenses grouped by category
    /// </summary>
    [HttpGet("by-category")]
    [ProducesResponseType(typeof(Dictionary<string, decimal>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Dictionary<string, decimal>>> GetByCategory()
    {
        var grouped = await _expenseService.GetExpensesByCategoryGroupAsync();
        return Ok(grouped);
    }
}
```

</details>

---

### 🔧 CategoriesController Implementation

**🤖 Use pattern-based generation:**

```text
#file:Controllers/Api/ExpensesController.cs Create CategoriesController 
following the same pattern, using ICategoryService instead.
Include CRUD operations for categories.
```

**🎯 Alternative Copilot Feature: Pattern replication**

Copilot recognizes patterns - use existing controller as template:

```text
@workspace Based on #file:Controllers/Api/ExpensesController.cs, 
create a matching CategoriesController with ICategoryService
```

---

## 📝 Step 2B: Create Minimal APIs (Option A - Alternative) (20 minutes)

### 🚀 Minimal API Endpoints

**🤖 Use GitHub Copilot for Minimal APIs:**

If you prefer the modern Minimal API approach, use this prompt:

```text
#file:Program.cs Add Minimal API endpoints for Expenses:
- MapGroup("/api/expenses") with tags
- MapGet all, MapGet by id, MapPost, MapPut, MapDelete
- Use IExpenseService from DI
- Include proper Results (Ok, NotFound, Created)
- Add Produces<T> attributes for response types
```

**🎯 Copilot Feature: Inline completion in Program.cs**

Type:

```csharp
// Minimal API endpoints for expenses
var expensesApi = app.MapGroup("/api/expenses")
```

Press `Tab` - Copilot will suggest the complete endpoint group.

---

<details>
<summary>✅ Click to verify: Expected Minimal API code</summary>

```csharp
// Expense endpoints
var expensesApi = app.MapGroup("/api/expenses")
    .WithTags("Expenses");

expensesApi.MapGet("/", async (IExpenseService service) =>
{
    var expenses = await service.GetAllExpensesAsync();
    return Results.Ok(expenses);
})
.WithName("GetAllExpenses")
.Produces<IEnumerable<ExpenseDto>>();

expensesApi.MapGet("/{id}", async (Guid id, IExpenseService service) =>
{
    var expense = await service.GetExpenseByIdAsync(id);
    return expense is not null ? Results.Ok(expense) : Results.NotFound();
})
.WithName("GetExpense")
.Produces<ExpenseDto>()
.Produces(StatusCodes.Status404NotFound);

expensesApi.MapPost("/", async (CreateExpenseDto dto, IExpenseService service) =>
{
    var created = await service.CreateExpenseAsync(dto);
    return Results.Created($"/api/expenses/{created.Id}", created);
})
.WithName("CreateExpense")
.Produces<ExpenseDto>(StatusCodes.Status201Created);

// ... similar for PUT, DELETE
```

</details>

---

## 📝 Step 3: Test APIs with Swagger (10 minutes)

### 🧪 Using Swagger UI

```bash
# Run application
dotnet run

# Open Swagger UI
# https://localhost:5001/swagger
```

**🤖 Use Copilot `@terminal` for test commands:**

```text
@terminal Generate curl commands to test all CRUD operations 
on the /api/expenses endpoint with sample JSON data
```

**🎯 Copilot Feature: `@terminal` for CLI help**

```text
@terminal How do I use curl to POST JSON to a local HTTPS API 
with self-signed certificate?
```

### 🔍 Test with curl

**🤖 Let Copilot generate test commands:**

```text
@workspace @terminal Generate curl commands to test:
1. GET all expenses
2. POST new expense with Food category
3. GET total expenses
4. DELETE an expense
Use the seed data category IDs from #file:Data/ExpenseTrackerContext.cs
```

---

<details>
<summary>✅ Click to verify: Expected curl commands</summary>

```bash
# Get all expenses (using https)
curl -X GET https://localhost:5001/api/expenses -k

# Create expense (Food & Dining category from seed data) (using https)
curl -X POST https://localhost:5001/api/expenses \
  -H "Content-Type: application/json" \
  -d '{
    "amount": 45.99,
    "description": "Grocery shopping",
    "expenseDate": "2025-12-07",
    "categoryId": "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
  }' -k

# Get total
curl -X GET https://localhost:5001/api/expenses/total -k

# Get by category
curl -X GET https://localhost:5001/api/expenses/by-category -k

# Add new expense using curl (note:http)
curl -X POST http://localhost:5000/api/expenses \
  -H "Content-Type: application/json" \
  -d '{
    "amount": 45.99,
    "description": "Lunch at restaurant",
    "expenseDate": "2025-12-12",
    "categoryId": "11111111-1111-1111-1111-111111111111"
  }'

```

</details>

---

## 📝 Step 4: Error Handling Middleware (10 minutes)

### 🛡️ Global Exception Handler

**🤖 Use GitHub Copilot to create middleware:**

```text
/generate Create Middleware/ExceptionHandlingMiddleware.cs:
- Implement IMiddleware pattern
- Catch all unhandled exceptions in try-catch
- Log errors with ILogger
- Return ProblemDetails JSON for errors
- Map exception types to HTTP status codes:
  * ValidationException → 400 Bad Request
  * KeyNotFoundException → 404 Not Found
  * UnauthorizedAccessException → 401 Unauthorized
  * Exception → 500 Internal Server Error
Include request path in error details for debugging.
```

**🎯 Copilot Feature: `/explain` middleware pattern**

```text
/explain What is the middleware pipeline in ASP.NET Core 
and how does exception handling middleware work?
```

**🤖 Register middleware in Program.cs:**

```text
#file:Program.cs Add ExceptionHandlingMiddleware to the pipeline 
before other middleware. Show the correct ordering.
```

---

<details>
<summary>✅ Click to verify: Expected middleware registration</summary>

```csharp
// Add early in pipeline, before other middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();
```

</details>

---

## ✅ Session Verification (5 minutes)

### 🧪 API Testing Checklist

- [ ] Swagger UI loads successfully
- [ ] GET /api/expenses returns 200 OK
- [ ] GET /api/expenses/{id} returns 200 or 404
- [ ] POST /api/expenses creates and returns 201
- [ ] PUT /api/expenses/{id} updates and returns 204
- [ ] DELETE /api/expenses/{id} deletes and returns 204
- [ ] GET /api/expenses/total returns correct sum
- [ ] Error responses return proper JSON

---

## 🎉 Session Complete!

### ✅ What You've Built

- ✅ Complete REST API layer (Controllers or Minimal APIs)
- ✅ Proper HTTP status codes and error handling
- ✅ OpenAPI/Swagger documentation
- ✅ Global exception handling middleware
- ✅ Tested all CRUD operations

### 📚 Copilot Skills Learned

- ✅ Advanced Chat Interface usage
- ✅ Agent-based development (`@workspace`, `@terminal`)
- ✅ Chat-guided API design
- ✅ Terminal-integrated testing
- ✅ Slash command mastery

### 🚀 Next Session

**Session 4: Razor Pages & Web Interface** - Build a beautiful UI with multi-language coordination!

**Take a 5-minute break!** ☕
