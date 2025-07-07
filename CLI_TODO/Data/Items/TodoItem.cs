namespace CLI_TODO.Data.Items;

public interface ITodoItem
{
    TodoType TodoType { get; }
    string? Description { get; set; }
    DateTime DueDate { get; set; }
    bool IsCompleted { get; set; }
    

    public void PrintInfo()
    {
        Console.WriteLine($"{TodoType}\t{Description}\t{DueDate:dd.MM.yyyy}");
    }
}
