namespace ExpenseTracker.Web.DTOs;

/// <summary>
/// Data transfer object for reading category information.
/// </summary>
public class CategoryDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the category.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the category name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the icon identifier (e.g., "fas fa-utensils").
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets the hex color code (e.g., "#28a745").
    /// </summary>
    public string Color { get; set; } = "#6c757d";

    /// <summary>
    /// Gets or sets the count of expenses in this category.
    /// </summary>
    public int ExpenseCount { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when this category was created (UTC).
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
