namespace learn;

public class DuplicateLetter
{
    public readonly record struct DuplicateResult(char Character, int Index);

    public static void Run()
    {
        String letters = "abcdefsa";
        
        DuplicateResult? result = FindFirstDuplicate(letters);
        Console.WriteLine(result);
    }

    public static DuplicateResult? FindFirstDuplicate(string input)
    {
        if (string.IsNullOrEmpty(input) || input.Length < 2)
        {
            return null;
        }

        var seen = new HashSet<char>();

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            // Skip non-letters if required
            if (!char.IsLetter(c))
            {
                continue;
            }

            // Normalize case if case-insensitive
            char normalized = char.ToLowerInvariant(c);

            if (!seen.Add(normalized))
            {
                return new DuplicateResult(c, i);
            }
        }

        return null;
    }
}