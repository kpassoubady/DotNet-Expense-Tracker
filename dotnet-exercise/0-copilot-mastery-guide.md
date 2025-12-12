# 🤖 GitHub Copilot Mastery Guide - Personal Expense Tracker (.NET Track)

## 🎯 **Overview**

This guide provides advanced GitHub Copilot techniques specifically designed for the Personal Expense Tracker .NET project. Use these techniques throughout your development sessions to maximize productivity and learning.

**⏱️ Reference Time: Use as needed during exercises**

---

## 🚀 **Available AI Models & When to Use Them**

### 🧠 **Model Selection Examples**

Your organization may provide different models. Here are common examples:

- **GPT-4 Turbo/GPT-4o**: Best for complex reasoning, architecture decisions
- **Claude 3.5 Sonnet**: Excellent for code review, refactoring, documentation  
- **GPT-3.5 Turbo**: Fast for simple code completion, basic prompts
- **Codex/GitHub Copilot**: Optimized for code generation, inline suggestions
- **GPT-4.5/GPT-5** (when available): Next-generation capabilities

**📝 How to Switch Models:**

```
# In Copilot Chat
/model gpt-4-turbo
/model claude-3-5-sonnet
/model gpt-3.5-turbo
```

*Note: Use whatever models are available in your organization's Copilot subscription.*

---

## 💬 **Advanced Chat Interface Techniques**

### 🎯 **Context Selection Commands**

Use these to provide precise context to Copilot:

#### **File & Code Selection**

```
# Reference specific code
#selection - Use currently selected text/code
#editor - Reference the entire active file
#file:src/ExpenseTracker/Models/Expense.cs - Reference specific file

Example:
"Analyze this #selection and suggest improvements for EF Core entity design"
```

#### **Terminal Integration**

```
#terminal_selection - Use selected terminal output
#terminal_last_command - Reference the last command run
#terminal - Reference entire terminal context

Example:
"Fix this compilation error: #terminal_selection"
"Explain what happened with: #terminal_last_command"
```

#### **Workspace Context**

```
#workspace - Reference entire project context
#git - Reference git history and changes
#problems - Reference current IDE problems/errors

Example:
"How can I improve the overall architecture of #workspace?"
```

### 🤖 **Agent Specialization**

Switch between specialized agents for different tasks:

```
@workspace - Project structure, architecture questions
@terminal - Command-line operations, build issues  
@git - Version control, branching, merging
@vscode - Editor configuration, extensions

Example:
"@workspace How should I organize my service layer structure?"
"@terminal Why did my dotnet build fail?"
"@git Help me create a feature branch for expense categories"
```

---

## 📝 **Prompt Engineering for ASP.NET Core**

### 🏗️ **Entity Generation Prompts**

```
/generate Create an Entity Framework Core entity for [EntityName] with:
- Properties: [property1: type1], [property2: type2]
- Relationships: [relationship description]
- Data annotations for validation
- Navigation properties

Example:
"Create an EF Core entity for Expense with:
- Id (Guid primary key)
- Description (string, required, max 200 chars)
- Amount (decimal, required, precision 18,2)
- Date (DateTime, required)
- CategoryId (Guid, foreign key)
- Navigation property to Category entity"
```

### 🔧 **DbContext Configuration Prompts**

```
/generate Create DbContext configuration for ExpenseTrackerContext:
- DbSet<Expense> and DbSet<Category>
- OnModelCreating with Fluent API configurations
- Connection string setup for SQLite
- Seed data method

Example:
"Configure DbContext with:
- Expense-Category one-to-many relationship
- Cascade delete enabled
- Unique constraint on Category.Name
- Default values for CreatedAt timestamps"
```

### 🎨 **Service Layer Prompts**

```
/generate Create service class for [EntityName]Service:
- Interface IExpenseService with CRUD operations
- Async methods with proper return types
- Repository pattern integration
- FluentValidation rules
- Custom exception handling

Example:
"Create ExpenseService with:
- GetAllExpensesAsync() returning List<ExpenseDto>
- CreateExpenseAsync(CreateExpenseDto) with validation
- UpdateExpenseAsync with existence check
- DeleteExpenseAsync with soft delete pattern
- GetExpensesByCategoryAsync(Guid categoryId)"
```

### 🌐 **Controller/Minimal API Prompts**

```
/generate Create REST API endpoints for Expenses:
- [HttpGet] all expenses
- [HttpGet("{id}")] single expense
- [HttpPost] create expense
- [HttpPut("{id}")] update expense
- [HttpDelete("{id}")] delete expense
- Proper status codes and error handling
- OpenAPI/Swagger annotations

Example (Minimal API):
"Create Minimal API endpoints in Program.cs for Expenses:
- GET /api/expenses - returns all expenses
- POST /api/expenses - creates new expense
- Include validation, error handling, and OpenAPI descriptions"

Example (Controller):
"Create ExpensesController with:
- Dependency injection for IExpenseService
- RESTful CRUD endpoints
- ActionResult return types
- ProducesResponseType attributes
- ModelState validation"
```

### 🎨 **Razor Pages Prompts**

```
/generate Create Razor Page for Expense management:
- PageModel with dependency injection
- OnGetAsync for loading data
- OnPostAsync for form submission
- Bootstrap 5 form layout
- Tag Helpers for model binding
- Client-side validation

Example:
"Create Create.cshtml Razor Page for Expense:
- Form with asp-for Tag Helpers
- Category dropdown from ViewData
- Date picker with Bootstrap DateTimePicker
- Client-side validation with jQuery Validation
- Success/error message display with TempData"
```

### 🧪 **Test Generation Prompts**

```
/generate Create xUnit test class for [ServiceName]:
- Arrange-Act-Assert pattern
- Mock dependencies with Moq
- Test success and failure scenarios
- Realistic test data with Bogus or AutoFixture
- Async test methods

Example:
"Create ExpenseServiceTests with:
- Mock IExpenseRepository
- Test CreateExpenseAsync success case
- Test validation failure scenarios
- Test null parameter handling
- Use FluentAssertions for readable assertions"
```

---

## 🎯 **Slash Commands Reference**

### Core Commands

```
/explain - Explain selected code or concept
/fix - Suggest fixes for errors or issues
/generate - Generate new code based on description
/doc - Generate documentation/comments
/tests - Generate unit tests
/optimize - Suggest performance improvements
/simplify - Simplify complex code
/review - Code review with best practices
```

### Advanced Usage

```
# Combine with context
/explain #selection - Explain selected EF Core configuration
/fix #terminal_last_command - Fix last build error
/tests #file:Services/ExpenseService.cs - Generate tests for service
/doc #selection - Document this Razor Page handler
```

---

## 🔥 **Advanced Techniques**

### 💡 **Multi-Step Code Generation**

Use Copilot iteratively for complex features:

```
Step 1: "Create Expense entity with basic properties"
Step 2: "Add validation attributes to Expense entity"
Step 3: "Configure Expense entity in DbContext with Fluent API"
Step 4: "Create migration for Expense table"
```

### 🎨 **Pattern Recognition & Application**

Train Copilot on your coding patterns:

```
# Show example pattern
"I want to create services following this pattern:
[paste existing service interface and implementation]

Now create IExpenseService and ExpenseService following the same pattern"
```

### 🧩 **Contextual Code Completion**

Leverage inline completions effectively:

```
# Type comment above code
// Create async method to get expenses by date range with filtering and sorting

// Copilot will suggest implementation
public async Task<List<ExpenseDto>> GetExpensesByDateRangeAsync(...)
```

### 🔍 **Smart Code Refactoring**

Use Chat for refactoring guidance:

```
"Refactor this #selection to:
- Use dependency injection properly
- Follow repository pattern
- Add async/await
- Include error handling
- Add XML documentation comments"
```

---

## 📚 **Project-Specific Copilot Patterns**

### 🏗️ **Entity Framework Core Patterns**

```
# Navigation Properties
"Add one-to-many relationship between Category and Expenses with proper navigation properties and Fluent API configuration"

# Migrations
"Create EF Core migration for adding CreatedAt and UpdatedAt audit fields to all entities"

# Queries
"Write LINQ query to get all expenses for current month grouped by category with total amounts"
```

### 🎨 **Razor Pages Patterns**

```
# Layout Templates
"Create _Layout.cshtml with Bootstrap 5 navigation, footer, and RenderBody section"

# Partial Views
"Create _ExpenseCard.cshtml partial view component displaying expense details with edit/delete buttons"

# Tag Helpers
"Create form with asp-for Tag Helpers for all Expense properties with validation messages"
```

### 🌐 **API Development Patterns**

```
# DTOs
"Create ExpenseDto, CreateExpenseDto, and UpdateExpenseDto with appropriate properties and validation attributes"

# Response Models
"Create ApiResponse<T> wrapper class for consistent API responses with data, errors, and metadata"

# Error Handling
"Create middleware for global exception handling with proper HTTP status codes and error responses"
```

### 🧪 **Testing Patterns**

```
# Arrange-Act-Assert
"Generate xUnit test for CreateExpenseAsync following AAA pattern with Moq and FluentAssertions"

# Test Data Builders
"Create ExpenseBuilder class for test data generation with fluent interface"

# Integration Tests
"Create integration test for ExpenseController using WebApplicationFactory and in-memory database"
```

---

## 🎓 **Learning Strategies**

### 📖 **Understanding AI Suggestions**

- **Always Read Generated Code**: Don't blindly accept suggestions
- **Ask "Why?"**: Request explanations for unfamiliar patterns
- **Verify Against Docs**: Cross-check with official .NET documentation
- **Test Thoroughly**: Run tests after accepting AI-generated code

### 🔄 **Iterative Improvement**

```
1. Generate initial code with Copilot
2. Review and identify improvements
3. Ask Copilot to refine specific areas
4. Add tests and verify functionality
5. Request optimization suggestions
```

### 🎯 **Best Practices**

- Use **specific, contextual prompts** for better results
- Provide **examples** when asking for pattern matching
- Leverage **#file references** for cross-file context
- Try **different models** to compare quality
- Create **custom agents** for specialized tasks
- **Experiment and iterate** - refine prompts based on results

---

## 🛠️ **Common Scenarios & Solutions**

### 🐛 **Debugging with Copilot**

```
# Explain build errors
"@terminal Why did this dotnet build fail? #terminal_last_command"

# Fix runtime exceptions
"Fix this NullReferenceException: #selection"

# Understand stack traces
"Explain this exception and suggest fixes: #terminal_selection"
```

### 📊 **Database Operations**

```
# Migration issues
"Why is my EF Core migration failing? #terminal_selection"

# Query optimization
"Optimize this LINQ query for better performance: #selection"

# Connection strings
"Create connection string for SQLite database in appsettings.json"
```

### 🎨 **Frontend Development**

```
# Bootstrap layouts
"Create responsive Bootstrap 5 card layout for displaying expenses"

# JavaScript integration
"Add Chart.js line chart for expense trends by category"

# Form validation
"Add jQuery validation for expense form with custom rules"
```

---

## 💡 **Pro Tips**

### ⚡ **Productivity Boosters**

1. **Keyboard Shortcuts**: Master Copilot shortcuts (see course materials)
2. **Snippet Templates**: Use Copilot to generate common code snippets
3. **Documentation Generation**: Auto-generate XML comments with `/doc`
4. **Code Reviews**: Use `/review` before committing code
5. **Learn Patterns**: Study generated code to learn .NET best practices

### 🎯 **Quality Assurance**

1. **Validation**: Always validate AI-generated code logic
2. **Security**: Review for security vulnerabilities (SQL injection, XSS)
3. **Performance**: Ask for optimization suggestions
4. **Testing**: Generate comprehensive test coverage
5. **Documentation**: Keep code documented with AI-generated comments

### 🔄 **Continuous Learning**

1. **Experiment**: Try different prompts for the same task
2. **Compare**: Use multiple models to see different approaches
3. **Refine**: Improve prompts based on results
4. **Share**: Document successful prompts for team use
5. **Feedback**: Rate suggestions to improve Copilot

---

## 📖 **Additional Resources**

### Official Documentation
- [GitHub Copilot Documentation](https://docs.github.com/copilot)
- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [Razor Pages Tutorial](https://docs.microsoft.com/aspnet/core/razor-pages)

### Course Materials
- [Day 1 Essentials](../day1/GitHub-Copilot-Day1-Essentials.md)
- [Day 2 Context Mastery](../day2/GitHub-Copilot-Day2-Context-Mastery.md)
- [Copilot Chat Cookbook](../day1/Copilot-Chat-Cookbook.md)
- [Slash Commands Reference](../day1/Copilot-Slash-Commands.md)

---

## 🎉 **Remember**

GitHub Copilot is a **powerful assistant**, not a replacement for understanding. Use it to:

- ✅ **Accelerate** development with intelligent suggestions
- ✅ **Learn** new patterns and best practices
- ✅ **Explore** different solutions to problems
- ✅ **Reduce** boilerplate and repetitive code
- ✅ **Focus** on business logic and architecture

But always:

- ⚠️ **Review** all generated code carefully
- ⚠️ **Test** thoroughly before deployment
- ⚠️ **Understand** what the code does
- ⚠️ **Maintain** code quality standards
- ⚠️ **Learn** from AI suggestions

**Happy Coding with AI!** 🚀
