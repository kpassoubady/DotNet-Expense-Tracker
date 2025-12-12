# Command Relay Race 🏃 (Slash Commands)

## The Challenge

Master Copilot's slash commands: `/explain`, `/fix`, `/tests`, `/doc`

## Race Format

Teams compete to complete tasks using ONLY slash commands. Manual coding = disqualification!

---

## Leg 1: The Explainer `/explain`

**Setup**: Given this cryptic legacy code:

```csharp
public int M(int[] a)
{
    int r = a[0];
    for (int i = 1; i < a.Length; i++)
    {
        r ^= a[i];
    }
    return r;
}
```

**Task**: Use `/explain` to understand what this code does

**Scoring**: First team to correctly identify "finds the single non-duplicate number using XOR" wins

---

## Leg 2: The Fixer `/fix`

**Setup**: This code has bugs:

```csharp
public List<string> FilterNames(List<string> names)
{
    return names
        .Where(n => n.Length > 3)
        .Select(n => n.ToUpper)
        .OrderBy(n => n)
        .ToList();
}
```

**Task**: Select the code and use `/fix` to identify and correct all issues

**Scoring**:
- ✅ 2 points: Fixes missing parentheses `ToUpper()`
- ✅ 1 point: Adds null check
- ✅ 1 point: Explains the fix

---

## Leg 3: The Tester `/tests`

**Setup**: Given this service method:

```csharp
public class DiscountCalculator
{
    public double CalculateDiscount(double price, string customerType)
    {
        if (customerType == "VIP") return price * 0.20;
        if (customerType == "REGULAR") return price * 0.10;
        return 0;
    }
}
```

**Task**: Use `/tests` to generate comprehensive xUnit tests

**Scoring**:
- ✅ 2 points: Tests all customer types
- ✅ 2 points: Tests edge cases (null, empty, negative price)
- ✅ 1 point: Uses `[Theory]` with `[InlineData]`

---

## Leg 4: The Documenter `/doc`

**Setup**: An undocumented utility class

```csharp
public static class StringUtils
{
    public static string Reverse(string input) { ... }
    public static bool IsPalindrome(string input) { ... }
    public static string Truncate(string input, int maxLength) { ... }
}
```

**Task**: Use `/doc` to generate complete XML documentation for all methods

**Scoring**:
- ✅ 1 point per method documented
- ✅ 1 point for `<param>` tags
- ✅ 1 point for `<returns>` tags
- ✅ 1 point for `<exception>` where applicable

---

## Bonus Round: Command Chain

**Challenge**: Fix, test, and document a method using only slash commands in sequence!

```csharp
// Start with buggy, untested, undocumented code
public int Fib(int n)
{
    if (n <= 1) return 1;  // Bug: should return n for fib(0)=0, fib(1)=1
    return Fib(n - 1) + Fib(n - 2);
}
```

1. `/fix` - Correct the base case
2. `/tests` - Generate tests
3. `/doc` - Add documentation

**Winner**: First team to have working, tested, documented code!

---

## Learning Outcomes

- Know which command to use for each situation
- Understand command capabilities and limitations
- Practice efficient AI-assisted workflows
