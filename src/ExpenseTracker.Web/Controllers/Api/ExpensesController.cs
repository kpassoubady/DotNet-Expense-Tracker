using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Web.DTOs;
using ExpenseTracker.Web.Services;

namespace ExpenseTracker.Web.Controllers.Api;

/// <summary>
/// API controller for managing expenses.
/// </summary>
[ApiController]
[Route("api/expenses")]
[Produces("application/json")]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _expenseService;
    private readonly ILogger<ExpensesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpensesController"/> class.
    /// </summary>
    /// <param name="expenseService">The expense service.</param>
    /// <param name="logger">The logger instance.</param>
    public ExpensesController(IExpenseService expenseService, ILogger<ExpensesController> logger)
    {
        _expenseService = expenseService;
        _logger = logger;
    }

    /// <summary>
    /// Gets all expenses.
    /// </summary>
    /// <returns>A collection of expense DTOs.</returns>
    /// <response code="200">Returns the list of expenses.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExpenseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetAllExpenses()
    {
        _logger.LogInformation("GET api/expenses - Retrieving all expenses");
        var expenses = await _expenseService.GetAllExpensesAsync();
        return Ok(expenses);
    }

    /// <summary>
    /// Gets a specific expense by ID.
    /// </summary>
    /// <param name="id">The expense ID.</param>
    /// <returns>The expense DTO if found.</returns>
    /// <response code="200">Returns the requested expense.</response>
    /// <response code="404">If the expense is not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ExpenseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExpenseDto>> GetExpenseById(Guid id)
    {
        _logger.LogInformation("GET api/expenses/{ExpenseId} - Retrieving expense", id);

        var expense = await _expenseService.GetExpenseByIdAsync(id);

        if (expense == null)
        {
            _logger.LogWarning("Expense with ID {ExpenseId} not found", id);
            return NotFound(new { message = $"Expense with ID {id} not found" });
        }

        return Ok(expense);
    }

    /// <summary>
    /// Gets all expenses for a specific category.
    /// </summary>
    /// <param name="categoryId">The category ID.</param>
    /// <returns>A collection of expenses for the specified category.</returns>
    /// <response code="200">Returns the list of expenses for the category.</response>
    [HttpGet("category/{categoryId}")]
    [ProducesResponseType(typeof(IEnumerable<ExpenseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetExpensesByCategory(Guid categoryId)
    {
        _logger.LogInformation("GET api/expenses/category/{CategoryId} - Retrieving expenses by category", categoryId);
        var expenses = await _expenseService.GetExpensesByCategoryAsync(categoryId);
        return Ok(expenses);
    }

    /// <summary>
    /// Gets expenses grouped by category.
    /// </summary>
    /// <returns>A dictionary of expenses grouped by category name.</returns>
    /// <response code="200">Returns expenses grouped by category.</response>
    [HttpGet("by-category")]
    [ProducesResponseType(typeof(Dictionary<string, IEnumerable<ExpenseDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Dictionary<string, IEnumerable<ExpenseDto>>>> GetExpensesByCategory()
    {
        _logger.LogInformation("GET api/expenses/by-category - Retrieving expenses grouped by category");
        var groupedExpenses = await _expenseService.GetExpensesByCategoryGroupAsync();
        return Ok(groupedExpenses);
    }

    /// <summary>
    /// Gets the total amount of all expenses.
    /// </summary>
    /// <returns>The total amount of expenses.</returns>
    /// <response code="200">Returns the total expenses amount.</response>
    [HttpGet("total")]
    [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
    public async Task<ActionResult<decimal>> GetTotalExpenses()
    {
        _logger.LogInformation("GET api/expenses/total - Calculating total expenses");
        var total = await _expenseService.GetTotalExpensesAsync();
        return Ok(new { total });
    }

    /// <summary>
    /// Creates a new expense.
    /// </summary>
    /// <param name="dto">The expense creation data.</param>
    /// <returns>The created expense.</returns>
    /// <response code="201">Returns the newly created expense.</response>
    /// <response code="400">If the expense data is invalid.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ExpenseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExpenseDto>> CreateExpense([FromBody] CreateExpenseDto dto)
    {
        _logger.LogInformation("POST api/expenses - Creating new expense");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var expense = await _expenseService.CreateExpenseAsync(dto);

        return CreatedAtAction(
            nameof(GetExpenseById),
            new { id = expense.Id },
            expense);
    }

    /// <summary>
    /// Updates an existing expense.
    /// </summary>
    /// <param name="id">The expense ID.</param>
    /// <param name="dto">The expense update data.</param>
    /// <returns>No content if successful.</returns>
    /// <response code="204">If the expense was updated successfully.</response>
    /// <response code="400">If the expense data is invalid.</response>
    /// <response code="404">If the expense is not found.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateExpense(Guid id, [FromBody] UpdateExpenseDto dto)
    {
        _logger.LogInformation("PUT api/expenses/{ExpenseId} - Updating expense", id);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var expense = await _expenseService.UpdateExpenseAsync(id, dto);

        if (expense == null)
        {
            _logger.LogWarning("Expense with ID {ExpenseId} not found for update", id);
            return NotFound(new { message = $"Expense with ID {id} not found" });
        }

        return NoContent();
    }

    /// <summary>
    /// Deletes an expense.
    /// </summary>
    /// <param name="id">The expense ID.</param>
    /// <returns>No content if successful.</returns>
    /// <response code="204">If the expense was deleted successfully.</response>
    /// <response code="404">If the expense is not found.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteExpense(Guid id)
    {
        _logger.LogInformation("DELETE api/expenses/{ExpenseId} - Deleting expense", id);

        var deleted = await _expenseService.DeleteExpenseAsync(id);

        if (!deleted)
        {
            _logger.LogWarning("Expense with ID {ExpenseId} not found for deletion", id);
            return NotFound(new { message = $"Expense with ID {id} not found" });
        }

        return NoContent();
    }
}
