using CLI_TODO.Data;

namespace CLI_TODO.Logic.InputOutput;

public class InputService
{
    private readonly CommandHandler _commandHandler = new();
    
    public void GetUserInput()
    {
        Console.Write("Enter a command or type 'help' > ");
        var input = Console.ReadLine() ?? string.Empty;
        InputParser.ParseInput(input);
    }

    public void ProcessUserInput(Commands command, string[] tokens, InputMessage result)
    {
        switch (command)
        {
            case Commands.Help: _commandHandler.ProcessHelp(); break;
            case Commands.Add: _commandHandler.ProcessAdd(tokens, result); break;
            case Commands.List: _commandHandler.ProcessList(); break;
            case Commands.Complete: _commandHandler.ProcessComplete(); break;
            case Commands.Reopen: _commandHandler.ProcessReopen(); break;
            case Commands.Update: _commandHandler.ProcessUpdate(); break;
            case Commands.Delete: _commandHandler.ProcessDelete(); break;
            case Commands.End: _commandHandler.ProcessEnd(); break;
            default:
                throw new ArgumentOutOfRangeException(nameof(command), command, null);
        }
    }
}