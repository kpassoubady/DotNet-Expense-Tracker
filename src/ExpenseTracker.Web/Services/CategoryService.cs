using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Web.Data;
using ExpenseTracker.Web.DTOs;
using ExpenseTracker.Web.Models;

namespace ExpenseTracker.Web.Services;

/// <summary>
/// Service implementation for managing category operations.
/// </summary>
public class CategoryService : ICategoryService
{
    private readonly ExpenseTrackerContext _context;
    private readonly ILogger<CategoryService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger instance.</param>
    public CategoryService(ExpenseTrackerContext context, ILogger<CategoryService> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        try
        {
            _logger.LogInformation("Retrieving all categories");

            var categories = await _context.Categories
                .Include(c => c.Expenses)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return categories.Select(MapToDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all categories");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<CategoryDto?> GetCategoryByIdAsync(Guid id)
    {
        try
        {
            _logger.LogInformation("Retrieving category with ID: {CategoryId}", id);

            var category = await _context.Categories
                .Include(c => c.Expenses)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                _logger.LogWarning("Category with ID {CategoryId} not found", id);
                return null;
            }

            return MapToDto(category);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving category with ID: {CategoryId}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
    {
        try
        {
            _logger.LogInformation("Creating new category: {CategoryName}", dto.Name);

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Icon = dto.Icon,
                Color = dto.Color
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Created category with ID: {CategoryId}", category.Id);
            return MapToDto(category);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating category: {CategoryName}", dto.Name);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<CategoryDto?> UpdateCategoryAsync(Guid id, UpdateCategoryDto dto)
    {
        try
        {
            _logger.LogInformation("Updating category with ID: {CategoryId}", id);

            var category = await _context.Categories
                .Include(c => c.Expenses)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                _logger.LogWarning("Category with ID {CategoryId} not found for update", id);
                return null;
            }

            category.Name = dto.Name;
            category.Description = dto.Description;
            category.Icon = dto.Icon;
            category.Color = dto.Color;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Updated category with ID: {CategoryId}", id);
            return MapToDto(category);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating category with ID: {CategoryId}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        try
        {
            _logger.LogInformation("Deleting category with ID: {CategoryId}", id);

            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                _logger.LogWarning("Category with ID {CategoryId} not found for deletion", id);
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Deleted category with ID: {CategoryId}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting category with ID: {CategoryId}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> CategoryExistsAsync(Guid id)
    {
        try
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if category exists with ID: {CategoryId}", id);
            throw;
        }
    }

    /// <summary>
    /// Maps a Category entity to a CategoryDto.
    /// </summary>
    /// <param name="category">The category entity to map.</param>
    /// <returns>The mapped category DTO.</returns>
    private static CategoryDto MapToDto(Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Icon = category.Icon,
            Color = category.Color,
            ExpenseCount = category.Expenses?.Count ?? 0,
            CreatedAt = category.CreatedAt
        };
    }
}
