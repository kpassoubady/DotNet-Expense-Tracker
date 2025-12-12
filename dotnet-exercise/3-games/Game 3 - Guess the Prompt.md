# Guess the Prompt - Palindrome Checker - Reverse Engineering

## Instructions

1. You will be given a C# method that solves a specific problem.
2. Your task is to write a prompt that could have been used to generate this method using an AI coding assistant.
3. The prompt should be clear, specific, and include all necessary details to guide the AI in generating the provided method.
4. Consider including:
   - The problem statement
   - Input and output examples
   - Any constraints or edge cases
   - Requirements for code quality (e.g., comments, readability)
   - Testing requirements
   - Performance considerations
5. Aim to make the prompt as comprehensive as possible to ensure the AI can produce the exact solution.

```csharp
public class PalindromeChecker
{
    public static bool IsPalindrome(string input)
    {
        if (input == null) return false;
        var cleaned = new string(input.Where(char.IsLetterOrDigit).ToArray()).ToLower();
        var reversed = new string(cleaned.Reverse().ToArray());
        return cleaned.Equals(reversed);
    }

    public static void Main(string[] args)
    {
        Console.WriteLine(IsPalindrome("A man, a plan, a canal: Panama")); // true
        Console.WriteLine(IsPalindrome("Hello, world")); // false
    }
}
```
