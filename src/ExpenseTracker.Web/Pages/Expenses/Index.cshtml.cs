using ExpenseTracker.Web.DTOs;
using ExpenseTracker.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExpenseTracker.Web.Pages.Expenses;

/// <summary>
/// Page model for displaying all expenses.
/// </summary>
public class IndexModel : PageModel
{
    private readonly IExpenseService _expenseService;
    private readonly ILogger<IndexModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="IndexModel"/> class.
    /// </summary>
    /// <param name="expenseService">The expense service.</param>
    /// <param name="logger">The logger instance.</param>
    public IndexModel(IExpenseService expenseService, ILogger<IndexModel> logger)
    {
        _expenseService = expenseService;
        _logger = logger;
    }

    /// <summary>
    /// Gets or sets the list of all expenses.
    /// </summary>
    public List<ExpenseDto> Expenses { get; set; } = new();

    /// <summary>
    /// Handles GET requests and loads all expenses.
    /// </summary>
    public async Task OnGetAsync()
    {
        try
        {
            _logger.LogInformation("Loading all expenses");

            var expenses = await _expenseService.GetAllExpensesAsync();
            
            // Order by date descending, then by created date
            Expenses = expenses
                .OrderByDescending(e => e.ExpenseDate)
                .ThenByDescending(e => e.CreatedAt)
                .ToList();

            _logger.LogInformation("Loaded {Count} expenses", Expenses.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading expenses");
            throw;
        }
    }
}
