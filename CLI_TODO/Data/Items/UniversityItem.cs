namespace CLI_TODO.Data.Items;

public class UniversityItem : ITodoItem
{
    public TodoType TodoType => TodoType.University;
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
}