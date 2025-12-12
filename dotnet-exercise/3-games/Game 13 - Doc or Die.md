# Doc or Die 📝 (Documentation Generation)

## The Challenge

Transform undocumented "legacy" code into beautifully documented, maintainable code using Copilot.

## The Setup

You inherit this undocumented class:

```csharp
public class OrderProcessor
{
    private List<Order> _orders = new();
    private Dictionary<string, double> _discounts = new();
    
    public void AddOrder(Order o)
    {
        if (o != null && o.Total > 0)
        {
            _orders.Add(o);
        }
    }
    
    public double Calc(string code)
    {
        double t = 0;
        foreach (var o in _orders)
        {
            t += o.Total;
        }
        if (_discounts.ContainsKey(code))
        {
            t *= (1 - _discounts[code]);
        }
        return t;
    }
    
    public void AddD(string c, double d)
    {
        if (d > 0 && d < 1)
        {
            _discounts[c] = d;
        }
    }
}
```

---

## Round 1: Method Documentation Race

**Task**: Generate complete XML documentation for each method

**Prompts to try**:
```text
Generate XML documentation for this method including <param>, <returns>, and <exception>
```

```text
/doc Add comprehensive documentation explaining the business logic
```

**Scoring**:
- ✅ 2 points: Complete `<param>` descriptions
- ✅ 2 points: Clear `<returns>` explanation
- ✅ 1 point: Documents any exceptions
- ✅ 1 point: Explains business rules

---

## Round 2: Variable Naming Makeover

**Task**: Ask Copilot to suggest better names

```text
Suggest better variable names for this method. The current names are: t, o, c, d
```

**Expected transformation**:
- `t` → `totalAmount`
- `o` → `order`
- `c` → `discountCode`
- `d` → `discountPercentage`

---

## Round 3: Class-Level Documentation

**Task**: Generate a class-level overview

```text
Write a class-level XML documentation that explains:
- What this class does
- How to use it (with example)
- Thread-safety considerations
- Any limitations
```

---

## Round 4: README Generation

**Task**: Use the documented class to generate a README section

```text
Based on #file:OrderProcessor.cs, generate a README.md section explaining the order processing system with usage examples
```

---

## Bonus: Inline Comments Challenge

**Task**: Add inline comments explaining complex logic

```csharp
// Before: No comments
public double Calc(string code)
{
    double t = 0;
    foreach (var o in _orders)
    {
        t += o.Total;
    }
    if (_discounts.ContainsKey(code))
    {
        t *= (1 - _discounts[code]);
    }
    return t;
}
```

**Goal**:
```csharp
// After: Clear inline comments
public double CalculateTotal(string discountCode)
{
    // Sum all order totals
    double total = 0;
    foreach (var order in _orders)
    {
        total += order.Total;
    }
    
    // Apply discount if valid code provided
    if (_discounts.TryGetValue(discountCode, out var discountRate))
    {
        total *= (1 - discountRate);  // Reduce total by discount percentage
    }
    
    return total;
}
```

---

## Learning Outcomes

- Generate professional XML documentation
- Improve code readability with better naming
- Create user-facing documentation from code
- Understand documentation best practices
