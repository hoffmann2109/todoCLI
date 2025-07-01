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
        result.TodoItem.PrintInfo();
        _databaseService.AddItemToList(result.TodoItem);
    }
    
    public void ProcessList()
    {
        var list = _databaseService.Todos;
        for (int i = 0; i < list.Count; i++)
        {
            list[i].PrintInfo();
        }
    }
    
    public void ProcessComplete(string[] tokens, InputMessage result)
    {
        var idToken = tokens[1];
        
        if (!Guid.TryParse(idToken, out var id))
        {
            Console.WriteLine($"Invalid ID format: '{idToken}'");
            return;
        }
        
        TodoItem item = _databaseService.GetItemById(id);
        item.IsCompleted = true;
        item.PrintInfo();
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
        _databaseService.AddItemsToDatabase();
        Environment.Exit(0);
    }
    
}