using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Web.Data;

namespace ExpenseTracker.Web.Pages;

public class DbTestModel : PageModel
{
    private readonly ExpenseTrackerContext _context;
    private readonly IConfiguration _configuration;
    private readonly ILogger<DbTestModel> _logger;

    public DbTestModel(
        ExpenseTrackerContext context,
        IConfiguration configuration,
        ILogger<DbTestModel> logger)
    {
        _context = context;
        _configuration = configuration;
        _logger = logger;
    }

    public bool IsConnected { get; set; }
    public int CategoryCount { get; set; }
    public int ExpenseCount { get; set; }
    public string ConnectionString { get; set; } = string.Empty;
    public string? ErrorMessage { get; set; }
    public DateTime TestTime { get; set; }

    public async Task OnGetAsync()
    {
        TestTime = DateTime.UtcNow;
        ConnectionString = _configuration.GetConnectionString("DefaultConnection") ?? "Not configured";

        try
        {
            // Test database connectivity by querying counts
            CategoryCount = await _context.Categories.CountAsync();
            ExpenseCount = await _context.Expenses.CountAsync();

            IsConnected = true;
            _logger.LogInformation("Database connection test successful. Categories: {CategoryCount}, Expenses: {ExpenseCount}",
                CategoryCount, ExpenseCount);
        }
        catch (Exception ex)
        {
            IsConnected = false;
            ErrorMessage = $"Error: {ex.Message}";
            _logger.LogError(ex, "Database connection test failed");
        }
    }
}
