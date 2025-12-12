using ExpenseTracker.Web.DTOs;

namespace ExpenseTracker.Web.Services;

/// <summary>
/// Service interface for managing expense operations.
/// </summary>
public interface IExpenseService
{
    /// <summary>
    /// Retrieves all expenses from the database.
    /// </summary>
    /// <returns>A collection of expense DTOs.</returns>
    Task<IEnumerable<ExpenseDto>> GetAllExpensesAsync();

    /// <summary>
    /// Retrieves a specific expense by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the expense.</param>
    /// <returns>The expense DTO if found; otherwise, null.</returns>
    Task<ExpenseDto?> GetExpenseByIdAsync(Guid id);

    /// <summary>
    /// Retrieves all expenses belonging to a specific category.
    /// </summary>
    /// <param name="categoryId">The unique identifier of the category.</param>
    /// <returns>A collection of expense DTOs for the specified category.</returns>
    Task<IEnumerable<ExpenseDto>> GetExpensesByCategoryAsync(Guid categoryId);

    /// <summary>
    /// Retrieves expenses grouped by category.
    /// </summary>
    /// <returns>A dictionary where the key is the category name and the value is a collection of expenses.</returns>
    Task<Dictionary<string, IEnumerable<ExpenseDto>>> GetExpensesByCategoryGroupAsync();

    /// <summary>
    /// Calculates the total amount of all expenses.
    /// </summary>
    /// <returns>The sum of all expense amounts.</returns>
    Task<decimal> GetTotalExpensesAsync();

    /// <summary>
    /// Creates a new expense.
    /// </summary>
    /// <param name="dto">The data transfer object containing the expense information.</param>
    /// <returns>The created expense DTO with generated Id and timestamps.</returns>
    Task<ExpenseDto> CreateExpenseAsync(CreateExpenseDto dto);

    /// <summary>
    /// Updates an existing expense.
    /// </summary>
    /// <param name="id">The unique identifier of the expense to update.</param>
    /// <param name="dto">The data transfer object containing the updated expense information.</param>
    /// <returns>The updated expense DTO if found; otherwise, null.</returns>
    Task<ExpenseDto?> UpdateExpenseAsync(Guid id, UpdateExpenseDto dto);

    /// <summary>
    /// Deletes an expense by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the expense to delete.</param>
    /// <returns>True if the expense was deleted; otherwise, false.</returns>
    Task<bool> DeleteExpenseAsync(Guid id);

    /// <summary>
    /// Checks whether an expense exists with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the expense.</param>
    /// <returns>True if the expense exists; otherwise, false.</returns>
    Task<bool> ExpenseExistsAsync(Guid id);
}
