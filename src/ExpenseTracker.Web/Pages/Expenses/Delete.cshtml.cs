using ExpenseTracker.Web.DTOs;
using ExpenseTracker.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExpenseTracker.Web.Pages.Expenses;

/// <summary>
/// Page model for deleting an expense.
/// </summary>
public class DeleteModel : PageModel
{
    private readonly IExpenseService _expenseService;
    private readonly ILogger<DeleteModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteModel"/> class.
    /// </summary>
    /// <param name="expenseService">The expense service.</param>
    /// <param name="logger">The logger instance.</param>
    public DeleteModel(IExpenseService expenseService, ILogger<DeleteModel> logger)
    {
        _expenseService = expenseService;
        _logger = logger;
    }

    /// <summary>
    /// Gets or sets the expense to be deleted.
    /// </summary>
    public ExpenseDto? Expense { get; set; }

    /// <summary>
    /// Handles GET requests to load the expense for deletion confirmation.
    /// </summary>
    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        Expense = await _expenseService.GetExpenseByIdAsync(id);
        if (Expense == null)
        {
            return NotFound();
        }

        return Page();
    }

    /// <summary>
    /// Handles POST requests to delete the expense.
    /// </summary>
    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        try
        {
            _logger.LogInformation("Deleting expense {Id}", id);
            var result = await _expenseService.DeleteExpenseAsync(id);
            
            if (!result)
            {
                return NotFound();
            }

            _logger.LogInformation("Expense deleted successfully");
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting expense {Id}", id);
            Expense = await _expenseService.GetExpenseByIdAsync(id);
            ModelState.AddModelError(string.Empty, "An error occurred while deleting the expense.");
            return Page();
        }
    }
}
