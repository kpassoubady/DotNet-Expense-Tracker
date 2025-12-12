using ExpenseTracker.Web.DTOs;
using ExpenseTracker.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseTracker.Web.Pages.Expenses;

/// <summary>
/// Page model for editing an existing expense.
/// </summary>
public class EditModel : PageModel
{
    private readonly IExpenseService _expenseService;
    private readonly ICategoryService _categoryService;
    private readonly ILogger<EditModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="EditModel"/> class.
    /// </summary>
    /// <param name="expenseService">The expense service.</param>
    /// <param name="categoryService">The category service.</param>
    /// <param name="logger">The logger instance.</param>
    public EditModel(IExpenseService expenseService, ICategoryService categoryService, ILogger<EditModel> logger)
    {
        _expenseService = expenseService;
        _categoryService = categoryService;
        _logger = logger;
    }

    /// <summary>
    /// Gets or sets the expense data for editing.
    /// </summary>
    [BindProperty]
    public UpdateExpenseDto Expense { get; set; } = new();

    /// <summary>
    /// Gets or sets the category options for the dropdown.
    /// </summary>
    public SelectList CategoryOptions { get; set; } = new SelectList(Enumerable.Empty<SelectListItem>());

    /// <summary>
    /// Gets or sets the expense ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Handles GET requests to load the expense for editing.
    /// </summary>
    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        Id = id;
        
        var expense = await _expenseService.GetExpenseByIdAsync(id);
        if (expense == null)
        {
            return NotFound();
        }

        Expense = new UpdateExpenseDto
        {
            Description = expense.Description,
            Amount = expense.Amount,
            ExpenseDate = expense.ExpenseDate,
            CategoryId = expense.CategoryId
        };

        await LoadCategoriesAsync();
        return Page();
    }

    /// <summary>
    /// Handles POST requests to update the expense.
    /// </summary>
    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        if (!ModelState.IsValid)
        {
            await LoadCategoriesAsync();
            return Page();
        }

        try
        {
            _logger.LogInformation("Updating expense {Id}", id);
            var result = await _expenseService.UpdateExpenseAsync(id, Expense);
            
            if (result == null)
            {
                return NotFound();
            }

            _logger.LogInformation("Expense updated successfully");
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating expense {Id}", id);
            await LoadCategoriesAsync();
            ModelState.AddModelError(string.Empty, "An error occurred while updating the expense.");
            return Page();
        }
    }

    private async Task LoadCategoriesAsync()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        CategoryOptions = new SelectList(categories.OrderBy(c => c.Name), "Id", "Name");
    }
}
