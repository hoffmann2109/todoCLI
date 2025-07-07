using CLI_TODO.Data;
using CLI_TODO.Data.Items;
using DotNetEnv;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CLI_TODO.Database;

public class DatabaseService
{
    private static readonly List<ITodoItem> _todos = [];
    private IMongoCollection<BsonDocument> _todosCollection = null!;

    public void InitializeDatabase()
    {
        Env.TraversePath().Load();
        
        var conn = Environment.GetEnvironmentVariable("MONGO_CONN") 
                   ?? throw new InvalidOperationException("Set MONGO_CONN in .env");
        
        try
        {
            var client = new MongoClient(conn);
            var database = client.GetDatabase("TodoDb");
            _todosCollection = database.GetCollection<BsonDocument>("todos");
            
            var ping = new BsonDocument("ping", 1);
            database.RunCommand<BsonDocument>(ping);

            Console.WriteLine("✅ Successfully connected to MongoDB.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Failed to connect to MongoDB: {ex.Message}");
        }
        
        RestoreDatabase();
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
            
            DateTime dueDate;
            var dueVal = doc.GetValue("DueDate", BsonNull.Value);
            if (dueVal is BsonDateTime bsonDt)
            {
                dueDate = bsonDt.ToUniversalTime();
            }
            else
            {
                dueDate = DateTime.MinValue;
            }

            if (!Enum.TryParse<TodoType>(typeString, out var todoType))
            {
                todoType = TodoType.Personal;
            }
            
            var item = TodoItemFactory.Create(todoType, desc, dueDate);
            item.IsCompleted = isCompleted;

            _todos.Add(item);
        }
    }
    
    public void AddItems(ITodoItem todoItem)
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

        Console.WriteLine("Inserted TODO into MongoDB");
    }

    public void UpdateItem(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
    {
        _todosCollection.UpdateOne(filter, update);
    }
    
    public List<ITodoItem> Todos => _todos;
}