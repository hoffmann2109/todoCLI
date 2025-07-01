using System.Collections;
using CLI_TODO.Data;

namespace CLI_TODO.Database;

public class DatabaseService
{
    private static readonly List<TodoItem> _todos = new();

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