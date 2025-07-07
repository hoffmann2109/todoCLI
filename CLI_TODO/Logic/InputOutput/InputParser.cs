using CLI_TODO.Data;

namespace CLI_TODO.Logic.InputOutput;

public static class InputParser
{
    public static void ParseInput(string input, InputService inputService)
    {
        var result = new InputMessage();
        
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Input cannot be empty", nameof(input));
        }
        
        var tokens = input
            .Trim()
            .Split((char[])null!, StringSplitOptions.RemoveEmptyEntries);

        if (!Enum.TryParse<Commands>(tokens[0], ignoreCase: true, out var command))
        {
            throw new FormatException($"Unknown command '{tokens[0]}'");
        }
        result.Command = command;
        
        inputService.ProcessUserInput(command, tokens, result);
    }
}