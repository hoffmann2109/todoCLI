namespace CLI_TODO.Logic.InputOutput;

public class InputParser
{
    public Commands ParseInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.Write("Invalid input");
            return Commands.Help;
        }
        
        var trimmedStart = input.TrimStart();
        
        var endOfWord = 0;
        while (endOfWord < trimmedStart.Length && !char.IsWhiteSpace(trimmedStart[endOfWord]))
        {
            endOfWord++;
        }
        
        var stringInput = trimmedStart.Substring(0, endOfWord);
        
        // try parse, ignore case
        if (Enum.TryParse<Commands>(stringInput, ignoreCase: true, out var cmd)
            && Enum.IsDefined(typeof(Commands), cmd))
        {
            return cmd;
        }

        return Commands.Help;
    }
}