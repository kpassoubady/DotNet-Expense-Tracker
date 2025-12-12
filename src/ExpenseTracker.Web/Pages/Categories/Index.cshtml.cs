using ExpenseTracker.Web.DTOs;
using ExpenseTracker.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExpenseTracker.Web.Pages.Categories;

/// <summary>
/// Page model for displaying all categories.
/// </summary>
public class IndexModel : PageModel
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<IndexModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="IndexModel"/> class.
    /// </summary>
    /// <param name="categoryService">The category service.</param>
    /// <param name="logger">The logger instance.</param>
    public IndexModel(ICategoryService categoryService, ILogger<IndexModel> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    /// <summary>
    /// Gets or sets the list of all categories.
    /// </summary>
    public List<CategoryDto> Categories { get; set; } = new();

    /// <summary>
    /// Handles GET requests and loads all categories.
    /// </summary>
    public async Task OnGetAsync()
    {
        try
        {
            _logger.LogInformation("Loading all categories");

            var categories = await _categoryService.GetAllCategoriesAsync();
            
            Categories = categories
                .OrderBy(c => c.Name)
                .ToList();

            _logger.LogInformation("Loaded {Count} categories", Categories.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading categories");
            throw;
        }
    }
}
