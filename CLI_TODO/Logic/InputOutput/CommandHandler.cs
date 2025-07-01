using System.Globalization;
using CLI_TODO.Data;
using CLI_TODO.Database;

namespace CLI_TODO.Logic.InputOutput;

public class CommandHandler
{
    private readonly DatabaseService _databaseService =  new();
    public void ProcessHelp()
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Available commands:");
        Console.WriteLine("Help ... See available commands");
        Console.WriteLine("Add ... Add an item");
        Console.WriteLine("List ... List all todo items");
        Console.WriteLine("Complete ... Mark todo item as complete");
        Console.WriteLine("Reopen ... Reopen todo item");
        Console.WriteLine("Update ... Update a todo item");
        Console.WriteLine("Delete ... Delete a todo item");
        Console.WriteLine("End ... End app");
        Console.WriteLine("----------------------------------------");
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
    
    public void ProcessComplete()
    {

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