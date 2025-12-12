namespace ExpenseTracker.Web.DTOs;

/// <summary>
/// Data transfer object for reading expense information.
/// </summary>
public class ExpenseDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the expense.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the monetary amount of the expense.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the description or purpose of the expense.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date when the expense occurred.
    /// </summary>
    public DateTime ExpenseDate { get; set; }

    /// <summary>
    /// Gets or sets the foreign key to the associated category.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the name of the associated category.
    /// </summary>
    public string CategoryName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the color of the associated category.
    /// </summary>
    public string CategoryColor { get; set; } = "#6c757d";

    /// <summary>
    /// Gets or sets the timestamp when this expense was created (UTC).
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
