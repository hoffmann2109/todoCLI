using System.Collections;
using CLI_TODO.Data;
using DotNetEnv;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CLI_TODO.Database;

public class DatabaseService
{
    private static readonly List<TodoItem> _todos = new();
    private IMongoCollection<BsonDocument> _todosCollection;

    public void InitializeDatabase()
    {
        const string envPath = "/home/thomas/RiderProjects/CLI_TODO/CLI_TODO/.env";
        Env.Load(envPath);
        
        var conn = Environment.GetEnvironmentVariable("MONGO_CONN")
                   ?? throw new InvalidOperationException("Set MONGO_CONN in .env");

        if (conn == null)
        {
            Console.WriteLine("You need to set MONGO_CONN in .env");
            Environment.Exit(0);
        }
        
        try
        {
            var client = new MongoClient(conn);
            var database = client.GetDatabase("TodoDb");
            _todosCollection = database.GetCollection<BsonDocument>("todos");

            // Ping the server to verify connectivity
            var ping = new BsonDocument("ping", 1);
            database.RunCommand<BsonDocument>(ping);

            Console.WriteLine("✅ Successfully connected to MongoDB.");
            
            // Sample: First element:
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId("686b4b216a1e49005be5e964"));
            var document    = _todosCollection.Find(filter).FirstOrDefault();
            Console.WriteLine(document);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Failed to connect to MongoDB: {ex.Message}");
        }
    }
    

    public void AddItems(TodoItem todoItem)
    {
        _todos.Add(todoItem);
        
        var doc = new BsonDocument
        {
            { "Description", todoItem.Description },
            { "DueDate",     todoItem.DueDate },
            { "IsCompleted", todoItem.IsCompleted },
            { "TodoType",    todoItem.TodoType.ToString() }
        };
        _todosCollection.InsertOne(doc);

        Console.WriteLine("➡ Inserted TODO into MongoDB:");
        Console.WriteLine(doc);
    }
    
    public TodoItem GetItemById()
    {
        
        // TODO: Is it in the database?
        return null;
    }
    
    public List<TodoItem> Todos
    {
        get { return _todos; }
    }
}