using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Web.Models;

public class Category
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Description { get; set; }

    [MaxLength(50)]
    public string? Icon { get; set; }

    [MaxLength(7)]
    [RegularExpression(@"^#[0-9A-Fa-f]{6}$")]
    public string Color { get; set; } = "#6c757d";

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation property: Category (1) -> Expense (many)
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public Category() { }

    public Category(string name, string? description = null, string? icon = null, string? color = null)
    {
        Name = name;
        Description = description;
        Icon = icon;
        if (!string.IsNullOrWhiteSpace(color))
            Color = color;
    }
}