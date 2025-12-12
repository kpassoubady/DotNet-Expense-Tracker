using ExpenseTracker.Web.DTOs;
using ExpenseTracker.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExpenseTracker.Web.Pages.Categories;

/// <summary>
/// Page model for editing an existing category.
/// </summary>
public class EditModel : PageModel
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<EditModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="EditModel"/> class.
    /// </summary>
    /// <param name="categoryService">The category service.</param>
    /// <param name="logger">The logger instance.</param>
    public EditModel(ICategoryService categoryService, ILogger<EditModel> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    /// <summary>
    /// Gets or sets the category data for editing.
    /// </summary>
    [BindProperty]
    public UpdateCategoryDto Category { get; set; } = new();

    /// <summary>
    /// Gets or sets the category ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Handles GET requests to load the category for editing.
    /// </summary>
    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        Id = id;
        
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        Category = new UpdateCategoryDto
        {
            Name = category.Name,
            Description = category.Description,
            Icon = category.Icon,
            Color = category.Color
        };

        return Page();
    }

    /// <summary>
    /// Handles POST requests to update the category.
    /// </summary>
    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            _logger.LogInformation("Updating category {Id}", id);
            var result = await _categoryService.UpdateCategoryAsync(id, Category);
            
            if (result == null)
            {
                return NotFound();
            }

            _logger.LogInformation("Category updated successfully");
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating category {Id}", id);
            ModelState.AddModelError(string.Empty, "An error occurred while updating the category.");
            return Page();
        }
    }
}
