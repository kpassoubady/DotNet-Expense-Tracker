using ExpenseTracker.Web.DTOs;
using ExpenseTracker.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExpenseTracker.Web.Pages.Categories;

/// <summary>
/// Page model for creating a new category.
/// </summary>
public class CreateModel : PageModel
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CreateModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateModel"/> class.
    /// </summary>
    /// <param name="categoryService">The category service.</param>
    /// <param name="logger">The logger instance.</param>
    public CreateModel(ICategoryService categoryService, ILogger<CreateModel> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    /// <summary>
    /// Gets or sets the category data for creation.
    /// </summary>
    [BindProperty]
    public CreateCategoryDto Category { get; set; } = new();

    /// <summary>
    /// Handles GET requests.
    /// </summary>
    public void OnGet()
    {
    }

    /// <summary>
    /// Handles POST requests to create a new category.
    /// </summary>
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            _logger.LogInformation("Creating new category: {Name}", Category.Name);
            await _categoryService.CreateCategoryAsync(Category);
            _logger.LogInformation("Category created successfully");
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating category");
            ModelState.AddModelError(string.Empty, "An error occurred while creating the category.");
            return Page();
        }
    }
}
