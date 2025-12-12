using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Web.Models;

/// <summary>
/// Represents an expense transaction in the expense tracker system.
/// </summary>
public class Expense
{
    /// <summary>
    /// Gets or sets the unique identifier for the expense.
    /// </summary>
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the monetary amount of the expense.
    /// Must be greater than 0 and stored with precision of 18 digits and 2 decimal places.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the description or purpose of the expense.
    /// Maximum length: 255 characters.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date when the expense occurred.
    /// Defaults to today's date.
    /// </summary>
    [Required]
    public DateTime ExpenseDate { get; set; } = DateTime.Today;

    /// <summary>
    /// Gets or sets the foreign key to the associated category.
    /// </summary>
    [Required]
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the associated <see cref="Models.Category"/>.
    /// </summary>
    [Required]
    public Category Category { get; set; } = null!;

    /// <summary>
    /// Gets or sets the timestamp when this expense was created (UTC).
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the timestamp when this expense was last updated (UTC).
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets the name of the associated category, or "Unknown" if category is not loaded.
    /// This is a computed property and is not mapped to the database.
    /// </summary>
    [NotMapped]
    public string CategoryName => Category?.Name ?? "Unknown";

    /// <summary>
    /// Initializes a new instance of the <see cref="Expense"/> class.
    /// </summary>
    public Expense()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Expense"/> class with specified values.
    /// </summary>
    /// <param name="amount">The monetary amount of the expense.</param>
    /// <param name="description">The description or purpose of the expense.</param>
    /// <param name="expenseDate">The date when the expense occurred.</param>
    /// <param name="categoryId">The foreign key to the associated category.</param>
    public Expense(decimal amount, string description, DateTime expenseDate, Guid categoryId)
    {
        Amount = amount;
        Description = description;
        ExpenseDate = expenseDate;
        CategoryId = categoryId;
    }
}