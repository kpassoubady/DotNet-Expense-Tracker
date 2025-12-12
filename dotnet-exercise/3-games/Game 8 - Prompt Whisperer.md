# The "Prompt Whisperer" Mystery 🕵️ (Prompt Engineering)

The Challenge: Reverse-engineer a complex LINQ query.

## The Setup

Here is the code:

```csharp
var result = transactions
    .Where(t => t.Amount > 1000)
    .GroupBy(t => t.Currency)
    .ToDictionary(g => g.Key, g => (long)g.Count());
```

## The Goal

Students must write a natural language prompt that gets Copilot to output this exact LINQ pipeline on the first try.
