using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Web.Data;
using ExpenseTracker.Web.DTOs;
using ExpenseTracker.Web.Models;

namespace ExpenseTracker.Web.Services;

/// <summary>
/// Service implementation for managing expense operations.
/// </summary>
public class ExpenseService : IExpenseService
{
    private readonly ExpenseTrackerContext _context;
    private readonly ILogger<ExpenseService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpenseService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger instance.</param>
    public ExpenseService(ExpenseTrackerContext context, ILogger<ExpenseService> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ExpenseDto>> GetAllExpensesAsync()
    {
        try
        {
            _logger.LogInformation("Retrieving all expenses");

            var expenses = await _context.Expenses
                .Include(e => e.Category)
                .OrderByDescending(e => e.ExpenseDate)
                .ToListAsync();

            return expenses.Select(MapToDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all expenses");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<ExpenseDto?> GetExpenseByIdAsync(Guid id)
    {
        try
        {
            _logger.LogInformation("Retrieving expense with ID: {ExpenseId}", id);

            var expense = await _context.Expenses
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (expense == null)
            {
                _logger.LogWarning("Expense with ID {ExpenseId} not found", id);
                return null;
            }

            return MapToDto(expense);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving expense with ID: {ExpenseId}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ExpenseDto>> GetExpensesByCategoryAsync(Guid categoryId)
    {
        try
        {
            _logger.LogInformation("Retrieving expenses for category ID: {CategoryId}", categoryId);

            var expenses = await _context.Expenses
                .Include(e => e.Category)
                .Where(e => e.CategoryId == categoryId)
                .OrderByDescending(e => e.ExpenseDate)
                .ToListAsync();

            return expenses.Select(MapToDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving expenses for category ID: {CategoryId}", categoryId);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<Dictionary<string, IEnumerable<ExpenseDto>>> GetExpensesByCategoryGroupAsync()
    {
        try
        {
            _logger.LogInformation("Retrieving expenses grouped by category");

            var expenses = await _context.Expenses
                .Include(e => e.Category)
                .OrderByDescending(e => e.ExpenseDate)
                .ToListAsync();

            var grouped = expenses
                .GroupBy(e => e.Category.Name)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(MapToDto).AsEnumerable()
                );

            return grouped;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving expenses grouped by category");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<decimal> GetTotalExpensesAsync()
    {
        try
        {
            _logger.LogInformation("Calculating total expenses");

            var total = await _context.Expenses.SumAsync(e => e.Amount);

            _logger.LogInformation("Total expenses: {TotalAmount:C}", total);
            return total;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating total expenses");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<ExpenseDto> CreateExpenseAsync(CreateExpenseDto dto)
    {
        try
        {
            _logger.LogInformation("Creating new expense for category ID: {CategoryId}", dto.CategoryId);

            var expense = new Expense
            {
                Id = Guid.NewGuid(),
                Amount = dto.Amount,
                Description = dto.Description,
                ExpenseDate = dto.ExpenseDate,
                CategoryId = dto.CategoryId
            };

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            // Reload with category to populate DTO
            await _context.Entry(expense)
                .Reference(e => e.Category)
                .LoadAsync();

            _logger.LogInformation("Created expense with ID: {ExpenseId}", expense.Id);
            return MapToDto(expense);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating expense");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<ExpenseDto?> UpdateExpenseAsync(Guid id, UpdateExpenseDto dto)
    {
        try
        {
            _logger.LogInformation("Updating expense with ID: {ExpenseId}", id);

            var expense = await _context.Expenses
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (expense == null)
            {
                _logger.LogWarning("Expense with ID {ExpenseId} not found for update", id);
                return null;
            }

            expense.Amount = dto.Amount;
            expense.Description = dto.Description;
            expense.ExpenseDate = dto.ExpenseDate;
            expense.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();

            // Reload category if it changed
            await _context.Entry(expense)
                .Reference(e => e.Category)
                .LoadAsync();

            _logger.LogInformation("Updated expense with ID: {ExpenseId}", id);
            return MapToDto(expense);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating expense with ID: {ExpenseId}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> DeleteExpenseAsync(Guid id)
    {
        try
        {
            _logger.LogInformation("Deleting expense with ID: {ExpenseId}", id);

            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
            {
                _logger.LogWarning("Expense with ID {ExpenseId} not found for deletion", id);
                return false;
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Deleted expense with ID: {ExpenseId}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting expense with ID: {ExpenseId}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> ExpenseExistsAsync(Guid id)
    {
        try
        {
            return await _context.Expenses.AnyAsync(e => e.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if expense exists with ID: {ExpenseId}", id);
            throw;
        }
    }

    /// <summary>
    /// Maps an Expense entity to an ExpenseDto.
    /// </summary>
    /// <param name="expense">The expense entity to map.</param>
    /// <returns>The mapped expense DTO.</returns>
    private static ExpenseDto MapToDto(Expense expense)
    {
        return new ExpenseDto
        {
            Id = expense.Id,
            Amount = expense.Amount,
            Description = expense.Description,
            ExpenseDate = expense.ExpenseDate,
            CategoryId = expense.CategoryId,
            CategoryName = expense.Category?.Name ?? "Unknown",
            CategoryColor = expense.Category?.Color ?? "#6c757d",
            CreatedAt = expense.CreatedAt
        };
    }
}
