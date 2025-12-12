# Agent Arena 🤖 (Custom Agents & Instructions)

## The Challenge

Learn to create and use custom Copilot agents (chatmodes) for specialized assistance.

## Round 1: Create Your First Agent

**Task**: Create an ASP.NET Core expert agent

Create file: `.github/agents/aspnet-expert.agent.md`

```markdown
---
description: 'ASP.NET Core development specialist'
tools: []
---

You are an ASP.NET Core expert assistant. When helping with code:

## Your Expertise
- ASP.NET Core 10.0 and .NET 10
- Entity Framework Core 10.0
- ASP.NET Core Identity with OAuth2/JWT
- Minimal APIs and Controllers
- Testing with xUnit and Moq

## Code Style
- Use constructor injection (not [Inject] on fields)
- Prefer records for DTOs
- Use nullable reference types
- Follow REST API best practices
- Include proper exception handling

## When Generating Code
- Always include necessary using statements
- Add appropriate attributes
- Include XML documentation for public methods
- Suggest relevant tests
```

**Test it**:
```text
@workspace /agents aspnet-expert

Create a REST endpoint for user registration with validation
```

---

## Round 2: Domain-Specific Agent Battle

**Challenge**: Create agents for different roles and compare outputs

### Agent 1: Security Expert
```markdown
---
description: 'Security-focused code reviewer'
---

You are a security expert. Always check for:
- SQL injection vulnerabilities
- XSS attack vectors
- Authentication/authorization issues
- Sensitive data exposure
- OWASP Top 10 compliance
```

### Agent 2: Performance Expert
```markdown
---
description: 'Performance optimization specialist'
---

You are a performance expert. Always consider:
- Algorithm complexity (Big O)
- Memory usage and allocations
- Database query optimization (N+1, indexes)
- Caching opportunities (IMemoryCache, distributed cache)
- Async/await patterns and ConfigureAwait
```

**The Battle**: Both teams analyze the same code:

```csharp
public List<User> SearchUsers(string query)
{
    var allUsers = _userRepository.GetAll();
    return allUsers
        .Where(u => u.Name.Contains(query))
        .ToList();
}
```

**Scoring**: Which agent finds more issues?

---

## Round 3: Project Instructions

**Task**: Create project-wide Copilot instructions

Create file: `.github/copilot-instructions.md`

```markdown
# Project Coding Standards

## Architecture
This project follows Clean Architecture:
- `Domain/` - Business logic, no framework dependencies
- `Application/` - Use cases and interfaces
- `Infrastructure/` - Adapters (EF Core, REST clients)
- `WebApi/` - ASP.NET Core controllers/endpoints

## Naming Conventions
- Services: `*Service` (e.g., `OrderService`)
- Repositories: `*Repository` (e.g., `UserRepository`)
- Controllers: `*Controller` (e.g., `ProductController`)
- DTOs: `*Request`, `*Response` (e.g., `CreateUserRequest`)

## Testing
- Unit tests in `*.Tests` projects
- Integration tests in `*.IntegrationTests` projects
- Use `[Fact]` for single tests, `[Theory]` for parameterized
- Use Moq for mocking

## Database
- Use EF Core migrations in `Infrastructure/Migrations`
- Migration naming: `{Timestamp}_{Description}.cs`
```

**Test**: Generate code and verify it follows project conventions

---

## Round 4: Agent Switching Challenge

**Scenario**: Build a feature using multiple agents

1. **Use `security-expert`**: Design authentication flow
2. **Use `aspnet-expert`**: Implement the endpoints
3. **Use `performance-expert`**: Review and optimize

**Task**: Implement a rate-limited API endpoint

```text
@workspace /agents security-expert
Design a rate limiting strategy for our API
```

```text
@workspace /agents aspnet-expert  
Implement rate limiting using AspNetCoreRateLimit middleware
```

```text
@workspace /agents performance-expert
Review this rate limiting implementation for performance issues
```

---

## Bonus: Agent Creation Contest

**Challenge**: Create the most useful custom agent for your team

**Judging Criteria**:
- ✅ Solves a real team problem
- ✅ Instructions are clear and specific
- ✅ Produces consistently good output
- ✅ Other team members find it valuable

---

## Learning Outcomes

- Create custom agents for specialized tasks
- Write effective agent instructions
- Use project-wide Copilot configuration
- Switch between agents for different perspectives
- Understand how context shapes AI responses
