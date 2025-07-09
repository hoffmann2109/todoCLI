using System.Globalization;
using CLI_TODO.Data;
using CLI_TODO.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CLI_TODO.Logic.InputOutput;

public class CommandHandler(DatabaseService databaseService)
{
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
        
        var description = GetDescription(tokens);
        
        var item = TodoItemFactory.Create(type, description, dueDate);
        result.TodoItem = item;
        
        Console.WriteLine("Added item: ");
        result.TodoItem.PrintInfo();
        databaseService.AddItems(result.TodoItem);
    }
    
    public void ProcessList(string[] tokens)
    {
        if (tokens.Length > 1 && !string.IsNullOrWhiteSpace(tokens[1]))
        {
            var flag = tokens[1].Trim();
            
            switch (flag.ToLowerInvariant())
            {
                case "--all": // --all: show completed + uncompleted
                    var allTodos = databaseService.Todos
                        .OrderBy(item => item.DueDate);
                    foreach (var todo in allTodos)
                        todo.PrintInfo();
                    return;

                // TODO: add more flags
                default:
                    Console.WriteLine($"Unknown flag: {flag}");
                    return;
            }
        }

        // No flag: filter out completed items
        var sortedUncompleted = databaseService.Todos
            .Where(item => !item.IsCompleted)
            .OrderBy(item => item.DueDate);

        foreach (var todo in sortedUncompleted)
            todo.PrintInfo();
    }
    
    public void ProcessStatusChange(string[] tokens, bool isComplete)
    {
        var description = string.Join(" ", tokens.Skip(1));
        databaseService.UpdateItem(description, isComplete);
    }

    private string GetDescription(string[] tokens)
    {
        var descTokens = tokens.Skip(2).Take(tokens.Length - 3);
        var description = string.Join(" ", descTokens);
        return description;
    }
    
    public void ProcessDelete(string[] tokens)
    {
        var description = string.Join(" ", tokens.Skip(1));
        databaseService.DeleteItem(description);
    }

    public void ProcessEnd()
    {
        Environment.Exit(0);
    }
    
}