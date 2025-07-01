namespace CLI_TODO.Logic.InputOutput;

public class InputParser
{
    public Commands ParseInput(string input)
    {
        var command = Commands.Help;
        
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.Write("Invalid input");
            return command;
        }
        
        var trimmedStart = input.TrimStart();
        
        var endOfWord = 0;
        while (endOfWord < trimmedStart.Length && !char.IsWhiteSpace(trimmedStart[endOfWord]))
        {
            endOfWord++;
        }
        
        var stringInput = trimmedStart.Substring(0, endOfWord);
        
        command = Enum.Parse<Commands>(stringInput, ignoreCase: true);

        return command;
    }
}