namespace CLI_TODO.Data;

public class WorkItem
{
    public TodoType TodoType => TodoType.Work;
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
}