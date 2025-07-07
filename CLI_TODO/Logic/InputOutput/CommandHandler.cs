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
    
    public void ProcessList()
    {
        // Filter out completed items for now
        var sortedUncompleted = databaseService.Todos
            .Where(item => !item.IsCompleted)
            .OrderBy(item => item.DueDate);
        
        foreach (var todo in sortedUncompleted)
            todo.PrintInfo();
    }
    
    public void ProcessComplete(string[] tokens, InputMessage result)
    {
        var description = string.Join(" ", tokens.Skip(1));
        var filter = Builders<BsonDocument>.Filter.Eq("Description", description);

        var update = Builders<BsonDocument>.Update.Set("IsCompleted", true);

        databaseService.UpdateItem(filter, update);
        
        var todo = databaseService.Todos.FirstOrDefault(t => t.Description == description);
        if (todo != null)
        {
            todo.IsCompleted = true;
        }
            
    }

    private string GetDescription(string[] tokens)
    {
        var descTokens = tokens.Skip(2).Take(tokens.Length - 3);
        var description = string.Join(" ", descTokens);
        return description;
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