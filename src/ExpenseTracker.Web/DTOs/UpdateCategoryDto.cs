using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Web.DTOs;

/// <summary>
/// Data transfer object for updating an existing category.
/// </summary>
public class UpdateCategoryDto
{
    /// <summary>
    /// Gets or sets the category name.
    /// </summary>
    [Required(ErrorMessage = "Category name is required")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category description.
    /// </summary>
    [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the icon identifier (e.g., "fas fa-utensils").
    /// </summary>
    [MaxLength(50, ErrorMessage = "Icon cannot exceed 50 characters")]
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets the hex color code (e.g., "#28a745").
    /// </summary>
    [MaxLength(7, ErrorMessage = "Color must be a valid hex code")]
    [RegularExpression(@"^#[0-9A-Fa-f]{6}$", ErrorMessage = "Color must be a valid hex color code (e.g., #28a745)")]
    public string Color { get; set; } = "#6c757d";
}
