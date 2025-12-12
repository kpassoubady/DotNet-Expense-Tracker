# Context Master 🎯 (Context Selection)

## The Challenge

Learn to use Copilot's context selectors effectively: `#file`, `#selection`, `@workspace`

## Round 1: The File Reference Challenge

**Setup**: You have a `User.cs` model and need to create a `UserDto.cs`

**Task**: Use `#file:User.cs` in your prompt to generate a matching DTO

```text
Create a UserDto record based on #file:User.cs but exclude sensitive fields like PasswordHash and include only Id, Name, Email
```

**Scoring**:
- ✅ 3 points: DTO matches model structure
- ✅ 2 points: Sensitive fields excluded
- ✅ 1 point: Proper XML documentation generated

---

## Round 2: The Selection Surgeon

**Setup**: Select a complex method in your code

**Task**: Use `#selection` to ask targeted questions

```text
Refactor #selection to use the Strategy pattern instead of switch statements
```

**Learning**: Students discover how precise context leads to better suggestions

---

## Round 3: The Workspace Detective

**Setup**: A multi-project solution with services, repositories, and controllers

**Task**: Use `@workspace` to understand project structure

```text
@workspace Where is the UserService used? Show me all the places it's injected.
```

```text
@workspace What database tables exist in this project? List all Entity Framework entities.
```

**Scoring**:
- ✅ 3 points: Finds all usages across projects
- ✅ 2 points: Understands solution architecture
- ✅ 1 point: Generates accurate dependency graph

---

## Round 4: Combined Context Challenge

**Task**: Use multiple context selectors together

```text
@workspace Based on the existing repository pattern in #file:UserRepository.cs, create a new ProductRepository that follows the same conventions
```

---

## Learning Outcomes

- Understand when to use `#file` vs `#selection` vs `@workspace`
- Learn that better context = better suggestions
- Practice referencing code without copy-pasting
