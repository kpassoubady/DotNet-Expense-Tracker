# Hallucination Hunter 🐛 (Critical Review)

## The Challenge: The Thread-Safety Trap

Here is the code:

```csharp
public class DateUtil
{
    // Copilot suggests this 'optimization' to avoid creating instances
    private static readonly Regex DateRegex = new Regex(@"^\d{4}-\d{2}-\d{2}$");
    private static CultureInfo Culture = new CultureInfo("en-US");

    public static DateTime Parse(string dateStr)
    {
        return DateTime.Parse(dateStr, Culture);
    }
}
```

## The Goal 

Students must use Copilot Chat to ask, "Does this code have a bug/thread-safety issue?" or write a unit test to expose the bug. 

**Hint**: `CultureInfo` is mutable and not thread-safe when shared as a static field without proper synchronization.
