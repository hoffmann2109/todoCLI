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
        // PC:
        // const string envPath = "/home/thomas/RiderProjects/CLI_TODO/CLI_TODO/.env";
        
        // Laptop:
        const string envPath = "/home/thomas/todoCLI/CLI_TODO/.env";
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
            
            // Serialize all objects into the list:
            RestoreDatabase();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Failed to connect to MongoDB: {ex.Message}");
        }
    }

    private void RestoreDatabase()
    {
        var emptyFilter = Builders<BsonDocument>.Filter.Empty;
        var allDocs     = _todosCollection.Find(emptyFilter).ToList();

        foreach (var doc in allDocs)
        {
            var desc        = doc.GetValue("Description", "").AsString;
            var isCompleted = doc.GetValue("IsCompleted", false).AsBoolean;
            var typeString  = doc.GetValue("TodoType",    "").AsString;

            // Safe DateTime extraction:
            DateTime dueDate;
            var dueVal = doc.GetValue("DueDate", BsonNull.Value);
            if (dueVal is BsonDateTime bsonDt)
                dueDate = bsonDt.ToUniversalTime();
            else
                dueDate = DateTime.MinValue;   // or whatever default you prefer

            // Parse enum
            if (!Enum.TryParse<TodoType>(typeString, out var todoType))
                todoType = TodoType.Personal;

            // Factory + restore
            var item = TodoItemFactory.Create(todoType, desc, dueDate);
            item.IsCompleted = isCompleted;

            _todos.Add(item);
            Console.WriteLine($"🔄 Restored ({todoType}): {item.Description} – Due {item.DueDate}");
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