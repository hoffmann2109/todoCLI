namespace CLI_TODO.Logic.InputOutput;

public class InputService
{
    private InputParser inputParser = new();
    
    public void getUserInput()
    {
        Console.Write("Enter your command ");
        Console.Write("> ");
        var input = Console.ReadLine() ?? string.Empty;
        var command = inputParser.ParseInput(input);
        Console.WriteLine(command);
    }
}