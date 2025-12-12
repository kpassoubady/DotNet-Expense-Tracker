using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Web.DTOs;
using ExpenseTracker.Web.Services;

namespace ExpenseTracker.Web.Controllers.Api;

/// <summary>
/// API controller for managing expense categories.
/// </summary>
[ApiController]
[Route("api/categories")]
[Produces("application/json")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoriesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoriesController"/> class.
    /// </summary>
    /// <param name="categoryService">The category service.</param>
    /// <param name="logger">The logger instance.</param>
    public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    /// <summary>
    /// Gets all categories.
    /// </summary>
    /// <returns>A collection of category DTOs.</returns>
    /// <response code="200">Returns the list of categories.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories()
    {
        _logger.LogInformation("GET api/categories - Retrieving all categories");
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    /// <summary>
    /// Gets a specific category by ID.
    /// </summary>
    /// <param name="id">The category ID.</param>
    /// <returns>The category DTO if found.</returns>
    /// <response code="200">Returns the requested category.</response>
    /// <response code="404">If the category is not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDto>> GetCategoryById(Guid id)
    {
        _logger.LogInformation("GET api/categories/{CategoryId} - Retrieving category", id);

        var category = await _categoryService.GetCategoryByIdAsync(id);

        if (category == null)
        {
            _logger.LogWarning("Category with ID {CategoryId} not found", id);
            return NotFound(new { message = $"Category with ID {id} not found" });
        }

        return Ok(category);
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="dto">The category creation data.</param>
    /// <returns>The created category.</returns>
    /// <response code="201">Returns the newly created category.</response>
    /// <response code="400">If the category data is invalid.</response>
    [HttpPost]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CreateCategoryDto dto)
    {
        _logger.LogInformation("POST api/categories - Creating new category");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = await _categoryService.CreateCategoryAsync(dto);

        return CreatedAtAction(
            nameof(GetCategoryById),
            new { id = category.Id },
            category);
    }

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="id">The category ID.</param>
    /// <param name="dto">The category update data.</param>
    /// <returns>No content if successful.</returns>
    /// <response code="204">If the category was updated successfully.</response>
    /// <response code="400">If the category data is invalid.</response>
    /// <response code="404">If the category is not found.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryDto dto)
    {
        _logger.LogInformation("PUT api/categories/{CategoryId} - Updating category", id);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = await _categoryService.UpdateCategoryAsync(id, dto);

        if (category == null)
        {
            _logger.LogWarning("Category with ID {CategoryId} not found for update", id);
            return NotFound(new { message = $"Category with ID {id} not found" });
        }

        return NoContent();
    }

    /// <summary>
    /// Deletes a category.
    /// </summary>
    /// <param name="id">The category ID.</param>
    /// <returns>No content if successful.</returns>
    /// <response code="204">If the category was deleted successfully.</response>
    /// <response code="404">If the category is not found.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        _logger.LogInformation("DELETE api/categories/{CategoryId} - Deleting category", id);

        var deleted = await _categoryService.DeleteCategoryAsync(id);

        if (!deleted)
        {
            _logger.LogWarning("Category with ID {CategoryId} not found for deletion", id);
            return NotFound(new { message = $"Category with ID {id} not found" });
        }

        return NoContent();
    }
}