namespace CLI_TODO.Logic.InputOutput;

public class InputService
{
    private readonly InputParser _inputParser = new();
    private readonly CommandHandler _commandHandler = new();
    
    public void GetUserInput()
    {
        Console.Write("Enter a command or type 'help' > ");
        var input = Console.ReadLine() ?? string.Empty;
        var command = _inputParser.ParseInput(input);
        ProcessUserInput(command);
    }

    private void ProcessUserInput(Commands command)
    {
        switch (command)
        {
            case Commands.Help: _commandHandler.ProcessHelp(); break;
            case Commands.Add: _commandHandler.ProcessAdd(); break;
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