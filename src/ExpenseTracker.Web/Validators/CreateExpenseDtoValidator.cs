using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Web.DTOs;
using ExpenseTracker.Web.Data;

namespace ExpenseTracker.Web.Validators;

/// <summary>
/// Validator for CreateExpenseDto using FluentValidation.
/// </summary>
public class CreateExpenseDtoValidator : AbstractValidator<CreateExpenseDto>
{
    private readonly ExpenseTrackerContext _context;

    public CreateExpenseDtoValidator(ExpenseTrackerContext context)
    {
        _context = context;

        // Amount validation
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0")
            .PrecisionScale(18, 2, false)
            .WithMessage("Amount cannot have more than 2 decimal places");

        // Description validation
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MaximumLength(255)
            .WithMessage("Description cannot exceed 255 characters");

        // ExpenseDate validation
        RuleFor(x => x.ExpenseDate)
            .NotEmpty()
            .WithMessage("Expense date is required")
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage("Expense date cannot be in the future");

        // CategoryId validation
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("Category is required")
            .Must(CategoryExists)
            .WithMessage("Selected category does not exist");
    }

    /// <summary>
    /// Validates that the category exists in the database.
    /// </summary>
    /// <param name="categoryId">The category ID to validate.</param>
    /// <returns>True if the category exists; otherwise, false.</returns>
    private bool CategoryExists(Guid categoryId)
    {
        if (categoryId == Guid.Empty)
            return false;

        return _context.Categories.Any(c => c.Id == categoryId);
    }
}
