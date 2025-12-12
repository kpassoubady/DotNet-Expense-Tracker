using ExpenseTracker.Web.DTOs;
using ExpenseTracker.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExpenseTracker.Web.Pages;

/// <summary>
/// Dashboard page model displaying expense overview and statistics.
/// </summary>
public class IndexModel : PageModel
{
    private readonly IExpenseService _expenseService;
    private readonly ICategoryService _categoryService;
    private readonly ILogger<IndexModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="IndexModel"/> class.
    /// </summary>
    /// <param name="expenseService">The expense service.</param>
    /// <param name="categoryService">The category service.</param>
    /// <param name="logger">The logger instance.</param>
    public IndexModel(
        IExpenseService expenseService,
        ICategoryService categoryService,
        ILogger<IndexModel> logger)
    {
        _expenseService = expenseService;
        _categoryService = categoryService;
        _logger = logger;
    }

    /// <summary>
    /// Gets or sets the total amount of all expenses.
    /// </summary>
    public decimal TotalExpenses { get; set; }

    /// <summary>
    /// Gets or sets the total count of expenses.
    /// </summary>
    public int ExpenseCount { get; set; }

    /// <summary>
    /// Gets or sets the total count of categories.
    /// </summary>
    public int CategoryCount { get; set; }

    /// <summary>
    /// Gets or sets the list of recent expenses (top 5).
    /// </summary>
    public List<ExpenseDto> RecentExpenses { get; set; } = new();

    /// <summary>
    /// Gets or sets expenses grouped by category name.
    /// </summary>
    public Dictionary<string, IEnumerable<ExpenseDto>> ExpensesByCategory { get; set; } = new();

    /// <summary>
    /// Handles GET requests and loads dashboard data.
    /// </summary>
    public async Task OnGetAsync()
    {
        try
        {
            _logger.LogInformation("Loading dashboard data");

            // Load total expenses amount
            TotalExpenses = await _expenseService.GetTotalExpensesAsync();

            // Load all expenses
            var allExpenses = await _expenseService.GetAllExpensesAsync();
            ExpenseCount = allExpenses.Count();

            // Get top 5 recent expenses ordered by ExpenseDate descending
            RecentExpenses = allExpenses
                .OrderByDescending(e => e.ExpenseDate)
                .ThenByDescending(e => e.CreatedAt)
                .Take(5)
                .ToList();

            // Load expenses grouped by category
            ExpensesByCategory = await _expenseService.GetExpensesByCategoryGroupAsync();

            // Load category count
            var allCategories = await _categoryService.GetAllCategoriesAsync();
            CategoryCount = allCategories.Count();

            _logger.LogInformation(
                "Dashboard loaded successfully: {ExpenseCount} expenses, {CategoryCount} categories, Total: {TotalExpenses:C}",
                ExpenseCount,
                CategoryCount,
                TotalExpenses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading dashboard data");
            throw;
        }
    }
}
