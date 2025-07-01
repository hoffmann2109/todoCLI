using System.Collections;
using CLI_TODO.Data;
using DotNetEnv;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CLI_TODO.Database;

public class DatabaseService
{
    private static readonly List<TodoItem> _todos = new();
    private IMongoClient _client;
    private IMongoDatabase _database;
    private IMongoCollection<TodoItem> _todoCollection;

    public void InitializeDatabase()
    {
        const string envPath = "/home/thomas/RiderProjects/CLI_TODO/CLI_TODO/.env";
        Env.Load(envPath);
        
        var conn = Environment.GetEnvironmentVariable("MONGO_CONN")
                   ?? throw new InvalidOperationException("Set MONGO_CONN in .env");
        try
        {
            _client = new MongoClient(conn);
            _database = _client.GetDatabase("TodoDb");
            _todoCollection = _database.GetCollection<TodoItem>("todos");

            // Ping the server to verify connectivity
            var ping = new BsonDocument("ping", 1);
            _database.RunCommand<BsonDocument>(ping);

            Console.WriteLine("✅ Successfully connected to MongoDB.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Failed to connect to MongoDB: {ex.Message}");
        }
    }
    
    public void AddItemToList(TodoItem todoItem)
    {
        _todos.Add(todoItem);
    }
    
    public void AddItemsToDatabase()
    {
        foreach (TodoItem t in _todos)
        {
            // TODO: Implement method
        }
    }
    
    public TodoItem GetItemById(Guid id)
    {
        // Is it in the list?
        foreach (TodoItem t in _todos)
        {
            if (t.Id == id)
            {
                return t;
            }
        }
        
        // TODO: Is it in the database?
        return null;
    }
    
    public List<TodoItem> Todos
    {
        get { return _todos; }
    }
}