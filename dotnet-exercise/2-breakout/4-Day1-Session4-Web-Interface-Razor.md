# 🏗️ Day 1 - Session 4: Razor Pages & Web Interface (45 mins)

## 🎯 Learning Objectives

By the end of this session, you will:

- Create Razor Pages with Bootstrap 5 integration
- Build complete CRUD pages for Categories and Expenses
- Implement interactive dashboards with Chart.js
- Master `/generate` command for creating new files

**⏱️ Time Allocation: 45 minutes**

## 🤖 GitHub Copilot Focus: Multi-Language Coordination

**Today's Copilot Skills:**

- Multi-language context (C#, Razor, HTML, CSS, JavaScript)
- `/generate` command for new file creation
- Tag Helper suggestions
- JavaScript integration

---

## 📝 Step 1: Create Shared Layout (5 minutes)

### 🎨 Main Layout Template

**🤖 Use `/generate` to create the layout:**

```text
/generate Create Pages/Shared/_Layout.cshtml - Razor layout with:
- Bootstrap 5.3.2 CDN (CSS + JS bundle)
- jQuery 3.7.1 CDN  
- Chart.js 4.4.0 CDN
- Font Awesome 6.4.0 for icons
- Responsive navbar with: Dashboard, Expenses, Categories links
- asp-page Tag Helpers for navigation (/Index, /Expenses/Index, /Categories/Index)
- @RenderBody() main content area
- Footer with copyright
- @RenderSectionAsync("Scripts") for page scripts
- Modern dark navbar (navbar-dark bg-primary)
```

---

<details>
<summary>✅ Click to verify: Expected _Layout.cshtml</summary>

```cshtml
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - Expense Tracker</title>
    
    <!-- Bootstrap 5.3.2 CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome 6.4.0 -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet">
    <!-- Custom CSS -->
    <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
</head>
<body>
    <header>
        <nav class="navbar navbar-expand-lg navbar-dark bg-primary">
            <div class="container-fluid">
                <a class="navbar-brand" asp-page="/Index">
                    <i class="fas fa-wallet me-2"></i>Expense Tracker
                </a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav ms-auto">
                        <li class="nav-item">
                            <a class="nav-link" asp-page="/Index">
                                <i class="fas fa-home me-1"></i>Dashboard
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" asp-page="/Expenses/Index">
                                <i class="fas fa-receipt me-1"></i>Expenses
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" asp-page="/Categories/Index">
                                <i class="fas fa-tags me-1"></i>Categories
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>

    <div class="container-fluid mt-4">
        <main role="main" class="pb-3">
            @RenderBody()
        </main>
    </div>

    <footer class="footer mt-auto py-3 bg-light border-top">
        <div class="container-fluid text-center">
            <span class="text-muted">© @DateTime.Now.Year Expense Tracker</span>
        </div>
    </footer>

    <!-- jQuery 3.7.1 -->
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <!-- Bootstrap 5.3.2 JS Bundle -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <!-- Chart.js 4.4.0 -->
    <script src="https://cdn.jsdelivr.net/npm/chart.js@4.4.0/dist/chart.umd.min.js"></script>
    <!-- Custom scripts -->
    <script src="~/js/site.js" asp-append-version="true"></script>
    @await RenderSectionAsync("Scripts", required: false)
</body>
</html>
```

</details>

---

## 📝 Step 2: Create Dashboard Page (10 minutes)

### 📊 Dashboard PageModel

**🤖 Use `/generate` to create the PageModel:**

```text
/generate Create Pages/Index.cshtml.cs - Dashboard PageModel:
- Inject IExpenseService and ICategoryService
- Properties: TotalExpenses (decimal), ExpenseCount (int), CategoryCount (int)
- Properties: RecentExpenses (List<ExpenseDto>), ExpensesByCategory (Dictionary<string, decimal>)
- OnGetAsync: load totals, get top 5 recent expenses ordered by date desc
- Reference #file:Services/IExpenseService.cs for available methods
```

---

<details>
<summary>✅ Click to verify: Expected Index.cshtml.cs</summary>

```csharp
using Microsoft.AspNetCore.Mvc.RazorPages;
using ExpenseTracker.Web.Services;
using ExpenseTracker.Web.DTOs;

namespace ExpenseTracker.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IExpenseService _expenseService;
    private readonly ICategoryService _categoryService;

    public IndexModel(IExpenseService expenseService, ICategoryService categoryService)
    {
        _expenseService = expenseService;
        _categoryService = categoryService;
    }

    public decimal TotalExpenses { get; set; }
    public int ExpenseCount { get; set; }
    public int CategoryCount { get; set; }
    public List<ExpenseDto> RecentExpenses { get; set; } = new();
    public Dictionary<string, decimal> ExpensesByCategory { get; set; } = new();

    public async Task OnGetAsync()
    {
        TotalExpenses = await _expenseService.GetTotalExpensesAsync();
        
        var allExpenses = await _expenseService.GetAllExpensesAsync();
        ExpenseCount = allExpenses.Count();
        RecentExpenses = allExpenses
            .OrderByDescending(e => e.ExpenseDate)
            .Take(5)
            .ToList();

        var allCategories = await _categoryService.GetAllCategoriesAsync();
        CategoryCount = allCategories.Count();

        var grouped = await _expenseService.GetExpensesByCategoryGroupAsync();
        ExpensesByCategory = grouped.ToDictionary(
            g => g.Key, 
            g => g.Value.Sum(e => e.Amount)
        );
    }
}
```

</details>

---

### 📊 Dashboard View with Chart.js

**🤖 Use `/generate` to create the view:**

```text
/generate Create Pages/Index.cshtml - Dashboard view:
- @page and @model IndexModel directives
- Row of 3 Bootstrap cards: Total Expenses (bg-primary), Transaction Count (bg-success), Categories (bg-info)
- Font Awesome icons in each card header
- Two-column layout: Chart.js pie chart | Recent expenses table
- Table columns: Date, Description, Category (colored badge), Amount
- @section Scripts with Chart.js pie chart initialization
- Use @Html.Raw(Json.Serialize(Model.ExpensesByCategory)) to pass data to JavaScript
```

---

<details>
<summary>✅ Click to verify: Expected Index.cshtml</summary>

```cshtml
@page
@model IndexModel
@{
    ViewData["Title"] = "Dashboard";
}

<h1 class="mb-4"><i class="fas fa-tachometer-alt me-2"></i>Dashboard</h1>

<div class="row mb-4">
    <div class="col-md-4">
        <div class="card text-white bg-primary">
            <div class="card-body">
                <h5 class="card-title"><i class="fas fa-dollar-sign me-2"></i>Total Expenses</h5>
                <h2 class="card-text">@Model.TotalExpenses.ToString("C")</h2>
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div class="card text-white bg-success">
            <div class="card-body">
                <h5 class="card-title"><i class="fas fa-receipt me-2"></i>Transactions</h5>
                <h2 class="card-text">@Model.ExpenseCount</h2>
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div class="card text-white bg-info">
            <div class="card-body">
                <h5 class="card-title"><i class="fas fa-tags me-2"></i>Categories</h5>
                <h2 class="card-text">@Model.CategoryCount</h2>
            </div>
        </div>
    </div>
</div>

<div class="row">
    <div class="col-md-6">
        <div class="card">
            <div class="card-header">
                <h5><i class="fas fa-chart-pie me-2"></i>Expenses by Category</h5>
            </div>
            <div class="card-body">
                <canvas id="categoryChart"></canvas>
            </div>
        </div>
    </div>
    <div class="col-md-6">
        <div class="card">
            <div class="card-header">
                <h5><i class="fas fa-clock me-2"></i>Recent Expenses</h5>
            </div>
            <div class="card-body">
                <table class="table table-hover">
                    <thead>
                        <tr>
                            <th>Date</th>
                            <th>Description</th>
                            <th>Category</th>
                            <th class="text-end">Amount</th>
                        </tr>
                    </thead>
                    <tbody>
                        @foreach (var expense in Model.RecentExpenses)
                        {
                            <tr>
                                <td>@expense.ExpenseDate.ToString("MM/dd/yyyy")</td>
                                <td>@expense.Description</td>
                                <td>
                                    <span class="badge" style="background-color: @expense.CategoryColor">
                                        @expense.CategoryName
                                    </span>
                                </td>
                                <td class="text-end">@expense.Amount.ToString("C")</td>
                            </tr>
                        }
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>

@section Scripts {
    <script>
        const ctx = document.getElementById('categoryChart').getContext('2d');
        const categoryData = @Html.Raw(Json.Serialize(Model.ExpensesByCategory));

        new Chart(ctx, {
            type: 'pie',
            data: {
                labels: Object.keys(categoryData),
                datasets: [{
                    data: Object.values(categoryData),
                    backgroundColor: [
                        '#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', 
                        '#9966FF', '#FF9F40', '#C9CBCF', '#7CFC00'
                    ]
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: { position: 'bottom' }
                }
            }
        });
    </script>
}
```

</details>

---

## 📝 Step 3: Create Categories CRUD Pages (10 minutes)

### 📋 Categories List Page

**🤖 Use `/generate` to create Index page:**

```text
/generate Create Pages/Categories/Index.cshtml and Index.cshtml.cs:
- PageModel: inject ICategoryService, List<CategoryDto> Categories property
- OnGetAsync: load all categories ordered by name
- View: Bootstrap card grid layout (row-cols-md-3)
- Each card shows: Name (header with color), Description, ExpenseCount badge
- Edit/Delete buttons using asp-page and asp-route-id
- "Create New" button at top
```

---

### ➕ Create Category Page

**🤖 Use `/generate` to create the form:**

```text
/generate Create Pages/Categories/Create.cshtml and Create.cshtml.cs:
- PageModel:
  * [BindProperty] Category property with CreateCategoryDto
  * OnGet: empty (just display form)
  * OnPostAsync: validate, call CreateCategoryAsync, redirect to Index
- View:
  * Bootstrap card layout, centered (col-md-6)
  * Form fields: Name (required), Description (textarea), Icon (text), Color (color picker)
  * asp-for and asp-validation-for Tag Helpers
  * Submit and Back to List buttons
  * @section Scripts with _ValidationScriptsPartial
```

---

### ✏️ Edit Category Page

**🤖 Use `/generate` to create Edit page:**

```text
/generate Create Pages/Categories/Edit.cshtml and Edit.cshtml.cs:
- @page "{id:guid}" route constraint
- PageModel:
  * [BindProperty] Category property with UpdateCategoryDto
  * OnGetAsync(Guid id): load category, populate form, return NotFound if missing
  * OnPostAsync(Guid id): validate, call UpdateCategoryAsync, redirect to Index
- View: same form layout as Create, pre-populated with existing values
```

---

### 🗑️ Delete Category Page

**🤖 Use `/generate` to create Delete page:**

```text
/generate Create Pages/Categories/Delete.cshtml and Delete.cshtml.cs:
- @page "{id:guid}" route constraint
- PageModel:
  * CategoryDto Category property (for display)
  * OnGetAsync(Guid id): load category for confirmation display
  * OnPostAsync(Guid id): call DeleteCategoryAsync, redirect to Index
- View:
  * Warning alert about deletion
  * Display category details (Name, Description, ExpenseCount)
  * Confirm Delete button (btn-danger) and Cancel button
```

---

<details>
<summary>✅ Click to verify: Expected Categories/Index.cshtml</summary>

```cshtml
@page
@model ExpenseTracker.Web.Pages.Categories.IndexModel
@{
    ViewData["Title"] = "Categories";
}

<div class="container-fluid">
    <div class="d-flex justify-content-between align-items-center mb-4">
        <h1><i class="fas fa-tags me-2"></i>Categories</h1>
        <a asp-page="Create" class="btn btn-primary">
            <i class="fas fa-plus me-2"></i>Create New
        </a>
    </div>

    @if (Model.Categories.Any())
    {
        <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
            @foreach (var category in Model.Categories)
            {
                <div class="col">
                    <div class="card h-100">
                        <div class="card-header" style="background-color: @category.Color; color: white;">
                            @if (!string.IsNullOrEmpty(category.Icon))
                            {
                                <i class="@category.Icon me-2"></i>
                            }
                            <strong>@category.Name</strong>
                        </div>
                        <div class="card-body">
                            <p class="card-text text-muted">
                                @(category.Description ?? "No description")
                            </p>
                            <span class="badge bg-secondary">@category.ExpenseCount expense(s)</span>
                        </div>
                        <div class="card-footer bg-transparent">
                            <div class="btn-group w-100">
                                <a asp-page="Edit" asp-route-id="@category.Id" class="btn btn-outline-primary">
                                    <i class="fas fa-edit"></i> Edit
                                </a>
                                <a asp-page="Delete" asp-route-id="@category.Id" class="btn btn-outline-danger">
                                    <i class="fas fa-trash"></i> Delete
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            }
        </div>
    }
    else
    {
        <div class="card">
            <div class="card-body text-center py-5">
                <i class="fas fa-folder-open fa-4x text-muted mb-4"></i>
                <h4 class="text-muted">No Categories Found</h4>
                <a asp-page="Create" class="btn btn-primary btn-lg mt-3">
                    <i class="fas fa-plus me-2"></i>Create Your First Category
                </a>
            </div>
        </div>
    }
</div>
```

</details>

---

## 📝 Step 4: Create Expense Management Pages (15 minutes)

### 📝 Expenses List Page

**🤖 Use `/generate` to create the list page:**

```text
/generate Create Pages/Expenses/Index.cshtml and Index.cshtml.cs:
- PageModel: inject IExpenseService, List<ExpenseDto> Expenses property
- OnGetAsync: load all expenses ordered by date descending
- View: Bootstrap table with columns: Date, Description, Category, Amount, Actions
- "Create New" button at top
- Edit/Delete buttons for each row using asp-page and asp-route-id
- Category shown as colored badge using expense.CategoryColor
- Format: dates as MM/dd/yyyy, amounts as currency
- Show total at bottom of table
```

---

### ➕ Create Expense Page

**🤖 Use `/generate` to create the form:**

```text
/generate Create Pages/Expenses/Create.cshtml and Create.cshtml.cs:
- PageModel: 
  * Inject IExpenseService and ICategoryService
  * [BindProperty] Expense property with CreateExpenseDto
  * SelectList CategoryOptions for dropdown
  * OnGetAsync: load categories into SelectList
  * OnPostAsync: validate, call CreateExpenseAsync, redirect to Index
- View:
  * Bootstrap card layout, centered (col-md-6)
  * Form fields: Description, Amount (with $ prefix), Date (date picker), Category (dropdown)
  * asp-for and asp-validation-for Tag Helpers
  * Submit and Back to List buttons
  * @section Scripts with _ValidationScriptsPartial
```

---

### ✏️ Edit Expense Page

**🤖 Use `/generate` to create Edit page:**

```text
/generate Create Pages/Expenses/Edit.cshtml and Edit.cshtml.cs:
- @page "{id:guid}" route constraint
- PageModel:
  * Inject IExpenseService and ICategoryService
  * [BindProperty] Expense property with UpdateExpenseDto
  * SelectList CategoryOptions for dropdown
  * OnGetAsync(Guid id): load expense, populate form, load categories, return NotFound if missing
  * OnPostAsync(Guid id): validate, call UpdateExpenseAsync, redirect to Index
- View: same form layout as Create, pre-populated with existing values
```

---

### 🗑️ Delete Expense Page

**🤖 Use `/generate` to create Delete page:**

```text
/generate Create Pages/Expenses/Delete.cshtml and Delete.cshtml.cs:
- @page "{id:guid}" route constraint
- PageModel:
  * Inject IExpenseService
  * ExpenseDto Expense property (for display)
  * OnGetAsync(Guid id): load expense for confirmation display
  * OnPostAsync(Guid id): call DeleteExpenseAsync, redirect to Index
- View:
  * Warning alert about deletion
  * Display expense details (Description, Amount, Date, Category)
  * Confirm Delete button (btn-danger) and Cancel button
```

---

<details>
<summary>✅ Click to verify: Expected Expenses/Index.cshtml</summary>

```cshtml
@page
@model ExpenseTracker.Web.Pages.Expenses.IndexModel
@{
    ViewData["Title"] = "Expenses";
}

<div class="container-fluid">
    <div class="d-flex justify-content-between align-items-center mb-4">
        <h1><i class="fas fa-receipt me-2"></i>Expenses</h1>
        <a asp-page="Create" class="btn btn-primary">
            <i class="fas fa-plus me-2"></i>Create New
        </a>
    </div>

    @if (Model.Expenses.Any())
    {
        <div class="card">
            <div class="card-body">
                <div class="table-responsive">
                    <table class="table table-hover table-striped align-middle">
                        <thead class="table-light">
                            <tr>
                                <th>Date</th>
                                <th>Description</th>
                                <th>Category</th>
                                <th class="text-end">Amount</th>
                                <th class="text-center">Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            @foreach (var expense in Model.Expenses)
                            {
                                <tr>
                                    <td>@expense.ExpenseDate.ToString("MM/dd/yyyy")</td>
                                    <td>@expense.Description</td>
                                    <td>
                                        <span class="badge" style="background-color: @expense.CategoryColor">
                                            @expense.CategoryName
                                        </span>
                                    </td>
                                    <td class="text-end fw-bold">@expense.Amount.ToString("C")</td>
                                    <td class="text-center">
                                        <div class="btn-group">
                                            <a asp-page="Edit" asp-route-id="@expense.Id" 
                                               class="btn btn-sm btn-outline-primary">
                                                <i class="fas fa-edit"></i>
                                            </a>
                                            <a asp-page="Delete" asp-route-id="@expense.Id" 
                                               class="btn btn-sm btn-outline-danger">
                                                <i class="fas fa-trash"></i>
                                            </a>
                                        </div>
                                    </td>
                                </tr>
                            }
                        </tbody>
                        <tfoot class="table-light">
                            <tr>
                                <td colspan="3" class="text-end fw-bold">Total:</td>
                                <td class="text-end fw-bold fs-5">
                                    @Model.Expenses.Sum(e => e.Amount).ToString("C")
                                </td>
                                <td></td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
            </div>
        </div>
    }
    else
    {
        <div class="card">
            <div class="card-body text-center py-5">
                <i class="fas fa-inbox fa-4x text-muted mb-4"></i>
                <h4 class="text-muted">No Expenses Found</h4>
                <a asp-page="Create" class="btn btn-primary btn-lg mt-3">
                    <i class="fas fa-plus me-2"></i>Add Your First Expense
                </a>
            </div>
        </div>
    }
</div>
```

</details>

---

## 🛠️ Step 5: Troubleshooting Common Issues (5 minutes)

### ⚠️ FluentValidation Async Error

If you see this error when creating/editing:

```
Validator "CreateExpenseDtoValidator" can't be used with ASP.NET automatic 
validation as it contains asynchronous rules.
```

**🔧 Fix:** Change `MustAsync` to `Must` in your validators:

```csharp
// ❌ BEFORE (causes error)
RuleFor(x => x.CategoryId)
    .MustAsync(CategoryExists)
    .WithMessage("Category does not exist");

// ✅ AFTER (works with ASP.NET)
RuleFor(x => x.CategoryId)
    .Must(CategoryExists)
    .WithMessage("Category does not exist");

// Change the method from async to sync:
private bool CategoryExists(Guid categoryId)
{
    return _context.Categories.Any(c => c.Id == categoryId);
}
```

**Why?** ASP.NET's validation pipeline is synchronous and cannot invoke async rules.

---

## ✅ Session Verification

### 🧪 UI Testing Checklist

Run the application:

```bash
dotnet run
```

Test each feature:

- [ ] **Dashboard** displays with summary cards and chart
- [ ] **Categories**: List, Create, Edit, Delete all work
- [ ] **Expenses**: List, Create, Edit, Delete all work
- [ ] **Navigation** links work correctly
- [ ] **Forms** validate and show error messages
- [ ] **Responsive** design works on mobile

---

## 🎉 Day 1 Complete! 🎊

### ✅ What You've Built Today

**Backend:**
- ✅ EF Core entities with proper relationships
- ✅ DbContext with Fluent API and migrations
- ✅ Service layer with business logic
- ✅ FluentValidation rules
- ✅ Comprehensive unit tests (15+)

**Frontend:**
- ✅ REST API endpoints (Controllers/Minimal APIs)
- ✅ Swagger/OpenAPI documentation
- ✅ Razor Pages with Bootstrap UI
- ✅ Interactive dashboard with Chart.js
- ✅ CRUD operations for Categories
- ✅ CRUD operations for Expenses
- ✅ Responsive, professional design

### 📚 Copilot Skills Mastered

- ✅ `/generate` command for file creation
- ✅ Context selection (`#file`, `#selection`)
- ✅ Multi-language coordination (C#/Razor/JS)
- ✅ Tag Helper suggestions
- ✅ Pattern-based code generation
- ✅ Troubleshooting with `/fix`

### 🚀 Next Steps

**Optional Enhancements:**
- Add authentication with ASP.NET Core Identity
- Implement export to CSV/Excel
- Add expense reports and analytics
- Add file upload for receipts
- Implement budget tracking

---

## 🎓 Congratulations!

You've successfully built a complete ASP.NET Core expense tracker application using GitHub Copilot! You've mastered AI-assisted development from database design through to a polished web interface.

**Keep practicing and exploring - you're now a GitHub Copilot power user!** 🚀

**Happy Coding!** 💻✨
