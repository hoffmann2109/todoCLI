using System.Globalization;
using CLI_TODO.Data;
using CLI_TODO.Database;

namespace CLI_TODO.Logic.InputOutput;

public class CommandHandler
{
    private readonly DatabaseService _databaseService =  new();
    private readonly OutputService _outputService = new ();
    public void ProcessHelp()
    {
        _outputService.PrintCommands();
    }
    
    public void ProcessAdd(string[] tokens, InputMessage result)
    {
        if (!Enum.TryParse<TodoType>(tokens[1], ignoreCase: true, out var type))
            throw new FormatException($"Unknown todo type '{tokens[1]}'");
        
        var rawDate = tokens[^1];
        if (!DateTime.TryParseExact(
                rawDate,
                "dd.MM.yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var dueDate))
        {
            throw new FormatException($"Date '{rawDate}' is not in the expected format dd.MM.yyyy");
        }
        
        var descTokens = tokens.Skip(2).Take(tokens.Length - 3);
        var description = string.Join(" ", descTokens);
        
        var item = TodoItemFactory.Create(type, description, dueDate);
        result.TodoItem = item;
        
        Console.WriteLine("Added item: ");
        result.TodoItem.printInfo();
        _databaseService.addItemToDatabase(result.TodoItem);
    }
    
    public void ProcessList()
    {

    }
    
    public void ProcessComplete(InputMessage result)
    {
        if (result.TodoItem != null)
        {
            result.TodoItem.IsCompleted = true;
        }
    }
    
    public void ProcessReopen()
    {

    }
    
    public void ProcessUpdate()
    {

    }
    
    public void ProcessDelete()
    {

    }

    public void ProcessEnd()
    {
        Environment.Exit(0);
    }
    
}