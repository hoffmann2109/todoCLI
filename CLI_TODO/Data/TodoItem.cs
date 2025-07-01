namespace CLI_TODO.Data;

public interface TodoItem
{
    TodoType TodoType { get; }
    string Description { get; set; }
    DateTime DueDate { get; set; }
    bool IsCompleted { get; set; }

    public void printInfo()
    {
        Console.WriteLine($"{TodoType}\t{Description}\t{DueDate:dd.MM.yyyy}\t{IsCompleted}");
    }
}
