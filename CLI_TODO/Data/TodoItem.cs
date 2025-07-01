namespace CLI_TODO.Data;

public interface TodoItem
{
    TodoType TodoType { get; }
    string Description { get; set; }
    DateTime DueDate { get; set; }
    bool IsCompleted { get; set; }
}