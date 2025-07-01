namespace CLI_TODO.Data;

public interface TodoItem
{
    TodoType TodoType { get; }
    Guid Id  { get; set; }
    string Description { get; set; }
    DateTime DueDate { get; set; }
    bool IsCompleted { get; set; }
    

    public void printInfo()
    {
        Console.WriteLine($"{Id}\t{TodoType}\t{Description}\t{DueDate:dd.MM.yyyy}\t{IsCompleted}");
    }
    
}
