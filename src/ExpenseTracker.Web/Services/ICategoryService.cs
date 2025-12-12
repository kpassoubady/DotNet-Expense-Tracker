using ExpenseTracker.Web.DTOs;

namespace ExpenseTracker.Web.Services;

/// <summary>
/// Service interface for managing category operations.
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// Retrieves all categories from the database.
    /// </summary>
    /// <returns>A collection of category DTOs.</returns>
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();

    /// <summary>
    /// Retrieves a specific category by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the category.</param>
    /// <returns>The category DTO if found; otherwise, null.</returns>
    Task<CategoryDto?> GetCategoryByIdAsync(Guid id);

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="dto">The data transfer object containing the category information.</param>
    /// <returns>The created category DTO with generated Id and timestamps.</returns>
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto);

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="id">The unique identifier of the category to update.</param>
    /// <param name="dto">The data transfer object containing the updated category information.</param>
    /// <returns>The updated category DTO if found; otherwise, null.</returns>
    Task<CategoryDto?> UpdateCategoryAsync(Guid id, UpdateCategoryDto dto);

    /// <summary>
    /// Deletes a category by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the category to delete.</param>
    /// <returns>True if the category was deleted; otherwise, false.</returns>
    Task<bool> DeleteCategoryAsync(Guid id);

    /// <summary>
    /// Checks whether a category exists with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the category.</param>
    /// <returns>True if the category exists; otherwise, false.</returns>
    Task<bool> CategoryExistsAsync(Guid id);
}
