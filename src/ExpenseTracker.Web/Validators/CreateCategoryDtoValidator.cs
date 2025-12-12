using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Web.DTOs;
using ExpenseTracker.Web.Data;

namespace ExpenseTracker.Web.Validators;

/// <summary>
/// Validator for CreateCategoryDto using FluentValidation.
/// </summary>
public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    private readonly ExpenseTrackerContext _context;

    public CreateCategoryDtoValidator(ExpenseTrackerContext context)
    {
        _context = context;

        // Name validation
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Category name is required")
            .MaximumLength(100)
            .WithMessage("Category name cannot exceed 100 characters")
            .Must(BeUniqueName)
            .WithMessage("A category with this name already exists");

        // Description validation (optional)
        RuleFor(x => x.Description)
            .MaximumLength(255)
            .WithMessage("Description cannot exceed 255 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        // Color validation (optional, but must match hex pattern if provided)
        RuleFor(x => x.Color)
            .Matches(@"^#[0-9A-Fa-f]{6}$")
            .WithMessage("Color must be a valid hex color code (e.g., #28a745)")
            .When(x => !string.IsNullOrWhiteSpace(x.Color));

        // Icon validation (optional)
        RuleFor(x => x.Icon)
            .MaximumLength(50)
            .WithMessage("Icon cannot exceed 50 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.Icon));
    }

    /// <summary>
    /// Validates that the category name is unique in the database.
    /// </summary>
    /// <param name="name">The category name to validate.</param>
    /// <returns>True if the name is unique; otherwise, false.</returns>
    private bool BeUniqueName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return true;

        return !_context.Categories
            .Any(c => c.Name.ToLower() == name.ToLower());
    }
}
