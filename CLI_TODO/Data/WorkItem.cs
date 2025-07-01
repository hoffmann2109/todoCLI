namespace CLI_TODO.Data;

public class WorkItem : TodoItem
{
    public TodoType TodoType => TodoType.Work;
    public Guid Id { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }

}