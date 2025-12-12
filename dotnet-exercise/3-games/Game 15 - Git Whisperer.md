# Git Whisperer 📝 (Git Integration)

## The Challenge

Use Copilot to write professional commit messages, PR descriptions, and understand code changes.

## Round 1: Commit Message Craftsman

**Setup**: You've made these changes to a file:

```diff
- public void ProcessOrder(Order order)
- {
-     if (order != null)
-     {
-         _repository.Save(order);
-     }
- }
+ public async Task ProcessOrderAsync(Order order)
+ {
+     ArgumentNullException.ThrowIfNull(order);
+     ValidateOrder(order);
+     var savedOrder = await _repository.SaveAsync(order);
+     await _eventPublisher.PublishAsync(new OrderCreatedEvent(savedOrder));
+     _logger.LogInformation("Order {OrderId} processed successfully", savedOrder.Id);
+ }
+
+ private void ValidateOrder(Order order)
+ {
+     if (!order.Items.Any())
+     {
+         throw new ArgumentException("Order must have at least one item", nameof(order));
+     }
+ }
```

**Task**: Ask Copilot to generate a commit message

```text
Generate a conventional commit message for these changes following the format:
type(scope): description

Include a body explaining what and why
```

**Expected output**:
```
feat(orders): add validation and event publishing to order processing

- Add null check using ArgumentNullException.ThrowIfNull
- Validate that orders contain at least one item
- Publish OrderCreatedEvent after successful save
- Add structured logging for successful order processing
- Convert to async/await pattern
```

**Scoring**:
- ✅ 2 points: Correct commit type (feat/fix/refactor)
- ✅ 2 points: Clear, concise subject line
- ✅ 2 points: Meaningful body with bullet points

---

## Round 2: PR Description Generator

**Task**: Generate a pull request description

```text
Generate a GitHub pull request description for the order processing changes including:
- Summary of changes
- Motivation/context
- Testing done
- Checklist items
```

**Template to follow**:
```markdown
## Summary
Brief description of what this PR does

## Changes
- [ ] Change 1
- [ ] Change 2

## Testing
How was this tested?

## Checklist
- [ ] Tests added/updated
- [ ] Documentation updated
- [ ] No breaking changes
```

---

## Round 3: Code Review Assistant

**Setup**: Review a teammate's code using Copilot

```csharp
public class UserService
{
    public User FindUser(string id)
    {
        var user = _userRepository.FindById(id);
        if (user == null)
        {
            return null;  // Is this the best approach?
        }
        user.LastAccessed = DateTime.Now;
        _userRepository.Save(user);
        return user;
    }
}
```

**Task**: Ask Copilot for review feedback

```text
Review this code and provide feedback on:
1. Error handling approach
2. Side effects (updating LastAccessed on read)
3. Thread safety
4. Suggested improvements
```

---

## Round 4: Changelog Generator

**Task**: Generate a CHANGELOG entry from recent commits

```text
Based on these commit messages, generate a CHANGELOG.md entry:

- feat(auth): add OAuth2 support for Google login
- fix(orders): prevent duplicate order submission
- refactor(users): extract validation to separate service
- docs(api): update Swagger annotations
```

**Expected format**:
```markdown
## [1.2.0] - 2024-01-15

### Added
- OAuth2 support for Google login (#123)

### Fixed
- Prevent duplicate order submission (#124)

### Changed
- Extract user validation to separate service (#125)

### Documentation
- Update Swagger API annotations (#126)
```

---

## Bonus: Release Notes

**Task**: Generate user-friendly release notes from technical changelog

```text
Convert this technical changelog into user-friendly release notes for non-technical stakeholders
```

---

## Learning Outcomes

- Write professional commit messages
- Create comprehensive PR descriptions
- Use AI for code review assistance
- Generate changelogs and release notes
- Communicate technical changes effectively
