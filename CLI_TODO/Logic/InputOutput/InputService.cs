using CLI_TODO.Data;
using CLI_TODO.Database;
using Sprache;

namespace CLI_TODO.Logic.InputOutput;

public class InputService(DatabaseService databaseService)
{
    private readonly CommandHandler _commandHandler = new(databaseService);

    public void GetUserInput()
    {
        Console.Write("Enter a command or type 'help' > ");
        var input = Console.ReadLine() ?? string.Empty;
        try
        {
            InputParser.ParseInput(input, this);
        }
        catch (Exception e)
        {
            Console.WriteLine("Please enter a valid command or type 'help'");
        }
  
    }

    public void ProcessUserInput(Commands command, string[] tokens, InputMessage result)
    {
        switch (command)
        {
            case Commands.Help: _commandHandler.ProcessHelp(); break;
            case Commands.Add: _commandHandler.ProcessAdd(tokens, result); break;
            case Commands.List: _commandHandler.ProcessList(tokens); break;
            case Commands.Complete: _commandHandler.ProcessStatusChange(tokens, true); break;
            case Commands.Reopen: _commandHandler.ProcessStatusChange(tokens, false); break;
            case Commands.Delete: _commandHandler.ProcessDelete(tokens); break;
            case Commands.End: _commandHandler.ProcessEnd(); break;
            default:
                throw new ArgumentOutOfRangeException(nameof(command), command, null);
        }
    }
}