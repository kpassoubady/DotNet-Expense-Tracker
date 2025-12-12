using ExpenseTracker.Web.DTOs;
using ExpenseTracker.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseTracker.Web.Pages.Expenses;

/// <summary>
/// Page model for creating a new expense.
/// </summary>
public class CreateModel : PageModel
{
    private readonly IExpenseService _expenseService;
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CreateModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateModel"/> class.
    /// </summary>
    /// <param name="expenseService">The expense service.</param>
    /// <param name="categoryService">The category service.</param>
    /// <param name="logger">The logger instance.</param>
    public CreateModel(IExpenseService expenseService, ICategoryService categoryService, ILogger<CreateModel> logger)
    {
        _expenseService = expenseService;
        _categoryService = categoryService;
        _logger = logger;
    }

    /// <summary>
    /// Gets or sets the expense data for creation.
    /// </summary>
    [BindProperty]
    public CreateExpenseDto Expense { get; set; } = new();

    /// <summary>
    /// Gets or sets the category options for the dropdown.
    /// </summary>
    public SelectList CategoryOptions { get; set; } = new SelectList(Enumerable.Empty<SelectListItem>());

    /// <summary>
    /// Handles GET requests.
    /// </summary>
    public async Task OnGetAsync()
    {
        await LoadCategoriesAsync();
    }

    /// <summary>
    /// Handles POST requests to create a new expense.
    /// </summary>
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadCategoriesAsync();
            return Page();
        }

        try
        {
            _logger.LogInformation("Creating new expense: {Description}", Expense.Description);
            await _expenseService.CreateExpenseAsync(Expense);
            _logger.LogInformation("Expense created successfully");
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating expense");
            await LoadCategoriesAsync();
            ModelState.AddModelError(string.Empty, "An error occurred while creating the expense.");
            return Page();
        }
    }

    private async Task LoadCategoriesAsync()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        CategoryOptions = new SelectList(categories.OrderBy(c => c.Name), "Id", "Name");
    }
}
