using ExpenseTracker.Web.DTOs;
using ExpenseTracker.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExpenseTracker.Web.Pages.Categories;

/// <summary>
/// Page model for deleting a category.
/// </summary>
public class DeleteModel : PageModel
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<DeleteModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteModel"/> class.
    /// </summary>
    /// <param name="categoryService">The category service.</param>
    /// <param name="logger">The logger instance.</param>
    public DeleteModel(ICategoryService categoryService, ILogger<DeleteModel> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    /// <summary>
    /// Gets or sets the category to be deleted.
    /// </summary>
    public CategoryDto? Category { get; set; }

    /// <summary>
    /// Handles GET requests to load the category for deletion confirmation.
    /// </summary>
    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        Category = await _categoryService.GetCategoryByIdAsync(id);
        if (Category == null)
        {
            return NotFound();
        }

        return Page();
    }

    /// <summary>
    /// Handles POST requests to delete the category.
    /// </summary>
    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        try
        {
            _logger.LogInformation("Deleting category {Id}", id);
            var result = await _categoryService.DeleteCategoryAsync(id);
            
            if (!result)
            {
                return NotFound();
            }

            _logger.LogInformation("Category deleted successfully");
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting category {Id}", id);
            Category = await _categoryService.GetCategoryByIdAsync(id);
            ModelState.AddModelError(string.Empty, "An error occurred while deleting the category.");
            return Page();
        }
    }
}
