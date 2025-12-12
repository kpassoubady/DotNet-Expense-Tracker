using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace ExpenseTracker.Web.DTOs;

/// <summary>
/// Data transfer object for creating a new expense.
/// </summary>
public class CreateExpenseDto
{
    /// <summary>
    /// Gets or sets the monetary amount of the expense.
    /// Must be greater than 0.
    /// </summary>
    /// <example>45.99</example>
    [Required(ErrorMessage = "Amount is required")]
    [Range(0.01, 999999.99, ErrorMessage = "Amount must be between 0.01 and 999,999.99")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the description or purpose of the expense.
    /// </summary>
    /// <example>Lunch at restaurant</example>
    [Required(ErrorMessage = "Description is required")]
    [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date when the expense occurred.
    /// </summary>
    /// <example>2025-12-12</example>
    [Required(ErrorMessage = "Expense date is required")]
    public DateTime ExpenseDate { get; set; } = DateTime.Today;

    /// <summary>
    /// Gets or sets the foreign key to the associated category.
    /// </summary>
    /// <example>11111111-1111-1111-1111-111111111111</example>
    [Required(ErrorMessage = "Category is required")]
    public Guid CategoryId { get; set; }
}
