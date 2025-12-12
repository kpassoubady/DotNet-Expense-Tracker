# Test Architect 🧪 (Test Generation Mastery)

## The Challenge

Become a testing expert using Copilot's test generation capabilities.

## Round 1: Basic Test Generation

**Setup**: Given this service:

```csharp
public class EmailValidator
{
    private static readonly Regex EmailPattern = 
        new(@"^[A-Za-z0-9+_.-]+@(.+)$", RegexOptions.Compiled);
    
    public bool IsValid(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }
        return EmailPattern.IsMatch(email);
    }
    
    public string GetDomain(string email)
    {
        if (!IsValid(email))
        {
            throw new ArgumentException("Invalid email", nameof(email));
        }
        return email[(email.IndexOf('@') + 1)..];
    }
}
```

**Task**: Use `/tests` and refine with follow-up prompts

```text
/tests Generate xUnit tests with edge cases
```

```text
Add Theory tests for multiple valid and invalid email formats
```

**Scoring**:
- ✅ 2 points: Tests happy path
- ✅ 2 points: Tests null/empty inputs
- ✅ 2 points: Tests boundary cases
- ✅ 2 points: Uses `[Theory]` with `[InlineData]`

---

## Round 2: Mock Master

**Setup**: A service with dependencies:

```csharp
public class OrderService
{
    private readonly IOrderRepository _repository;
    private readonly IEmailService _emailService;
    
    public OrderService(IOrderRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }
    
    public async Task<Order> CreateOrderAsync(OrderRequest request)
    {
        var order = new Order(request);
        var saved = await _repository.SaveAsync(order);
        await _emailService.SendConfirmationAsync(saved);
        return saved;
    }
}
```

**Task**: Generate tests with Moq mocks

```text
Generate xUnit tests for OrderService using Moq to mock IOrderRepository and IEmailService
```

**Scoring**:
- ✅ 2 points: Properly mocks dependencies
- ✅ 2 points: Verifies mock interactions
- ✅ 2 points: Tests exception scenarios

---

## Round 3: Test-Driven Development (TDD)

**Challenge**: Write tests FIRST, then ask Copilot to implement

**Step 1**: Write test specification
```text
Generate xUnit tests for a ShoppingCart class that should:
- Add items with quantity
- Remove items
- Calculate total price
- Apply percentage discount
- Throw exception for negative quantities
```

**Step 2**: Ask Copilot to implement the class
```text
Based on these tests, implement the ShoppingCart class that makes all tests pass
```

**Scoring**:
- ✅ 3 points: All tests pass
- ✅ 2 points: Implementation follows test contracts
- ✅ 1 point: No test modifications needed

---

## Round 4: Integration Test Challenge

**Task**: Generate integration tests for a REST controller

```csharp
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(Guid id) { ... }
    
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] CreateUserRequest request) { ... }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id) { ... }
}
```

```text
Generate WebApplicationFactory integration tests for this controller including:
- Success scenarios
- Not found scenarios (404)
- Validation failure scenarios (400)
- Content type verification
```

---

## Bonus: Coverage Detective

**Task**: Identify untested code paths

```text
Analyze #file:OrderService.cs and identify which code paths are NOT covered by #file:OrderServiceTests.cs
```

**Then**: Generate tests for missing coverage

---

## Learning Outcomes

- Generate comprehensive unit tests
- Use mocking frameworks effectively (Moq)
- Practice TDD workflow with AI
- Create integration tests with WebApplicationFactory
- Identify and fill coverage gaps
