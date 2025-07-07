namespace CLI_TODO.Data.Items;

public class WorkItem : ITodoItem
{
    public TodoType TodoType => TodoType.Work;
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }

}